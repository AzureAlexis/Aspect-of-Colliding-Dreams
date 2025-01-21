using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    List<float> cooldowns = new List<float>();
    PlayerStats playerStats;
    bool spellActive = false;
    public PlayerPattern activeSpell;
    public float patternTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldowns.Add(0);
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShots();
        UpdateSpells();
        UpdateCooldowns();
    }

    void UpdateShots()
    {
        if(BattleManager.IsActive())
        {
            if(Input.GetKey(KeyCode.Z) && cooldowns[0] <= 0)
            {
                Fire(GetComponent<PlayerStats>().danmaku1, 0);
            }
        }
    }

    void UpdateSpells()
    {
        if(BattleManager.IsActive())
        {
            if(Input.GetKeyDown(KeyCode.X) && !spellActive)
            {
                StartSpell(playerStats.spell1);
            }
            else if(spellActive)
            {
                UpdateActiveSpell();
            }
        }
    }

    void StartSpell(int id)
    {
        spellActive = true;
        activeSpell = PatternManager.GetPlayerPattern(id);
    }

    void UpdateActiveSpell()
    {
        patternTime += Time.deltaTime;
        FireShotsReady();

        if(patternTime > activeSpell.endTime)
        {
            EndSpell();
        }
    }

    void EndSpell()
    {
        spellActive = false;
        activeSpell = null;
    }

    void FireShotsReady()
    {
        for(int i = 0; i < activeSpell.shots.Count; i++)
        {
            if(InRange(activeSpell.shots[i]))
            {
                DanmakuManager.Fire(activeSpell.shots[i].data.danmaku, gameObject);
            }
        }
    }

    public bool InRange(PlayerShot shot)
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
            if(shotTimes[i] - Time.deltaTime < patternTime && shotTimes[i] >= patternTime)
            {
                return true;
            }
        }

        return false;
    }

    void UpdateCooldowns()
    {
        for(int i = 0; i < cooldowns.Count; i++)
        {
            cooldowns[i] = Mathf.Max(cooldowns[i] - Time.deltaTime, 0);
        }
    }

    void Fire(GameObject prefab, int slot)
    {
        GameObject danmaku = Instantiate(prefab, transform.position, prefab.transform.rotation, GameObject.Find("DanmakuManager").transform);
        danmaku.GetComponent<ComplexDanmaku>().active = true;

        cooldowns[slot] = danmaku.GetComponent<ComplexDanmaku>().cooldown;
    }
    
    bool IsActive()
    {
        return true;
    }
}
