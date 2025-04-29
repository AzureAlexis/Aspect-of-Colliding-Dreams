using System.Collections.Generic;
using UnityEngine;

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
    float loopTime;
    float patternTime;

    // Misc vars
    float mhp;

    public bool boss = false;
    bool dying;
    float deathTick = 0;
    public AudioSource death;

    void Start()
    {
        currentPatternId = patternIDs[0];
        player = PlayerStats.player;
        mhp = hp;
        home = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = PlayerStats.player;
        if(IsActive() && !dying)
        {
            // UpdatePosition();
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
                TextManager.StartConversation("postFight");
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
                if(hp < pattern.endValue)
                {
                    hp = pattern.endValue;
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

        currentPatternIndex += 1;

        currentPatternId = patternIDs[currentPatternIndex];
        loopTime = 0;
        patternTime = 0;
        pattern = PatternManager.GetEnemyPattern(currentPatternId);
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
            loopTime += Time.deltaTime;
            FireShotsReady();
            if(loopTime >= pattern.loopTime)
            {
                loopTime = 0;
            }
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

        if(boss)
            UIManager.UpdateBossHp(hp, mhp);
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
                hp -= damage;
            }
        }
    }
}
