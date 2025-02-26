using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private static List<PlayerAttack> attackData = new List<PlayerAttack>();
    private static List<PlayerSpell> spellData = new List<PlayerSpell>();
    private static List<Equipment> equipmentData = new List<Equipment>();
    private static List<Consumable> consumableData = new List<Consumable>();
    private static List<Resource> resourceData = new List<Resource>();

    void Start()
    {
        ItemManager.BuildDatabase();
    }

    private static void BuildDatabase()
    {
        BuildAttack();
        BuildSpell();
        BuildResource();
        BuildEquipment();
        BuildConsumable();
    }

    private static void BuildAttack()
    {
        attackData.Add(new PlayerAttack(){
            name = "Sonic Wave",
            publicPower = "D+",
            publicSpeed = "C+",
            publicRange = "D+",
            publicAccu = "B-",
            publicCost = "B",

            flavor = "I started off with this shot when I was just learning. It isn't anything too amazing, but it's very well rounded and efficent to use"
        });

        attackData.Add(new PlayerAttack(){
            name = "Magic Missile",
            publicPower = "B",
            publicSpeed = "D+",
            publicRange = "D",
            publicAccu = "D-",
            publicCost = "C-",

            flavor = "This one's my favorite! These missiles are hard to land and pretty costly to use. But when they do, my target goes BOOM!"
        });

        attackData.Add(new PlayerAttack(){
            name = "Riftbinder",
            publicPower = "D",
            publicSpeed = "C",
            publicRange = "A-",
            publicAccu = "A-",
            publicCost = "C+",

            flavor = "When I fire these, they home in on their target after a short delay. This shot isn't very powerful, but makes up for it with it's great accuracy and range. Great if I'm up against something dangerous and need to prioritizing survival."
        });
    }

    private static void BuildSpell()
    {
        spellData.Add(new PlayerSpell(){
            name = "Rising Inferno",
            publicPower = "B+",
            publicSpeed = "B",
            publicRange = "A",
            publicAccu = "A-",
            publicCost = "C-",

            flavor = "A huge flame that covers the whole field. Hits everything and anything"
        });
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
        consumableData.Add(new Consumable(){
            name = "Life Stone",
            effect = "hp",
            value = 0.3f,
            limit = 2,
            flavor = "Restores a lot of health. These are pretty rare, so I shouldn't waste them. That makes me think, where did this life come from? Maybe I shouldn't think about it too much."
        });

        consumableData.Add(new Consumable(){
            name = "Anima Dust",
            effect = "ap",
            value = 0.2f,
            limit = 8,
            flavor = "Restores a little anima. Almost everyone stores a little bit of anima, but it's really hard to manifest it physically."
        });

        consumableData.Add(new Consumable(){
            name = "Anima Shard",
            effect = "ap",
            value = 0.4f,
            limit = 6,
            flavor = "Restores some anima. Even when I crush this, I can't identify where this anima came from. How is there a type of amina I don't know?"
        });

        consumableData.Add(new Consumable(){
            name = "Anima Crystal",
            effect = "ap",
            value = 0.8f,
            limit = 4,
            flavor = "Restores a lot of anima. I know that amina is formed from the borders of concepts, so this much condensed amina has to come from a huge, base idea."
        });
    }

    private static void BuildResource()
    {
        resourceData.Add(new Resource(){
            name = "Spark",
            flavor = "An almost universal currency that nearly everyone accepts. I can never have too much!"
        });

        resourceData.Add(new Resource(){
            name = "Smoldering Cinders",
            flavor = "A weak material aquired by disenchanting fire-attunned items. Can be used to upgrade a lot of things"
        });

        resourceData.Add(new Resource(){
            name = "Silky Sand",
            flavor = "A weak material aquired by disenchanting water-attunned items. Can be used to upgrade a lot of things"
        });

        resourceData.Add(new Resource(){
            name = "Shaved Bark",
            flavor = "A weak material aquired by disenchanting wood-attunned items. Can be used to upgrade a lot of things"
        });

        resourceData.Add(new Resource(){
            name = "Iron Dust",
            flavor = "A weak material aquired by disenchanting metal-attunned items. Can be used to upgrade a lot of things"
        });

        resourceData.Add(new Resource(){
            name = "Crumbled Stone",
            flavor = "A weak material aquired by disenchanting earth-attunned items. Can be used to upgrade a lot of things"
        });
    }

    public static BattleSlotBase GetItemByName(string name)
    {
        for(int i = 0; i < consumableData.Count; i++)
        {
            if(consumableData[i].name == name)
                return consumableData[i];
        }
        for(int i = 0; i < equipmentData.Count; i++)
        {
            if(equipmentData[i].name == name)
                return equipmentData[i];
        }
        for(int i = 0; i < resourceData.Count; i++)
        {
            if(resourceData[i].name == name)
                return resourceData[i];
        }
        for(int i = 0; i < attackData.Count; i++)
        {
            if(attackData[i].name == name)
                return attackData[i];
        }
        for(int i = 0; i < spellData.Count; i++)
        {
            if(spellData[i].name == name)
                return spellData[i];
        }
        return null;
    }
    public static string GetAbilityDesc(string ability)
    {
        switch(ability)
        {
            case "Faithguard":
                return "Once per battle, if you would take lethal damage, survive with 1 HP instead. Multiequip to increase uses per battle";

            case "Resolute":
                return "Increases your power signifigantly while fighting bosses. Multiequip to further increase power gain";

            case "Phantasm":
                return "Reduce all stats to what they were at level 1. Also grants Faithguard and Resolute. WARNING: This makes combat extremely diffcult, and is only reccomended for very experienced players";

            default:
                return "No Description Found";
        }
    }
}
