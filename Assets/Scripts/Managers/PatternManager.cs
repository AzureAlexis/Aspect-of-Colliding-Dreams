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
        shots[0].name = "8 way spread";
        shots[0].danmaku = new List<DanmakuData>();

        for(int i = 0; i < 8; i++)
        {
            shots[0].danmaku.Add(new DanmakuData());
            shots[0].danmaku[i].speed = 4;
            shots[0].danmaku[i].dir = 45 * i;
            shots[0].danmaku[i].material = enemyBulletRed;
        }
    }

    static void CreateInitialPatterns()
    {
        patterns.Add(new Pattern());
        patterns[0].name = "debug shot";
        patterns[0].length = 2;

        patterns[0].shots = new List<Shot>();
        patterns[0].shots.Add(GetShot(0));

        patterns[0].shotTimes = new List<float>();
        patterns[0].shotTimes.Add(2);
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