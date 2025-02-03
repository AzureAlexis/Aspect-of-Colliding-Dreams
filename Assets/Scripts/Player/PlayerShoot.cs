using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot
{
    // Refrences
    public static GameObject player;
    
    static List<float> cooldowns = new List<float>();
    static float globalCooldown = 0;
    public static bool spellActive = false;
    public static PlayerPattern activeSpell;
    public static float patternTime;

    public static void Update()
    {
        UpdateInputs();
        UpdateSpells();
        UpdateCooldowns();
    }

    static void UpdateShots()
    {
        if(BattleManager.IsActive())
        {
            if(Input.GetKey(KeyCode.Z) && cooldowns[0] <= 0)
            {
                Fire(PlayerStats.danmaku1, 0);
            }
        }
    }

    public static void UpdateInputs()
    {
        /*
        if(BattleManager.IsActive() && globalCooldown <= 0)
        {
            for(int i = 0; i < PlayerStats.battleSlots.Capacity; i++)
            {
                BattleSlotBase slot = PlayerStats.battleSlots[i];

                switch(slot.GetType().ToString())
                {
                    case "Consumable":
                        UseItem(slot);
                        break;

                    case "PlayerShot"
                        Fire(slot);

                    case "PlayerSpell"
                        StartSpell(slot);
                }
            }
        }
        */
    }

    static void UpdateSpells()
    {
        if(BattleManager.IsActive())
        {
            if(Input.GetKeyDown(KeyCode.X) && !spellActive)
            {
                StartSpell(PlayerStats.spell1);
            }
            else if(spellActive)
            {
                UpdateActiveSpell();
            }
        }
    }

    static void StartSpell(int id)
    {
        spellActive = true;
        activeSpell = PatternManager.GetPlayerPattern(id);
        PlayerStats.invState = 1;
    }

    static void UpdateActiveSpell()
    {
        patternTime += Time.deltaTime;
        FireShotsReady();

        if(patternTime > activeSpell.endTime)
        {
            EndSpell();
        }
    }

    static void EndSpell()
    {
        spellActive = false;
        activeSpell = null;
    }

    static void FireShotsReady()
    {
        for(int i = 0; i < activeSpell.shots.Count; i++)
        {
            if(InRange(activeSpell.shots[i]))
            {
                DanmakuManager.Fire(activeSpell.shots[i].data.danmaku, player);
            }
        }
    }

    static public bool InRange(PlayerShot shot)
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

    static void UpdateCooldowns()
    {
        for(int i = 0; i < cooldowns.Count; i++)
        {
            cooldowns[i] = Mathf.Max(cooldowns[i] - Time.deltaTime, 0);
        }
    }

    static void Fire(GameObject prefab, int slot)
    {
        GameObject danmaku = MonoBehaviour.Instantiate(prefab, player.transform.position, prefab.transform.rotation, GameObject.Find("DanmakuManager").transform);
        danmaku.GetComponent<ComplexDanmaku>().active = true;

        cooldowns[slot] = danmaku.GetComponent<ComplexDanmaku>().cooldown;
    }
    
    static void UseItem(Consumable item)
    {
        switch (item.effect) 
        {
            case "hp":
                PlayerStats.hp += item.value * PlayerStats.mhp;
                break;

            case "ap":
                PlayerStats.ap += item.value * PlayerStats.map;
                break;
        }

        PlayerStats.consumables[PlayerStats.GetItemIndex(item.name)].count -= 1;
        if(PlayerStats.consumables[PlayerStats.GetItemIndex(item.name)].count <= 0)
        {
            PlayerStats.consumables.RemoveAt(PlayerStats.GetItemIndex(item.name));
        }
    }

    bool IsActive()
    {
        return true;
    }
}
