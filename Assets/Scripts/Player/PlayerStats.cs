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
    
    public static float hp = 5;

    // Items
    public static List<Consumable> consumables = new List<Consumable>();   // What consumables the player currently has
    public static List<Equipment> equipments = new List<Equipment>();      // What equipment the player currently has
    public static List<Resource> resources = new List<Resource>();         // What resources the player currently has
    public static Equipment weapon;                                        // The equipped weapon
    public static Equipment armor;                                         // The equipped armor
    public static List<Equipment> charms;                                  // The equipped charms

    public static float inv = 0;
    public static int invState = 0;
    public static GameObject danmaku1;
    public static GameObject circle;
    public static int spell1 = 0;
    public static bool hit = false;

    public AudioSource dead;

    // Update is called once per frame
    public static void Update()
    {
        CalculateStats();
        UpdateInv();
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

    static void UpdateInv()
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

    public void TakeDamage()
    {
        if(inv == 0 && !hit)
        {
            hit = true;
            hp -= 1;
            invState = 1;
            dead.Play();
        }
    }
}
