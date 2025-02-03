using UnityEngine;

public class Consumable : ItemBase
{
    // Effects
    public string effect;                   // What this item does when used
    public float value;                     // Magnitude of the effect
    public int limit;                       // How many of these can be equipped in one slot
    public int uses;                        // How many times this has been used this battle


    // Meta-information
    public new string type = "Resource";    // What type of item this is. Resource in this case
}
