/* 
    This (mostly) static class contains all bullet patterns in the game. The object
    "PatternManager" only exists to initilize the patterns in this script
*/

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    static List<EnemyPattern> enemyPatterns = new List<EnemyPattern>();
    static List<PlayerPattern> playerPatterns = new List<PlayerPattern>();
    static List<EnemyShotData> enemyShots = new List<EnemyShotData>();
    static List<PlayerShotData> playerShots = new List<PlayerShotData>();
    public static Material enemyBulletRed;
    public static GameObject playerBullet;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PatternManager.LoadMaterials();
        PatternManager.CreateInitialEnemyShots();
        PatternManager.CreateInitialEnemyPatterns();
        PatternManager.CreateInitialPlayerShots();
        PatternManager.CreateInitialPlayerPatterns();
    }

    static void LoadMaterials()
    {
        enemyBulletRed = Resources.Load("enemyBulletRed_0", typeof(Material)) as Material;
        playerBullet = Resources.Load("playerBullet", typeof(GameObject)) as GameObject;
    }

    static void CreateInitialEnemyShots()
    {
        enemyShots.Add(new EnemyShotData());
        enemyShots[0].name = "8 way spread 1";
        for(int i = 0; i < 8; i++)
        {
            enemyShots[0].danmaku.Add(new DanmakuData());
            enemyShots[0].danmaku[i].speed = 4;
            enemyShots[0].danmaku[i].dir = 45 * i;
            enemyShots[0].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[1].name = "8 way spread 2";
        for(int i = 0; i < 8; i++)
        {
            enemyShots[1].danmaku.Add(new DanmakuData());
            enemyShots[1].danmaku[i].speed = 4;
            enemyShots[1].danmaku[i].dir = 45 * i + 22.5f;
            enemyShots[1].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[2].name = "Tight 32 way spread 1";
        for(int i = 0; i < 32; i++)
        {
            enemyShots[2].danmaku.Add(new DanmakuData());
            enemyShots[2].danmaku[i].speed = 1f;
            enemyShots[2].danmaku[i].dir = i * 10 + 180;
            enemyShots[2].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[3].name = "Tight 32 way spread 1";
        for(int i = 0; i < 32; i++)
        {
            enemyShots[3].danmaku.Add(new DanmakuData());
            enemyShots[3].danmaku[i].speed = 1f;
            enemyShots[3].danmaku[i].dir = i * 10 + 240;
            enemyShots[3].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[4].name = "Back Spread 1";
        for(int i = 0; i < 36; i++)
        {
            enemyShots[4].danmaku.Add(new DanmakuData());
            enemyShots[4].danmaku[i].speed = 6;
            enemyShots[4].danmaku[i].dir = i * 5 + - 87.5f;
            enemyShots[4].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[5].name = "Back Spread 2";
        for(int i = 0; i < 36; i++)
        {
            enemyShots[5].danmaku.Add(new DanmakuData());
            enemyShots[5].danmaku[i].speed = 6;
            enemyShots[5].danmaku[i].dir = i * 5 + - 90f;
            enemyShots[5].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[6].name = "Undodgable";
        for(int i = 0; i < 360; i++)
        {
            enemyShots[6].danmaku.Add(new DanmakuData());
            enemyShots[6].danmaku[i].speed = 1;
            enemyShots[6].danmaku[i].dir = i;
            enemyShots[6].danmaku[i].material = enemyBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[7].name = "Random dir";
        enemyShots[7].danmaku.Add(new DanmakuData());
        enemyShots[7].danmaku[0].speed = 4;
        enemyShots[7].danmaku[0].dir = 0;
        enemyShots[7].danmaku[0].dirBehavior = "random";
        enemyShots[7].danmaku[0].material = enemyBulletRed;
    }

    static void CreateInitialEnemyPatterns()
    {
        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[0].name = "Move tutorial";
        enemyPatterns[0].endCondition = "time";
        enemyPatterns[0].endValue = 8;
        enemyPatterns[0].endEvent = "tutorial2";

        enemyPatterns[0].shots.Add(new EnemyShot{
            data = GetEnemyShot(0),
            startTime = 0,
            endTime = 8,
            loopDelay = 2
        });
        enemyPatterns[0].shots.Add(new EnemyShot{
            data = GetEnemyShot(1),
            startTime = 1,
            endTime = 8,
            loopDelay = 2
        });

        //----------------------------

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[1].name = "Focus tutorial";
        enemyPatterns[1].endCondition = "time";
        enemyPatterns[1].endValue = 12;
        enemyPatterns[1].endEvent = "tutorial3";

        enemyPatterns[1].shots.Add(new EnemyShot{
            data = GetEnemyShot(2),
            startTime = 1,
            endTime = 12,
            loopDelay = 2
        });
        enemyPatterns[1].shots.Add(new EnemyShot{
            data = GetEnemyShot(3),
            startTime = 2,
            endTime = 12,
            loopDelay = 2
        });

        //----------------------------

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[2].name = "Shoot tutorial";
        enemyPatterns[2].endCondition = "hp";
        enemyPatterns[2].endValue = 950;
        enemyPatterns[2].endEvent = "tutorial4";

        enemyPatterns[2].shots.Add(new EnemyShot{
            data = GetEnemyShot(4),
            startTime = 0.1f,
            endTime = 99,
            loopDelay = 0.2f
        });
        enemyPatterns[2].shots.Add(new EnemyShot{
            data = GetEnemyShot(5),
            startTime = 0.2f,
            endTime = 99,
            loopDelay = 0.2f
        });

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[3].name = "Spell tutorial";
        enemyPatterns[3].endCondition = "time";
        enemyPatterns[3].endValue = 6;
        enemyPatterns[3].endEvent = "tutorial5";

        enemyPatterns[3].shots.Add(new EnemyShot{
            data = GetEnemyShot(6),
            startTime = 0.1f,
            endTime = 1,
            loopDelay = 6f
        });

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[4].name = "Item tutorial";
        enemyPatterns[4].endCondition = "time";
        enemyPatterns[4].endValue = 8;
        enemyPatterns[4].endEvent = "tutorial6";

        enemyPatterns[4].shots.Add(new EnemyShot{
            data = GetEnemyShot(7),
            startTime = 0.1f,
            endTime = 8,
            loopDelay = 0.05f
        });
    }

    static void CreateInitialPlayerShots()
    {
        playerShots.Add(new PlayerShotData());
        for(int i = 0; i < 2; i++)
        {
            playerShots[0].danmaku.Add(new DanmakuData());
            playerShots[0].danmaku[i].speed = 20;
            playerShots[0].danmaku[i].player = true;
            playerShots[0].danmaku[i].complex = true;

            playerShots[0].danmaku[i].posMod = "sinX";
            playerShots[0].danmaku[i].posBehavior = "normalMod";

            playerShots[0].danmaku[i].dir = 90;

            playerShots[0].danmaku[i].prefab = playerBullet;
        }
        playerShots[0].danmaku[0].position = new Vector2(2, 0);
        playerShots[0].danmaku[1].position = new Vector2(-2, 0);
    }

    static void CreateInitialPlayerPatterns()
    {
        playerPatterns.Add(new PlayerPattern());
        playerPatterns[0].name = "Move tutorial";
        playerPatterns[0].endTime = 8;

        playerPatterns[0].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(0),
            startTime = 0,
            endTime = 2.375f,
            loopDelay = 0.02f
        });
    }

    public static EnemyPattern GetEnemyPattern(int id)
    {
        return enemyPatterns[id];
    }

    public static EnemyShotData GetEnemyShot(int id)
    {
        return enemyShots[id];
    }

    public static PlayerShotData GetPlayerShot(int id)
    {
        return playerShots[id];
    }

    public static PlayerPattern GetPlayerPattern(int id)
    {
        return playerPatterns[id];
    }


}