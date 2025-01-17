using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    List<float> cooldowns = new List<float>();
    PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldowns.Add(0);
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.IsActive())
        {
            if(Input.GetKey(KeyCode.Z) && cooldowns[0] <= 0)
            {
                Fire(GetComponent<PlayerStats>().danmaku1, 0);
            }
        }

        UpdateCooldowns();
    }

    void UpdateCooldowns()
    {
        for(int i = 0; i < cooldowns.Count; i++)
        {
            cooldowns[i] = Mathf.Max(cooldowns[i] - Time.deltaTime, 0);
        }
    }

    void Fire(GameObject prefab, int slot)
    {
        PlayerDanmakuStats danmakuStats = prefab.GetComponent<PlayerDanmakuStats>();
        GameObject danmaku = Instantiate(prefab, transform.position, prefab.transform.rotation, GameObject.Find("DanmakuManager").transform);

        cooldowns[slot] = danmakuStats.cooldown;
    }
    
    bool IsActive()
    {
        return true;
    }
}
