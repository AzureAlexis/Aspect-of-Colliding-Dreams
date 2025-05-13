using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Public variables
    public int[] patternIDs;
    public float hp;

    // Refrences
    GameObject player;

    // Stuff for waypoints
    Vector3 waypointOld = new Vector3(0, 0, 0);
    Vector3 waypointPosition = new Vector3(0, 0, 0);
    Vector3 home;
    float waypointDuration = 0;
    float waypointTime = 0;

    // Stuff for patterns
    private int currentPatternIndex = 0;
    private int currentPatternId;
    private EnemyPattern pattern;
    public float patternTime;

    // Refrences for UI
    Image circleInner;
    Image circleOuter;
    Image circleSpellcard;
    Image circleNotches;
    Image lineInner;
    Image lineOuter;
    Image lineSpellcard;
    Image animaInner;
    Image animaOuter;


    // Stats
    float patternHp;
    float patternMhp;
    float totalHp;
    float totalMhp;
    float spellsLeft = 0;

    public bool boss = false;
    bool dying;
    float deathTick = 0;
    public AudioSource death;

    void Start()
    {
        currentPatternId = patternIDs[0];
        player = PlayerStats.player;
        totalMhp = hp;
        home = transform.position;
        BuildHp();
        BuildRefrences();
    }

    void BuildHp()
    {
        totalMhp = 0;
        for(int i = 0; i < patternIDs.Length; i++)
        {
            totalMhp += PatternManager.GetEnemyPattern(patternIDs[i]).endValue;
            if(PatternManager.GetEnemyPattern(patternIDs[i]).spell)
                spellsLeft++;
        }
        totalHp = totalMhp;
        patternMhp = PatternManager.GetEnemyPattern(patternIDs[0]).endValue;
        patternHp = patternMhp;
    }

    void BuildRefrences()
    {
        Transform enemyHealth = GameObject.Find("BossHealth").transform;

        circleOuter = enemyHealth.GetChild(0).GetComponent<Image>();
        circleInner = enemyHealth.GetChild(0).GetChild(0).GetComponent<Image>();
        circleSpellcard = enemyHealth.GetChild(0).GetChild(1).GetComponent<Image>();
        circleNotches = enemyHealth.GetChild(0).GetChild(2).GetComponent<Image>();

        lineOuter = enemyHealth.GetChild(1).GetComponent<Image>();
        lineInner = enemyHealth.GetChild(1).GetChild(0).GetComponent<Image>();
        lineSpellcard = enemyHealth.GetChild(1).GetChild(1).GetComponent<Image>();

        animaOuter = enemyHealth.GetChild(2).GetComponent<Image>();
        animaInner = enemyHealth.GetChild(2).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = PlayerStats.player;
        if(IsActive() && !dying)
        {
            UpdateHP();
            UpdatePosition();
            UpdatePattern();
            UpdateShots();
        }
        else if(dying)
        {
            UpdateDeath();
        }
    }

    void UpdateDeath()
    {
        if(deathTick <= 2.5f)
        {
            deathTick += Time.deltaTime;

            if(deathTick >= 2.5f)
            {
                DanmakuManager.ClearAllBullets();
            }
        }
    }

    void UpdatePattern()
    {
        if(pattern == null)
        {
            pattern = PatternManager.GetEnemyPattern(currentPatternId);
        }
        
        switch (pattern.endCondition)
        {
            case "hp":
                if(patternHp < 0)
                {
                    NextPattern();
                }
                break;
            case "time":
                if(patternTime > pattern.endValue)
                {
                    NextPattern();
                }
                break;
        }
    }

    void NextPattern()
    {
        if(pattern.endEvent != "none")
            TextManager.StartConversation(pattern.endEvent);
        if(pattern.spell)
            spellsLeft--;

        currentPatternIndex += 1;

        currentPatternId = patternIDs[currentPatternIndex];
        patternTime = 0;
        pattern = PatternManager.GetEnemyPattern(currentPatternId);

        patternMhp = pattern.endValue;
        patternHp = patternMhp;
        DanmakuManager.ClearAllBullets();
        MakeWaypoint("home");
        
        // if(pattern.background >= 0)
            // UIManager.ChangeBackgroundStatus(pattern.background);
    }

    void UpdateShots()
    {
        if(IsActive())
        {
            patternTime += Time.deltaTime;
            FireShotsReady();
        }
    }

    void UpdatePosition()
    {
        if(waypointPosition != transform.position && waypointPosition != new Vector3(0, 0, 0))
        {
            float factor = waypointTime / waypointDuration;
            transform.position = AzalUtil.QuadOut(waypointOld, waypointPosition, factor);
            waypointTime += Time.deltaTime;
        }
    }

    void UpdateHP()
    {
        if(hp <= 0)
            StartDeath();

        if(boss && pattern != null)
        {
            if(pattern.lastWord)
            {
                circleSpellcard.fillAmount = Mathf.Clamp(patternHp / (patternMhp * 0.4f), 0, 1);
                lineSpellcard.fillAmount = Mathf.Clamp((patternHp - patternMhp * 0.4f) / (patternMhp * 0.6f), 0, 1);
            }
            else
            {
                
                circleSpellcard.fillAmount = 0;
                for(int i = 0; i < spellsLeft; i++)
                {
                    circleSpellcard.fillAmount += 1f/8f;
                }

                circleNotches.fillAmount = 1f/16f;
                for(int i = 0; i < spellsLeft; i++)
                    circleNotches.fillAmount += 1f/8f;

                lineSpellcard.fillAmount = 0;
                
                if(pattern.spell)
                {
                    lineInner.fillAmount = 0;
                    circleInner.fillAmount = 0;
                    circleSpellcard.fillAmount -= 0.125f * (1 - patternHp / patternMhp);
                    Debug.Log(patternHp / patternMhp);
                }
                else
                {
                    float percentToCircle = (1 - circleSpellcard.fillAmount) * 0.4f;
                    float percentToLine = 1 - percentToCircle;
                    float amountToCircle = patternMhp * percentToCircle;
                    float amountToLine = patternMhp - amountToCircle;

                    circleInner.fillAmount = circleSpellcard.fillAmount + Mathf.Clamp(patternHp / amountToCircle, 0, 1);
                    lineInner.fillAmount = Mathf.Clamp((patternHp - amountToCircle) / amountToLine, 0, 1);
                }
            }
        }
    }

    void StartDeath()
    {
        dying = true;
        death.Play();
    }

    void MakeWaypoint(string template)
    {
        float wayX = transform.position.x;;
        float wayY = transform.position.y;
        float wayD = 0;

        switch (template)
        {
            case "toPlayer" :
                wayX = player.transform.position.x;
                if(wayX < 0)
                {
                    wayX = Mathf.Max(wayX, transform.position.x - 1);
                }
                else
                {
                    wayX = Mathf.Min(wayX, transform.position.x + 1);
                }

                wayD = 1;
                break;

            case "geyser" :
                wayY -= 6.5f;
                wayD = 1.5f;
                break;

            case "return" :
                wayX = waypointOld.x;
                wayY = waypointOld.y;
                wayD = 1;
                break;

            case "home" :
                wayX = home.x;
                wayY = home.y;
                wayD = 1;
                break;

            case "random":
                wayX = Random.Range(-4, 4) + home.x;
                wayY = Random.Range(-1, 1) + home.y;
                wayD = 1f;
                break;
        }

        waypointOld = transform.position;
        waypointDuration = wayD;
        waypointPosition = new Vector3(wayX, wayY, 0);
        waypointTime = 0;
    }

    public bool IsActive()
    {
        return BattleManager.active && !TextManager.active;
    }

    void FireShotsReady()
    {
        for(int i = 0; i < pattern.shots.Count; i++)
        {
            if(InRange(pattern.shots[i]))
            {
                if(pattern.shots[i].data != null)
                    DanmakuManager.Fire(pattern.shots[i].data.danmaku, gameObject);

                if(pattern.shots[i].movement != null)
                    MakeWaypoint(pattern.shots[i].movement);

                if(pattern.shots[i].sound != null)
                {
                    pattern.shots[i].sound.Play(0);
                    Debug.Log(pattern.shots[i].sound);
                }
            }
        }
    }

    public bool InRange(EnemyShot shot)
    {
        float startTime = shot.startTime;
        float endTime = shot.endTime;
        float loopDelay = shot.loopDelay;

        List<float> shotTimes = new List<float>();
        for(float i = startTime; i <= endTime; i += loopDelay)
        {
            shotTimes.Add(i);
        }

        for(int i = 0; i < shotTimes.Count; i++)
        {
            float time = shotTimes[i];
            if(time <= patternTime && time + Time.smoothDeltaTime >= patternTime)
            {
                return true;
            }
        }

        return false;
    }

    public void TakeDamage(float damage)
    {
        if(pattern != null)
        {
            if(pattern.endCondition == "hp")
            {
                patternHp -= damage;
            }
        }
    }
}
