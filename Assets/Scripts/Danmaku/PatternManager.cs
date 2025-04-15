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
    public static Material smallBulletRed;
    public static Material bigBulletRed;
    public static GameObject playerBullet;

    public AudioSource smallAttack;
    public AudioSource bigAttack;
    public AudioSource strangeAttack;

    public static AudioSource small;
    public static AudioSource big;
    public static AudioSource strange;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        PatternManager.small = smallAttack;
        PatternManager.big = bigAttack;
        PatternManager.strange = strangeAttack;

        PatternManager.LoadMaterials();
        PatternManager.CreateInitialEnemyShots();
        PatternManager.CreateInitialEnemyPatterns();
        PatternManager.CreateInitialPlayerShots();
        PatternManager.CreateInitialPlayerPatterns();
    }

    static void LoadMaterials()
    {
        smallBulletRed = Resources.Load("bullets/smallBulletRed", typeof(Material)) as Material;
        bigBulletRed = Resources.Load("bullets/bigBulletRed", typeof(Material)) as Material;
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
            enemyShots[0].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[1].name = "8 way spread 2";
        for(int i = 0; i < 8; i++)
        {
            enemyShots[1].danmaku.Add(new DanmakuData());
            enemyShots[1].danmaku[i].speed = 4;
            enemyShots[1].danmaku[i].dir = 45 * i + 22.5f;
            enemyShots[1].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[2].name = "Tight 32 way spread 1";
        for(int i = 0; i < 32; i++)
        {
            enemyShots[2].danmaku.Add(new DanmakuData());
            enemyShots[2].danmaku[i].speed = 1f;
            enemyShots[2].danmaku[i].dir = i * 10 + 180;
            enemyShots[2].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[3].name = "Tight 32 way spread 1";
        for(int i = 0; i < 32; i++)
        {
            enemyShots[3].danmaku.Add(new DanmakuData());
            enemyShots[3].danmaku[i].speed = 1f;
            enemyShots[3].danmaku[i].dir = i * 10 + 240;
            enemyShots[3].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[4].name = "Back Spread 1";
        for(int i = 0; i < 36; i++)
        {
            enemyShots[4].danmaku.Add(new DanmakuData());
            enemyShots[4].danmaku[i].speed = 6;
            enemyShots[4].danmaku[i].dir = i * 5 + - 87.5f;
            enemyShots[4].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[5].name = "Back Spread 2";
        for(int i = 0; i < 36; i++)
        {
            enemyShots[5].danmaku.Add(new DanmakuData());
            enemyShots[5].danmaku[i].speed = 6;
            enemyShots[5].danmaku[i].dir = i * 5 + - 90f;
            enemyShots[5].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[6].name = "Undodgable";
        for(int i = 0; i < 360; i++)
        {
            enemyShots[6].danmaku.Add(new DanmakuData());
            enemyShots[6].danmaku[i].speed = 1;
            enemyShots[6].danmaku[i].dir = i;
            enemyShots[6].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[7].name = "Random dir";
        enemyShots[7].danmaku.Add(new DanmakuData());
        enemyShots[7].danmaku[0].speed = 4;
        enemyShots[7].danmaku[0].dir = 0;
        enemyShots[7].danmaku[0].dirBehavior = "random";
        enemyShots[7].danmaku[0].material = smallBulletRed;

        enemyShots.Add(new EnemyShotData());
        enemyShots[8].name = "Dormey nonspell pt1";
        for(int i = 0; i < 32; i++)
        {
            enemyShots[8].danmaku.Add(new DanmakuData());
            enemyShots[8].danmaku[i].speed = 4;
            enemyShots[8].danmaku[i].dir = i * 11.25f;
            enemyShots[8].danmaku[i].material = smallBulletRed;
        }
        for(int i = 32; i < 64; i++)
        {
            enemyShots[8].danmaku.Add(new DanmakuData());
            enemyShots[8].danmaku[i].speed = 2;
            enemyShots[8].danmaku[i].dir = i * 11.25f;
            enemyShots[8].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[9].name = "Dormey nonspell pt2";
        for(int i = 0; i < 16; i++)
        {
            enemyShots[9].danmaku.Add(new DanmakuData());
            enemyShots[9].danmaku[i].speed = 3;
            enemyShots[9].danmaku[i].dir = i * 22.5f;
            enemyShots[9].danmaku[i].material = bigBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[10].name = "Dormey nonspell pt3";
        for(int i = 0; i < 32; i++)
        {
            enemyShots[10].danmaku.Add(new DanmakuData());
            enemyShots[10].danmaku[i].speed = 4;
            enemyShots[10].danmaku[i].dir = i * 11.25f + 5.625f;
            enemyShots[10].danmaku[i].material = smallBulletRed;
        }
        for(int i = 32; i < 64; i++)
        {
            enemyShots[10].danmaku.Add(new DanmakuData());
            enemyShots[10].danmaku[i].speed = 2;
            enemyShots[10].danmaku[i].dir = i * 11.25f + 5.625f;
            enemyShots[10].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[11].name = "Dormey nonspell pt4";
        for(int i = 0; i < 16; i++)
        {
            enemyShots[11].danmaku.Add(new DanmakuData());
            enemyShots[11].danmaku[i].speed = 3;
            enemyShots[11].danmaku[i].dir = i * 22.5f + 11.25f;
            enemyShots[11].danmaku[i].material = bigBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[12].name = "Geyser Eruption pt1";
        for(int i = 0; i < 16; i++)
        {
            enemyShots[12].danmaku.Add(new DanmakuData());
            enemyShots[12].danmaku[i].speed = 5;
            enemyShots[12].danmaku[i].dir = i * 22.5f;
            enemyShots[12].danmaku[i].dirBehavior = "player";
            enemyShots[12].danmaku[i].material = bigBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[13].name = "Geyser Eruption pt2";
        for(int i = 0; i < 8; i++)
        {
            enemyShots[13].danmaku.Add(new DanmakuData());
            enemyShots[13].danmaku[i].speed = 3;
            enemyShots[13].danmaku[i].dirBehavior = "random";
            enemyShots[13].danmaku[i].dirAcc = 45;
            enemyShots[13].danmaku[i].material = bigBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[14].name = "Dormey nonspell 2 pt1";
        for(int i = 0; i < 24; i++)
        {
            enemyShots[14].danmaku.Add(new DanmakuData());
            enemyShots[14].danmaku[i].speed = 5;
            enemyShots[14].danmaku[i].dir = i * 15f;
            enemyShots[14].danmaku[i].dirAcc = 20;
            enemyShots[14].danmaku[i].dirBehavior = "player";
            enemyShots[14].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[15].name = "Dormey nonspell 2 pt2";
        for(int i = 0; i < 24; i++)
        {
            enemyShots[15].danmaku.Add(new DanmakuData());
            enemyShots[15].danmaku[i].speed = 5;
            enemyShots[15].danmaku[i].dir = i * 15f;
            enemyShots[15].danmaku[i].dirAcc = -20;
            enemyShots[15].danmaku[i].dirBehavior = "player";
            enemyShots[15].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[16].name = "Dormey nonspell 2 pt3";
        for(int i = 0; i < 24; i++)
        {
            enemyShots[16].danmaku.Add(new DanmakuData());
            enemyShots[16].danmaku[i].speed = 2;
            enemyShots[16].danmaku[i].dir = i * 15f;
            enemyShots[16].danmaku[i].dirAcc = -20f;
            enemyShots[16].danmaku[i].dirBehavior = "player";
            enemyShots[16].danmaku[i].material = bigBulletRed;
        }
        for(int i = 24; i < 48; i++)
        {
            enemyShots[16].danmaku.Add(new DanmakuData());
            enemyShots[16].danmaku[i].speed = 2;
            enemyShots[16].danmaku[i].dir = (i-24) * 15f;
            enemyShots[16].danmaku[i].dirAcc = 20f;
            enemyShots[16].danmaku[i].dirBehavior = "player";
            enemyShots[16].danmaku[i].material = bigBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[17].name = "Patient bravery pt1";
        for(int i = 0; i < 48; i++)
        {
            enemyShots[17].danmaku.Add(new DanmakuData());
            enemyShots[17].danmaku[i].speed = 1;
            enemyShots[17].danmaku[i].dir = i * 7.5f;
            enemyShots[17].danmaku[i].dirBehavior = "player";
            enemyShots[17].danmaku[i].material = smallBulletRed;
        }

        enemyShots.Add(new EnemyShotData());
        enemyShots[18].name = "Patient bravery pt2";
        enemyShots[18].danmaku.Add(new DanmakuData());
        enemyShots[18].danmaku[0].speed = 4;
        enemyShots[18].danmaku[0].dirBehavior = "random";
        enemyShots[18].danmaku[0].material = bigBulletRed;
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
            sound = small,
            loopDelay = 2
        });
        enemyPatterns[0].shots.Add(new EnemyShot{
            data = GetEnemyShot(1),
            startTime = 1,
            endTime = 8,
            sound = small,
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
            loopDelay = 2,
            sound = strange
        });
        enemyPatterns[1].shots.Add(new EnemyShot{
            data = GetEnemyShot(3),
            startTime = 2,
            endTime = 12,
            loopDelay = 2,
            sound = strange
        });

        //----------------------------

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[2].name = "Shoot tutorial";
        enemyPatterns[2].endCondition = "hp";
        enemyPatterns[2].endValue = 850;
        enemyPatterns[2].endEvent = "tutorial4";

        enemyPatterns[2].shots.Add(new EnemyShot{
            data = GetEnemyShot(4),
            startTime = 0.1f,
            endTime = 99,
            loopDelay = 0.2f,
            sound = small
        });
        enemyPatterns[2].shots.Add(new EnemyShot{
            data = GetEnemyShot(5),
            startTime = 0.2f,
            endTime = 99,
            loopDelay = 0.2f
        });
        enemyPatterns[2].shots.Add(new EnemyShot{
            movement = "toPlayer",
            startTime = 1f,
            endTime = 99,
            loopDelay = 1,
        });

        //----------------------------

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[3].name = "Spell tutorial";
        enemyPatterns[3].endCondition = "time";
        enemyPatterns[3].endValue = 6;
        enemyPatterns[3].endEvent = "tutorial5";

        enemyPatterns[3].shots.Add(new EnemyShot{
            data = GetEnemyShot(6),
            startTime = 0.1f,
            endTime = 1,
            loopDelay = 6f,
            sound = small
        });

        //----------------------------

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
        enemyPatterns[4].shots.Add(new EnemyShot{
            movement = "toPlayer",
            startTime = 1f,
            endTime = 99,
            loopDelay = 1,
        });

        //----------------------------

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[5].name = "Dormey nonspell 1";
        enemyPatterns[5].background = 1;
        enemyPatterns[5].endCondition = "hp";
        enemyPatterns[5].endValue = 700;

        enemyPatterns[5].shots.Add(new EnemyShot{
            data = GetEnemyShot(8),
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1,
            sound = small
        });
        enemyPatterns[5].shots.Add(new EnemyShot{
            data = GetEnemyShot(9),
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1
        });
        enemyPatterns[5].shots.Add(new EnemyShot{
            data = GetEnemyShot(10),
            startTime = 1,
            endTime = 99,
            loopDelay = 1
        });
        enemyPatterns[5].shots.Add(new EnemyShot{
            data = GetEnemyShot(11),
            startTime = 1,
            endTime = 99,
            loopDelay = 1,
            sound = big
        });
        enemyPatterns[5].shots.Add(new EnemyShot{
            movement = "toPlayer",
            startTime = 1,
            endTime = 99,
            loopDelay = 1,
        });

        //----------------------------

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[6].name = "Earth Wood Sign: Geyser Eruption";
        enemyPatterns[6].background = 2;
        enemyPatterns[6].endCondition = "hp";
        enemyPatterns[6].endValue = 550;
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 1,
            endTime = 99,
            loopDelay = 6,
            sound = big
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 1.125f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 1.25f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 2,
            endTime = 99,
            loopDelay = 6,
            sound = big
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 2.125f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 2.25f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 3,
            endTime = 99,
            loopDelay = 6,
            sound = big
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 3.125f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 3.25f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            movement = "geyser",
            startTime = 3.5f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(13),
            startTime = 5f,
            endTime = 99,
            loopDelay = 6,
            sound = strange
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(8),
            startTime = 5.25f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(13),
            startTime = 5.5f,
            endTime = 99,
            loopDelay = 6,
            sound = small
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(10),
            startTime = 5.75f,
            endTime = 99,
            loopDelay = 6
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            data = GetEnemyShot(13),
            startTime = 6f,
            endTime = 99,
            loopDelay = 6,
            sound = small
        });
        enemyPatterns[6].shots.Add(new EnemyShot{
            movement = "return",
            startTime = 5f,
            endTime = 99,
            loopDelay = 6
        });

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[7].name = "Dormey nonspell 2";
        enemyPatterns[7].background = 1;
        enemyPatterns[7].endCondition = "hp";
        enemyPatterns[7].endValue = 400;
        enemyPatterns[7].endEvent = "turn8";
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(15),
            startTime = 1,
            endTime = 99,
            loopDelay = 3,
            sound = small
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(14),
            startTime = 1.25f,
            endTime = 99,
            loopDelay = 3,
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(15),
            startTime = 1.5f,
            endTime = 99,
            loopDelay = 3,
            sound = small
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(14),
            startTime = 1.75f,
            endTime = 99,
            loopDelay = 3,
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(15),
            startTime = 2,
            endTime = 99,
            loopDelay = 3,
            sound = small
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(14),
            startTime = 2.25f,
            endTime = 99,
            loopDelay = 3,
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(16),
            startTime = 5.5f,
            endTime = 99,
            loopDelay = 3,
            sound = big
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            data = GetEnemyShot(16),
            startTime = 13f,
            endTime = 99,
            loopDelay = 3,
            sound = big
        });
        enemyPatterns[7].shots.Add(new EnemyShot{
            movement = "toPlayer",
            startTime = 1f,
            endTime = 99,
            loopDelay = 1,
        });

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[8].name = "The Nightmare One Has on their Last Sunday";
        enemyPatterns[8].background = 4;
        enemyPatterns[8].shots.Add(new EnemyShot{
            data = GetEnemyShot(17),
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1f,
            sound = small
        });
        enemyPatterns[8].shots.Add(new EnemyShot{
            data = GetEnemyShot(7),
            startTime = 5f,
            endTime = 99,
            loopDelay = 0.2f,
        });
        enemyPatterns[8].shots.Add(new EnemyShot{
            data = GetEnemyShot(18),
            startTime = 10.1f,
            endTime = 99,
            loopDelay = 0.2f,
        });
        enemyPatterns[8].shots.Add(new EnemyShot{
            data = GetEnemyShot(17),
            startTime = 15f,
            endTime = 99,
            loopDelay = 1f,
            sound = big
        });
        enemyPatterns[8].shots.Add(new EnemyShot{
            data = GetEnemyShot(12),
            startTime = 20f,
            endTime = 99,
            loopDelay = 1f,
            sound = strange
        });
        enemyPatterns[8].shots.Add(new EnemyShot{
            movement = "toPlayer",
            startTime = 1f,
            endTime = 99,
            loopDelay = 1,
        });
    }

    static void CreateInitialPlayerShots()
    {
        /*
            Shots for Sonic Wave (lv1)
        */ 
        playerShots.Add(new PlayerShotData()); // Sonic wave pt1
        for(int i = 0; i < 2; i++)
        {
            playerShots[0].danmaku.Add(new DanmakuData());
            playerShots[0].danmaku[i].speed = 20;
            playerShots[0].danmaku[i].player = true;

            playerShots[0].danmaku[i].dir = 90;
            playerShots[0].danmaku[i].position = new Vector2(-0.5f + i, 0);
            playerShots[0].danmaku[i].posBehavior = "normalMod";

            playerShots[0].danmaku[i].prefab = playerBullet;
        }
        playerShots.Add(new PlayerShotData()); // Sonic wave pt2
        for(int i = 0; i < 2; i++)
        {
            playerShots[1].danmaku.Add(new DanmakuData());
            playerShots[1].danmaku[i].speed = 20;
            playerShots[1].danmaku[i].player = true;

            playerShots[1].danmaku[i].dir = 60 * (i + 1);

            playerShots[1].danmaku[i].prefab = playerBullet;
        }
        playerShots.Add(new PlayerShotData()); // Sonic wave pt3
        for(int i = 0; i < 2; i++)
        {
            playerShots[2].danmaku.Add(new DanmakuData());
            playerShots[2].danmaku[i].speed = 20;
            playerShots[2].danmaku[i].player = true;

            playerShots[2].danmaku[i].dir = 85 + (i * 10);
            playerShots[2].danmaku[i].position = new Vector2(-0.5f + i, 0);
            playerShots[2].danmaku[i].posBehavior = "normalMod";

            playerShots[2].danmaku[i].prefab = playerBullet;
        }
        playerShots.Add(new PlayerShotData()); // Sonic wave pt4
        for(int i = 0; i < 2; i++)
        {
            playerShots[3].danmaku.Add(new DanmakuData());
            playerShots[3].danmaku[i].speed = 20;
            playerShots[3].danmaku[i].player = true;

            playerShots[3].danmaku[i].dir = 75 + (i * 30);
            playerShots[3].danmaku[i].posBehavior = "normal";

            playerShots[3].danmaku[i].prefab = playerBullet;
        }

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