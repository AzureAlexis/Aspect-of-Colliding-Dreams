using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    static List<Pattern> patterns = new List<Pattern>();
    static List<Shot> shots = new List<Shot>();
    public static Material material;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PatternManager.LoadMaterials();
        PatternManager.CreateInitialShots();
        PatternManager.CreateInitialPatterns();
    }

    static void LoadMaterials()
    {
        material = Resources.Load("enemyBulletRed_0", typeof(Material)) as Material;
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
            shots[0].danmaku[i].material = material;
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

public class Pattern // Stores patterns that enemies fire
{
    public string name;
    public List<Shot> shots;
    public List<float> shotTimes;
    public float length;
}

public class Shot // A list of danmakus that fire at the same time
{
    public string name;
    public List<DanmakuData> danmaku;
}

public class DanmakuData // The raw data of a danmaku, before it's applied to a specific instance
{
    public List<Movement> movements;
    public string name;                         // Name of the movement, for debug purposes
    public Vector2 position;
    public Material material;
    public bool complex = false;
    public float dir = 0;                       // What direction the danmaku moves in (degrees)
    public float speed = 1;                     // How fast the danmaku will move when this movement is triggered (unity units/sec)
    public float acc = 0;                       // How fast the danmaku accelerates (unity units/sec^2)
    public float time = 0;                      // How long has the danmaku existed (seconds)
    public string dirBehavior = "normal";  // The way the danmaku figures out where to move
    public string posBehavior = "enemy";
    public string accBehavior = "compound";
}

public class Movement // Stores danmaku movements
{
    public string name;                         // Name of the movement, for debug purposes
    public float dir = 0;                       // What direction the danmaku moves in (degrees)
    public float speed = 1;                     // How fast the danmaku will move when this movement is triggered (unity units/sec)
    public float acc = 0;                       // How fast the danmaku accelerates (unity units/sec^2)
    public float time = 0;                      // How long has the danmaku existed (seconds)
    public string dirBehavior = "normal";  // The way the danmaku figures out where to move
    public string posBehavior = "normal";
    /*
        Types of danmaku move behaviors:
            -normal: Moves foward based on dir passed
            -toPlayer: Points towards the players position and moves foward indefinitly
    */

}