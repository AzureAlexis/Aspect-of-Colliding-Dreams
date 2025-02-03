using UnityEngine;

public class ItemBase : BattleSlotBase
{
    public int count;       // How many of these the player owns
    public Sprite sprite;   // What sprite to use to render this in the UI
    public string type;     // What type of item this is
    public string name;
    public string flavor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
