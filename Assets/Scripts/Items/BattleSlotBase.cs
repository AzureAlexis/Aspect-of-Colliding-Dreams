using UnityEngine;

public class BattleSlotBase
{
    // What stats to display. Only relavent for equipment
    public string publicPower;
    public string publicSpeed;
    public string publicRange;
    public string publicAccu;
    public string publicCost;

    public int count;       // How many of these the player owns
    public float cost;      // How much this costs to use (/sec)
    public Sprite sprite;   // What sprite to use to render this in the UI
    public string type;     // What type of item this is
    public string name;
    public string flavor;
    public string flavorShort;
    public int limit;                       // How many of these can be equipped in one slot

    public PlayerPattern pattern = new PlayerPattern();
    public float time = 0;
    public float length;
}
