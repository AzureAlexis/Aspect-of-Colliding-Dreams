/*
    This is a static class that holds information about the player's stats and inventory.
*/

using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats
{
    // Refrences
    public static GameObject player;

    // Base stats (before equipment)
    public static float basePower = 0;      // Determines how much a shot's power is multiplied by when calculating damage
    public static float baseMagic = 0;      // Determines how much a spell's power is multiplied by when calculating damage. Also increases AP by 1 per point
    public static float baseStamina = 0;    // Determines how much HP the player has. Increases HP by 1 per point
    public static float baseEvasion = 20;   // Determines how big the player's hitbox is. Decreases hitbox size by 1% per point (past base)
    public static float baseSpeed = 20;     // Determines how fast the player moves. Increases speed by 0.4 units/sec per point
    public static float baseCharge = 20;    // Determines how much temp HP/AP is gained by grazing. Increases gain by 1% per point
    
    // Total stats (after equipment)
    public static float totalPower;
    public static float totalMagic;
    public static float totalStamina;
    public static float totalEvasion;
    public static float totalSpeed;
    public static float totalCharge;

    // Volitile stats (things that change a lot based on other vars)
    public static float hp;                 // Self explanatory
    public static float mhp;                // Maximum HP
    public static float thp;                // Temp HP. Aquired by items/grazing
    public static float ap;                 // AP. Used for shots/spells
    public static float map;                // Maximum AP
    public static float tap;                // Temp AP. Aquired by items/grazing
    public static float inv = 0;            // How much invincibility is left, in sec. If >0, the player can't take damage
    public static int invState = 0;         // Used to determine how inv should change. 0 = nothing, 1 = increase, 2 = decrease
    
    // Battle slots
    public static List<BattleSlotBase> battleSlots = new List<BattleSlotBase>(6);

    // Current items
    public static List<Consumable> consumables = new List<Consumable>();   // What consumables the player currently has
    public static List<Equipment> equipments = new List<Equipment>();      // What equipment the player currently has
    public static List<Resource> resources = new List<Resource>();         // What resources the player currently has
    public static Equipment weapon;                                        // The equipped weapon
    public static Equipment armor;                                         // The equipped armor
    public static List<Equipment> charms;                                  // The equipped charms


    public static GameObject danmaku1;
    public static GameObject circle;
    public static int spell1 = 0;
    public static bool hit = false;

    public AudioSource dead;

    // Update is called once per frame
    public static void Update()
    {
        CalculateStats();
        CalculateHP();
        CalculateAP();
        CalculateInv();
        UIManager.UpdatePlayerHp(hp);
    }

    static void CalculateStats()
    {
        totalPower = GetStatTotal("power");
        totalMagic = GetStatTotal("magic");
        totalStamina = GetStatTotal("stamina");
        totalSpeed = GetStatTotal("speed");
        totalEvasion = GetStatTotal("evasion");
        totalCharge = GetStatTotal("charge");
    }

    static void CalculateHP()
    {
        mhp = totalStamina;

        if(mhp < hp)                            // Reduce HP to MHP if greater
            hp = mhp;

        if(thp > 0)                             // If you have Temp HP, reduce it over time
            thp -= mhp * 0.1f * Time.deltaTime;

        if(thp < 0)                             // If Temp HP is less than 0, set to 0
            thp = 0;
    }

    static void CalculateAP()
    {
        map = totalMagic * 10;

        if(map < ap)                            // Reduce AP to MAP if greater
            ap = map;

        if(tap > 0)                             // If you have Temp AP, reduce it over time
            tap -= map * 0.1f * Time.deltaTime;

        if(thp < 0)                             // If Temp AP is less than 0, set to 0
            tap = 0;
    }

    static float GetStatTotal(string stat)
    {
        float returnValue = 0;

        switch(stat)
        {
            case "power":
                returnValue += basePower;
                returnValue += weapon.power;
                returnValue += armor.power;
                for(int i = 0; i < charms.Count; i++)
                    returnValue += charms[i].power;

                break;

            case "magic":
                returnValue += baseMagic;
                returnValue += weapon.magic;
                returnValue += armor.magic;
                for(int i = 0; i < charms.Count; i++)
                    returnValue += charms[i].magic;

                break;

            case "stamina":
                returnValue += baseStamina;
                returnValue += weapon.stamina;
                returnValue += armor.stamina;
                for(int i = 0; i < charms.Count; i++)
                    returnValue += charms[i].stamina;
                
                break;

            case "speed":
                returnValue += baseSpeed;
                returnValue += weapon.speed;
                returnValue += armor.speed;
                for(int i = 0; i < charms.Count; i++)
                    returnValue += charms[i].speed;
                
                break;

            case "evasion":
                returnValue += baseEvasion;
                returnValue += weapon.evasion;
                returnValue += armor.evasion;
                for(int i = 0; i < charms.Count; i++)
                    returnValue += charms[i].evasion;
                
                break;

            case "charge":
                returnValue += baseCharge;
                returnValue += weapon.charge;
                returnValue += armor.charge;
                for(int i = 0; i < charms.Count; i++)
                    returnValue += charms[i].charge;
                
                break;
        }

        return returnValue;
    }

    public static List<Consumable> GetConsumables(int type = 2)
    {
        return consumables;
    }

    public static List<Equipment> GetEquipment(int type = 3)
    {
        return equipments;
    }

    public static List<Resource> GetResources()
    {
        return resources;
    }

    public static int GetItemIndex(string name)
    {
        for(int i = 0; i < consumables.Count; i++)
        {
            if(consumables[i].name == name)
                return i;
        }
        for(int i = 0; i < resources.Count; i++)
        {
            if(resources[i].name == name)
                return i;
        }
        for(int i = 0; i < equipments.Count; i++)
        {
            if(equipments[i].name == name)
                return i;
        }
        return -1;
    }

    public static void GainItem(string name, int count = 1)
    {
        int index;
        ItemBase item = ItemManager.GetItemByName(name);
        item.count = count;

        switch(item.GetType().ToString())
        {
            case "Consumable":
                index = GetItemIndex(name);
                if(index >= 0)
                    consumables[index].count += count;
                else
                    consumables.Add(item as Consumable);
                return;

            case "Resource":
                index = GetItemIndex(name);
                if(index >= 0)
                    resources[index].count += count;
                else
                    resources.Add(item as Resource);
                return;

            case "Equipment":
                for(int i = 0; i < count; i++)
                    equipments.Add(item as Equipment);
                return;
        }
    }

    static void CalculateInv()
    {
        if(invState == 1)
        {
            inv += Time.deltaTime * 10;
            if(inv >= 5)
            {
                inv = 5;
                invState = 2;
            }
        }
        else if(invState == 2)
        {
            inv -= Time.deltaTime;
            if(inv <= 0)
            {
                inv = 0;
                invState = 0;
            }
        }

        circle.transform.localScale = new Vector3(inv * 0.1f, inv * 0.1f, 1);
        circle.transform.localEulerAngles = new Vector3(0, 0, inv * 180);
    }

    public static void TakeDamage(float damage)
    {
        if(inv == 0)
        {
            hp -= damage;
            invState = 1;
        }
    }
}
