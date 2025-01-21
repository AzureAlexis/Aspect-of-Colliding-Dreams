using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static GameObject player;
    public float power = 0;         // Determines how much a shot's power is multiplied by when calculating damage
    public float magic = 0;         // Determines how much a spell's power is multiplied by when calculating damage. Also increases AP by 1 per point
    public float durability = 0;    // Determines how much HP the player has. Increases HP by 1 per point
    public float evasion = 0;       // Determines how big the player's hitbox is. Decreases hitbox size by 1% per point (past base)
    public float speed = 20;        // Determines how fast the player moves. Increases speed by 0.4 units/sec per point
    public float charge = 20;       // Determines how much temp HP/AP is gained by grazing. Increases gain by 1% per point
    public float hp = 10;
    public GameObject danmaku1;
    public int spell1 = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        player = gameObject;
    }

    public void TakeDamage()
    {
        hp -= 1;
    }
}
