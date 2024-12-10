using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int patternId = 0;
    private Pattern pattern;
    GameObject player;
    GameObject bulletBase;
    float time;
    bool active = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        bulletBase = Resources.Load("EnemyBulletBase") as GameObject;
        pattern = PatternManager.GetPattern(patternId);
    }

    // Update is called once per frame
    void Update()
    {
        if(pattern == null)
        {
            pattern = PatternManager.GetPattern(patternId);
        }
        if(IsActive())
        {
            time += Time.deltaTime;
            FireShotsReady();
            if(time >= pattern.length)
            {
                time = 0;
            }
        }
    }

    public bool IsActive()
    {
        return active;
    }

    void FireShotsReady()
    {
        for(int i = 0; i < pattern.shots.Count; i++)
        {
            float shotTime = pattern.shotTimes[i];
            if(time - Time.deltaTime < shotTime && time > shotTime)
            {
                DanmakuManager.Fire(pattern.shots[i].bullets);
            }
        }
    }
}
