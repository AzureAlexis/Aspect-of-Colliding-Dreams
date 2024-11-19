using UnityEngine;

public class enemyHP : MonoBehaviour
{
    public int hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int damageAmount)
    {
        hp -= damageAmount;
    }
}
