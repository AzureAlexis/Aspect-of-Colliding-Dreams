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
                Fire(pattern.shots[i].bullets);
            }
        }
    }

    void Fire(List<RawBulletData> bullets)
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            GameObject bullet = Instantiate(bulletBase, GameObject.Find("Bullets").transform);
            switch (bullets[i].posBehavior)
            {
                case "enemy":
                    bullet.transform.position = transform.position;
                    break;

                case "enemyMod":
                    Vector3 position = new Vector3(0, 0, 0);
                    position.x = transform.position.x + bullets[i].x;
                    position.y = transform.position.y + bullets[i].y;
                    bullet.transform.position = position;
                    break;
            }
            switch (bullets[i].dirBehavior)
            {
                case "toPlayer":
                    bullet.transform.LookAt(player.transform);
                    break;

                case "normal":
                    bullet.transform.rotation = Quaternion.Euler(0, 0, bullets[i].dir);
                    break;
            }
            bullet.GetComponent<EnemyBullet>().speed = bullets[i].speed;
            bullet.GetComponent<EnemyBullet>().acc = bullets[i].acc;
            bullet.GetComponent<EnemyBullet>().movements = bullets[i].movements;
            bullet.GetComponent<EnemyBullet>().posBehavior = bullets[i].posBehavior;
            bullet.GetComponent<EnemyBullet>().dirBehavior = bullets[i].dirBehavior;
            bullet.GetComponent<EnemyBullet>().accBehavior = bullets[i].accBehavior;

        }
    }
}
