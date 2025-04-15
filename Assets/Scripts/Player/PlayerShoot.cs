using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot
{
    // Refrences
    public static GameObject player;
    
    // Inputs
    static bool[] keysDown = new bool[6];
    static bool[] keys = new bool[6];

    static BattleSlotBase currentAction;
    static List<float> cooldowns = new List<float>();
    static float globalCooldown = 0;
    public static bool spellActive = false;
    public static PlayerPattern activeSpell;
    public static float patternTime;

    public static void Update()
    {
        UpdateInputs();
        UpdateAction();
        UpdateSpells();
        UpdateCooldowns();
    }

    static void UpdateAction()
    {
        MakeAction();
        DoAction();
    }

    static void MakeAction()
    {
        for(int i = 0; i < 7; i++)
            if(keysDown[i])
                currentAction = PlayerStats.battleSlots[i];
    }

    static void DoAction()
    {

    }

    public static void UpdateInputs()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            keysDown[0] = true;
        else
            keysDown[0] = false;
        if(Input.GetKey(KeyCode.Z))
            keys[0] = true;
        else
            keys[0] = false;

        if(Input.GetKeyDown(KeyCode.X))
            keysDown[1] = true;
        else
            keysDown[1] = false;
        if(Input.GetKey(KeyCode.X))
            keys[1] = true;
        else
            keys[1] = false;

        if(Input.GetKeyDown(KeyCode.C))
            keysDown[2] = true;
        else
            keysDown[2] = false;
        if(Input.GetKey(KeyCode.C))
            keys[2] = true;
        else
            keys[2] = false;

        if(Input.GetKeyDown(KeyCode.A))
            keysDown[3] = true;
        else
            keysDown[3] = false;
        if(Input.GetKey(KeyCode.A))
            keys[3] = true;
        else
            keys[3] = false;

        if(Input.GetKeyDown(KeyCode.S))
            keysDown[4] = true;
        else
            keysDown[4] = false;
        if(Input.GetKey(KeyCode.S))
            keys[4] = true;
        else
            keys[4] = false;

        if(Input.GetKeyDown(KeyCode.D))
            keysDown[5] = true;
        else
            keysDown[5] = false;
        if(Input.GetKey(KeyCode.D))
            keys[5] = true;
        else
            keys[5] = false;
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
