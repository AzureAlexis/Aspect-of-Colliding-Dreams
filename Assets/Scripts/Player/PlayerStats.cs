using Unity.VisualScripting;
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
    public float hp = 5;

    public float inv = 0;
    public int invState = 0;
    public GameObject danmaku1;
    public GameObject circle;
    public int spell1 = 0;
    public bool hit = false;

    public AudioSource dead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        player = gameObject;
        UpdateInv();
        UIManager.UpdatePlayerHp(hp);
    }

    void UpdateFixed()
    {
        if(inv > 0 && GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 1))
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        else if(inv > 0 && GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 0))
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void UpdateInv()
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
