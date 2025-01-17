/* 
    This (mostly) static class contains all bullet patterns in the game. The object
    "PatternManager" only exists to initilize the patterns in this script
*/

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    static List<Pattern> patterns = new List<Pattern>();
    static List<Shot> shots = new List<Shot>();
    public static Material enemyBulletRed;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PatternManager.LoadMaterials();
        PatternManager.CreateInitialShots();
        PatternManager.CreateInitialPatterns();
    }

    static void LoadMaterials()
    {
        enemyBulletRed = Resources.Load("enemyBulletRed_0", typeof(Material)) as Material;
    }

    static void CreateInitialShots()
    {
        shots.Add(new Shot());
        shots[0].name = "8 way spread 1";
        for(int i = 0; i < 8; i++)
        {
            shots[0].danmaku.Add(new DanmakuData());
            shots[0].danmaku[i].speed = 4;
            shots[0].danmaku[i].dir = 45 * i;
            shots[0].danmaku[i].material = enemyBulletRed;
        }

        shots.Add(new Shot());
        shots[1].name = "8 way spread 2";
        for(int i = 0; i < 8; i++)
        {
            shots[1].danmaku.Add(new DanmakuData());
            shots[1].danmaku[i].speed = 4;
            shots[1].danmaku[i].dir = 45 * i + 22.5f;
            shots[1].danmaku[i].material = enemyBulletRed;
        }

        shots.Add(new Shot());
        shots[2].name = "Tight 32 way spread 1";
        for(int i = 0; i < 32; i++)
        {
            shots[2].danmaku.Add(new DanmakuData());
            shots[2].danmaku[i].speed = 0.5f;
            shots[2].danmaku[i].dir = i * 10 + 180;
            shots[2].danmaku[i].material = enemyBulletRed;
        }

        shots.Add(new Shot());
        shots[3].name = "Tight 32 way spread 1";
        for(int i = 0; i < 32; i++)
        {
            shots[3].danmaku.Add(new DanmakuData());
            shots[3].danmaku[i].speed = 0.5f;
            shots[3].danmaku[i].dir = i * 10 + 240;
            shots[3].danmaku[i].material = enemyBulletRed;
        }

        shots.Add(new Shot());
        shots[4].name = "Back Spread 1";
        for(int i = 0; i < 36; i++)
        {
            shots[4].danmaku.Add(new DanmakuData());
            shots[4].danmaku[i].speed = 6;
            shots[4].danmaku[i].dir = i * 5 + - 87.5f;
            shots[4].danmaku[i].material = enemyBulletRed;
        }

        shots.Add(new Shot());
        shots[5].name = "Back Spread 2";
        for(int i = 0; i < 36; i++)
        {
            shots[5].danmaku.Add(new DanmakuData());
            shots[5].danmaku[i].speed = 6;
            shots[5].danmaku[i].dir = i * 5 + - 90f;
            shots[5].danmaku[i].material = enemyBulletRed;
        }
    }

    static void CreateInitialPatterns()
    {
        patterns.Add(new Pattern());
        patterns[0].name = "Move tutorial";
        patterns[0].loopTime = 2;
        patterns[0].endCondition = "time";
        patterns[0].endValue = 8;
        patterns[0].endEvent = "tutorial2";

        patterns[0].shots.Add(GetShot(0));
        patterns[0].shots.Add(GetShot(1));

        patterns[0].shotTimes.Add(1);
        patterns[0].shotTimes.Add(2);

        //----------------------------

        patterns.Add(new Pattern());
        patterns[1].name = "Focus tutorial";
        patterns[1].loopTime = 4;
        patterns[1].endCondition = "time";
        patterns[1].endValue = 12;
        patterns[1].endEvent = "tutorial3";

        patterns[1].shots.Add(GetShot(2));
        patterns[1].shots.Add(GetShot(3));

        patterns[1].shotTimes.Add(1);
        patterns[1].shotTimes.Add(3);

        //----------------------------

        patterns.Add(new Pattern());
        patterns[2].name = "Shoot tutorial";
        patterns[2].loopTime = 0.2f;
        patterns[2].endCondition = "hp";
        patterns[2].endValue = 950;
        patterns[2].endEvent = "tutorial4";

        patterns[2].shots.Add(GetShot(4));
        patterns[2].shots.Add(GetShot(5));

        patterns[2].shotTimes.Add(0.1f);
        patterns[2].shotTimes.Add(0.2f);
    }

    public static Pattern GetPattern(int id)
    {
        return patterns[id];
    }

    public static Shot GetShot(int id)
    {
        return shots[id];
    }
}