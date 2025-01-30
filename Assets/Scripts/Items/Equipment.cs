using UnityEngine;

public class Equipment : ItemBase
{
    // Stuff that the equipment gives while equipped
    public int power = 0;
    public int magic = 0;
    public int stamina = 0;
    public int speed = 0;
    public int evasion = 0;
    public int charge = 0;
    public string ability;              // What ability this grants

    // Meta-information
    public new string type = "Equip";   // What type of item this is. Equipment in this case
    public int slot;                    // What slot the item is equiped in. 0 = Charm, 1 = Weapon, 2 = Broom
    public int cost;                    // How much AP is required to eqiuip this. Only matters if this is a charm
}
