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
    public static Material smallBulletYel;
    public static GameObject lightning;
    public static Material bigBulletRed;

    public static GameObject sonicWave;
    public static GameObject magicMissile;
    public static GameObject riftbinder;

    public AudioSource smallAttack;
    public AudioSource bigAttack;
    public AudioSource strangeAttack;

    public static AudioSource small;
    public static AudioSource big;
    public static AudioSource strange;

    static bool built = false;

    void Start()
    {
        if(!PatternManager.built)
            BuildDatabase();
    }
    private static void BuildDatabase()
    {
        built = true;
        PatternManager.LoadMaterials();
        PatternManager.CreateInitialEnemyShots();
        PatternManager.CreateInitialEnemyPatterns();
        PatternManager.CreateInitialPlayerShots();
        PatternManager.CreateInitialPlayerPatterns();
    }

    static void LoadMaterials()
    {
        smallBulletRed = Resources.Load("bullets/smallBulletRed", typeof(Material)) as Material;
        smallBulletYel = Resources.Load("bullets/smallBulletYel", typeof(Material)) as Material;
        bigBulletRed = Resources.Load("bullets/bigBulletRed", typeof(Material)) as Material;

        lightning = Resources.Load("bullets/lightning", typeof(GameObject)) as GameObject;
        sonicWave = Resources.Load("bullets/sonicWave", typeof(GameObject)) as GameObject;
        magicMissile = Resources.Load("bullets/magicMissile", typeof(GameObject)) as GameObject;
        riftbinder = Resources.Load("bullets/riftbinder", typeof(GameObject)) as GameObject;

    }

    static void CreateInitialEnemyShots()
    {
        #region Tutorial 1
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
        #endregion
        #region Tutorial 2
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
        #endregion
        #region Tutorial 3
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
        #endregion
        #region Tutorial 4
        enemyShots.Add(new EnemyShotData());
        enemyShots[6].name = "Undodgable";
        for(int i = 0; i < 360; i++)
        {
            enemyShots[6].danmaku.Add(new DanmakuData());
            enemyShots[6].danmaku[i].speed = 1;
            enemyShots[6].danmaku[i].dir = i;
            enemyShots[6].danmaku[i].material = smallBulletRed;
        }
        #endregion
        #region Tutorial 5
        enemyShots.Add(new EnemyShotData());
        enemyShots[7].name = "Random dir";
        enemyShots[7].danmaku.Add(new DanmakuData());
        enemyShots[7].danmaku[0].speed = 4;
        enemyShots[7].danmaku[0].dir = 0;
        enemyShots[7].danmaku[0].dirBehavior = "random";
        enemyShots[7].danmaku[0].material = smallBulletRed;
        #endregion
        #region Doremy Nonspell 1
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
        #endregion
        #region Geyser Eruption
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
        #endregion
        #region Doremy Nonspell 2
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
        #endregion
        #region Patient Bravery
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
        #endregion
        #region Nozomi Nonspell 1
        enemyShots.Add(new EnemyShotData());
        for(int i = 0; i < 100; i+=11)
        {
            enemyShots[19].danmaku.Add(new DanmakuData());
            enemyShots[19].danmaku[i].speed = 5;
            enemyShots[19].danmaku[i].dir = (i / 11) * 36;
            enemyShots[19].danmaku[i].material = bigBulletRed;
            for(int j = i + 1; j < i + 11; j++)
            {
                enemyShots[19].danmaku.Add(new DanmakuData());
                enemyShots[19].danmaku[j].speed = 5;
                enemyShots[19].danmaku[j].dir = (i / 11) * 36;
                enemyShots[19].danmaku[j].position = new Vector2(Mathf.Cos((j - i) * (Mathf.PI / 5)) * 0.7f, Mathf.Sin((j - i) * (Mathf.PI / 5)) * 0.7f);
                enemyShots[19].danmaku[j].posBehavior = "normalMod";
                enemyShots[19].danmaku[j].material = smallBulletRed;
            }
        }
        enemyShots.Add(new EnemyShotData());
        for(int i = 0; i < 100; i+=11)
        {
            enemyShots[20].danmaku.Add(new DanmakuData());
            enemyShots[20].danmaku[i].speed = 5;
            enemyShots[20].danmaku[i].dir = (i / 11) * 36 + 18;
            enemyShots[20].danmaku[i].material = bigBulletRed;
            for(int j = i + 1; j < i + 11; j++)
            {
                enemyShots[20].danmaku.Add(new DanmakuData());
                enemyShots[20].danmaku[j].speed = 5;
                enemyShots[20].danmaku[j].dir = (i / 11) * 36 + 18;
                enemyShots[20].danmaku[j].position = new Vector2(Mathf.Cos((j - i) * (Mathf.PI / 5)) * 0.7f, Mathf.Sin((j - i) * (Mathf.PI / 5)) * 0.7f);
                enemyShots[20].danmaku[j].posBehavior = "normalMod";
                enemyShots[20].danmaku[j].material = smallBulletRed;
            }
        }
        #endregion
        #region Nozomi Nonspell 2
        enemyShots.Add(new EnemyShotData());
        for(int i = 0; i < 12; i++)
        {
            enemyShots[21].danmaku.Add(new DanmakuData());
            enemyShots[21].danmaku[i].speed = 5;
            enemyShots[21].danmaku[i].dir = 2;
            enemyShots[21].danmaku[i].dirBehavior = "patternTime";
            enemyShots[21].danmaku[i].position = new Vector2(Mathf.Cos(i * (Mathf.PI / 6)) * 0.4f, Mathf.Sin(i * (Mathf.PI / 6)) * 0.35f);
            enemyShots[21].danmaku[i].posBehavior = "normalMod";
            enemyShots[21].danmaku[i].material = smallBulletRed;
        }
        for(int i = 12; i < 24; i++)
        {
            enemyShots[21].danmaku.Add(new DanmakuData());
            enemyShots[21].danmaku[i].speed = 2;
            enemyShots[21].danmaku[i].dir = -1.5f;
            enemyShots[21].danmaku[i].dirBehavior = "patternTime";
            enemyShots[21].danmaku[i].position = new Vector2(Mathf.Cos(i * (Mathf.PI / 6)) * 0.4f, Mathf.Sin(i * (Mathf.PI / 6)) * 0.35f);
            enemyShots[21].danmaku[i].posBehavior = "normalMod";
            enemyShots[21].danmaku[i].material = smallBulletYel;
        }
        #endregion
        #region Earth Shaking Bolt
        enemyShots.Add(new EnemyShotData());
        enemyShots[22].danmaku.Add(new DanmakuData());
        enemyShots[22].danmaku[0].complex = true;
        enemyShots[22].danmaku[0].type = "lightning";
        enemyShots[22].danmaku[0].prefab = lightning;
        enemyShots[22].danmaku[0].speed = 10;
        enemyShots[22].danmaku[0].length = 5;
        enemyShots[22].danmaku[0].dirBehavior = "player";
        for(int i = 1; i < 65; i++)
        {
            enemyShots[22].danmaku.Add(new DanmakuData());
            enemyShots[22].danmaku[i].speed = (float)i / 10;
            enemyShots[22].danmaku[i].dir = 30;
            enemyShots[22].danmaku[i].dirBehavior = "playerRandom";
            if(i % 2 == 0)
                enemyShots[22].danmaku[i].material = smallBulletYel;
            else
                enemyShots[22].danmaku[i].material = smallBulletRed;
        }
        #endregion
        #region Nozomi Nonspell 3
        enemyShots.Add(new EnemyShotData());
        for(int i = 0; i < 10; i++)
        {
            enemyShots[23].danmaku.Add(new DanmakuData());
            enemyShots[23].danmaku[i].speed = 5;
            enemyShots[23].danmaku[i].dir = i * 36;
            enemyShots[23].danmaku[i].complex = true;
            enemyShots[23].danmaku[i].type = "lightning";
            enemyShots[23].danmaku[i].prefab = lightning;
            enemyShots[23].danmaku[i].speed = 4;
            enemyShots[23].danmaku[i].length = 2;
        }
        enemyShots.Add(new EnemyShotData());
        for(int i = 0; i < 10; i++)
        {
            enemyShots[24].danmaku.Add(new DanmakuData());
            enemyShots[24].danmaku[i].speed = 5;
            enemyShots[24].danmaku[i].dir = i * 36 + 18;
            enemyShots[24].danmaku[i].complex = true;
            enemyShots[24].danmaku[i].type = "lightning";
            enemyShots[24].danmaku[i].prefab = lightning;
            enemyShots[24].danmaku[i].speed = 6;
            enemyShots[24].danmaku[i].length = 2;
        }
        enemyShots.Add(new EnemyShotData());
        for(int i = 0; i < 36; i++)
        {
            enemyShots[25].danmaku.Add(new DanmakuData());
            enemyShots[25].danmaku[i].speed = 3;
            enemyShots[25].danmaku[i].dir = i * 10;
            enemyShots[25].danmaku[i].material = smallBulletYel;
        }
        #endregion
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

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[9].name = "Nozomi Nonspell 1";

        enemyPatterns[9].shots.Add(new EnemyShot{
            data = GetEnemyShot(19),
            startTime = 0.3f,
            endTime = 99,
            loopDelay = 0.6f,
        });
        enemyPatterns[9].shots.Add(new EnemyShot{
            data = GetEnemyShot(20),
            startTime = 0.6f,
            endTime = 99,
            loopDelay = 0.6f,
        });
        enemyPatterns[9].shots.Add(new EnemyShot{
            movement = "random",
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1.5f,
        });

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[10].name = "Nozomi Nonspell 2";

        enemyPatterns[10].shots.Add(new EnemyShot{
            data = GetEnemyShot(21),
            startTime = 0f,
            endTime = 99,
            loopDelay = 0.05f,
        });
        enemyPatterns[10].shots.Add(new EnemyShot{
            movement = "random",
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1.5f,
        });
        
        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[11].name = "Earth Shaking Bolt";
        enemyPatterns[11].shots.Add(new EnemyShot{
            data = GetEnemyShot(22),
            startTime = 1f,
            endTime = 99,
            loopDelay = 4f,
        });
        enemyPatterns[11].shots.Add(new EnemyShot{
            movement = "random",
            startTime = 1f,
            endTime = 99,
            loopDelay = 4f,
        });
        enemyPatterns[11].shots.Add(new EnemyShot{
            data = GetEnemyShot(20),
            startTime = 1.5f,
            endTime = 99,
            loopDelay = 4f,
        });
        enemyPatterns[11].shots.Add(new EnemyShot{
            data = GetEnemyShot(19),
            startTime = 2f,
            endTime = 99,
            loopDelay = 4f,
        });

        enemyPatterns.Add(new EnemyPattern());
        enemyPatterns[12].name = "Nozomi Nonspell 3";
        enemyPatterns[12].shots.Add(new EnemyShot{
            data = GetEnemyShot(23),
            startTime = 0.3f,
            endTime = 99,
            loopDelay = 0.6f,
        });
        enemyPatterns[12].shots.Add(new EnemyShot{
            data = GetEnemyShot(24),
            startTime = 0.6f,
            endTime = 99,
            loopDelay = 0.6f,
        });
        enemyPatterns[12].shots.Add(new EnemyShot{
            data = GetEnemyShot(25),
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1.5f,
        });
        enemyPatterns[12].shots.Add(new EnemyShot{
            movement = "random",
            startTime = 0.5f,
            endTime = 99,
            loopDelay = 1.5f,
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
            playerShots[0].danmaku[i].speed = 30;
            playerShots[0].danmaku[i].player = true;

            playerShots[0].danmaku[i].dir = 90;
            playerShots[0].danmaku[i].position = new Vector2(-0.5f + i, 0);
            playerShots[0].danmaku[i].posBehavior = "normalMod";

            playerShots[0].danmaku[i].prefab = sonicWave;
        }
        playerShots.Add(new PlayerShotData()); // Sonic wave pt2
        for(int i = 0; i < 2; i++)
        {
            playerShots[1].danmaku.Add(new DanmakuData());
            playerShots[1].danmaku[i].speed = 30;
            playerShots[1].danmaku[i].player = true;

            playerShots[1].danmaku[i].dir = 87.5f + (i * 5);

            playerShots[1].danmaku[i].prefab = sonicWave;
        }
        playerShots.Add(new PlayerShotData()); // Sonic wave pt3
        for(int i = 0; i < 2; i++)
        {
            playerShots[2].danmaku.Add(new DanmakuData());
            playerShots[2].danmaku[i].speed = 30;
            playerShots[2].danmaku[i].player = true;

            playerShots[2].danmaku[i].dir = 85 + (i * 10);
            playerShots[2].danmaku[i].position = new Vector2(-0.5f + i, 0);
            playerShots[2].danmaku[i].posBehavior = "normalMod";

            playerShots[2].danmaku[i].prefab = sonicWave;
        }
        playerShots.Add(new PlayerShotData()); // Sonic wave pt4
        for(int i = 0; i < 2; i++)
        {
            playerShots[3].danmaku.Add(new DanmakuData());
            playerShots[3].danmaku[i].speed = 30;
            playerShots[3].danmaku[i].player = true;

            playerShots[3].danmaku[i].dir = 75 + (i * 30);
            playerShots[3].danmaku[i].posBehavior = "normal";

            playerShots[3].danmaku[i].prefab = sonicWave;
        }

        /*
            Magic Missile (lv1)
        */
        playerShots.Add(new PlayerShotData());
        for(int i = 0; i < 2; i++)
        {
            playerShots[4].danmaku.Add(new DanmakuData());
            playerShots[4].danmaku[i].speed = 0;
            playerShots[4].danmaku[i].speedAcc = 60;
            playerShots[4].danmaku[i].player = true;

            playerShots[4].danmaku[i].dir = 90;
            playerShots[4].danmaku[i].position = new Vector2(-0.25f + i * 0.5f, 0);
            playerShots[4].danmaku[i].posBehavior = "normalMod";

            playerShots[4].danmaku[i].prefab = magicMissile;
        }
        playerShots.Add(new PlayerShotData());
        for(int i = 0; i < 2; i++)
        {
            playerShots[5].danmaku.Add(new DanmakuData());
            playerShots[5].danmaku[i].speed = 0;
            playerShots[5].danmaku[i].speedAcc = 60;
            playerShots[5].danmaku[i].player = true;

            playerShots[5].danmaku[i].dir = 90;
            playerShots[5].danmaku[i].position = new Vector2(-0.5f + i, 0);
            playerShots[5].danmaku[i].posBehavior = "normalMod";

            playerShots[5].danmaku[i].prefab = magicMissile;
        }

        /*
            Magic Missile (lv2)
        */
        playerShots.Add(new PlayerShotData());
        for(int i = 0; i < 2; i++)
        {
            playerShots[6].danmaku.Add(new DanmakuData());
            playerShots[6].danmaku[i].speed = 0;
            playerShots[6].danmaku[i].speedAcc = 30;
            playerShots[6].danmaku[i].player = true;

            playerShots[6].danmaku[i].dirBehavior = "enemy";
            playerShots[6].danmaku[i].position = new Vector2(2, 1);
            playerShots[6].danmaku[i].posBehavior = "randomMod";

            playerShots[6].danmaku[i].prefab = riftbinder;
        }

    }

    static void CreateInitialPlayerPatterns()
    {
        /*
            Sonic Wave
        */
        playerPatterns.Add(new PlayerPattern());
        playerPatterns[0].name = "Sonic Wave Lv1";
        playerPatterns[0].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(0),
            startTime = 0f
        });
        playerPatterns[0].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(1),
            startTime = 0.025f
        });
        playerPatterns[0].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(2),
            startTime = 0.05f
        });
        playerPatterns[0].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(3),
            startTime = 0.075f
        });

        /*
            Magic Missile
        */
        playerPatterns.Add(new PlayerPattern());
        playerPatterns[1].name = "Sonic Wave Lv1";
        playerPatterns[1].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(4),
            startTime = 0f
        });
        playerPatterns[1].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(5),
            startTime = 0.25f
        });

        /*
            Riftbinder
        */
        playerPatterns.Add(new PlayerPattern());
        playerPatterns[2].name = "Riftbinder Lv1";
        playerPatterns[2].shots.Add(new PlayerShot
        {
            data = GetPlayerShot(6),
            startTime = 0f
        });
    }

    public static EnemyPattern GetEnemyPattern(int id)
    {
        if(!built)
            BuildDatabase();
        return enemyPatterns[id];
    }

    public static EnemyShotData GetEnemyShot(int id)
    {
        if(!built)
            BuildDatabase();
        return enemyShots[id];
    }

    public static PlayerShotData GetPlayerShot(int id)
    {
        if(!built)
            BuildDatabase();
        return playerShots[id];
    }

    public static PlayerPattern GetPlayerPattern(int id)
    {
        if(!built)
            BuildDatabase();
        return playerPatterns[id];
    }
}