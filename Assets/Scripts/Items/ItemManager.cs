using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static List<Equipment> equipmentData = new List<Equipment>();
    private static List<Consumable> consumableData = new List<Consumable>();
    private static List<Resource> resourceData = new List<Resource>();

    void Start()
    {
        ItemManager.BuildDatabase();
    }

    private static void BuildDatabase()
    {
        BuildEquipment();
        BuildConsumable();
        BuildResource();
    }

    private static void BuildEquipment()
    {
        equipmentData.Add(new Equipment(){
            power = 3,
            magic = 1,
            slot = 1,

            name = "Star Furnace",
            flavor = "The focus that I started with. It doesn't seem very powerful at first glance, but it's helped me a lot before."
        });

        equipmentData.Add(new Equipment(){
            magic = 2,
            stamina = 3,
            slot = 2,

            name = "Dusty Broom",
            flavor = "The magic broom that I started with. I know it's a sterotype that witches ride on brooms, but I like them! They get me around without much effort and they're very cheap to replace when I inevidably destroy mine."
        });

        equipmentData.Add(new Equipment(){
            power = 1,
            magic = 1,
            stamina = 1,
            slot = 0,
            cost = 1,
            ability = "Faithguard",

            name = "Heart Charm",
            flavor = "A good luck charm that Mikara gave me a long time ago. It's really important to me, but it'd be so embarressing if she found out I care so much!"
        });

        equipmentData.Add(new Equipment(){
            power = 1,
            magic = 1,
            stamina = 1,
            slot = 0,
            cost = 1,
            ability = "Resolute",

            name = "Rift Charm",
            flavor = "Somehow, I don't remember where I got this. It keeps me determened to move foward, though"
        });

        equipmentData.Add(new Equipment(){
            slot = 0,
            cost = 0,
            ability = "Phantasm",

            name = "Phantasmal Shard",
            flavor = "Something that looks like a glass shard I got from the antique shop a long time ago. When I look through it, I can faintly see a place I've never been to before"
        });
    }

    private static void BuildConsumable()
    {
        
    }

    private static void BuildResource()
    {
        resourceData.Add(new Resource(){
            name = "Spark",
            flavor = "An almost universal currency that nearly everyone accepts. I can never have too much!"
        });
    }

    public static string GetAbilityDesc(string ability)
    {
        switch(ability)
        {
            case "Faithguard":
                return "If you would take lethal damage, survive with 1 HP instead.";

            case "Resolute":
                return "Increases your power signifigantly while fighting bosses";

            case "Phantasm":
                return "Reduce all stats to what they were at level 1. Also grants Faithguard and Resolute. WARNING: This makes combat extremely diffcult, and is only reccomended for very experienced players";

            default:
                return "No Description Found";
        }
    }
}
