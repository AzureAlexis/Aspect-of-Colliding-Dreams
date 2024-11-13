using UnityEngine;

public class playerShoot : MonoBehaviour
{
    List<float> cooldowns = new List<float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActive())
        {
            if(Input.GetKey(KeyCode.Z) && cooldowns[0] )
            {
                Fire(playerStats.bullet1, 0);
            }
        }

        UpdateCooldowns();
    }

    void UpdateCooldowns()
    {
        foreach(float cooldown in cooldowns)
        {
            cooldown = Mathf.Max(cooldown - Time.deltaTime, 0);
        }
    }

    void Fire(GameObject prefab, int slot)
    {
        PlayerBulletStats bulletStats = bullet.GetComponent<PlayerBulletStats>();
        GameObject bullet = Instantiate(prefab, transform.position, transfor)

        cooldown[slot] == bulletStats.cooldown;
    }
    bool IsActive()
    {
        return true;
    }
}
