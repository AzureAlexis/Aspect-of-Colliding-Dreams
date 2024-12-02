using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    static List<Pattern> patterns = new List<Pattern>();
    static List<Shot> shots = new List<Shot>();
    static GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        DontDestroyOnLoad(gameObject);
        PatternManager.CreateInitialShots();
        PatternManager.CreateInitialPatterns();
    }

    static void CreateInitialShots()
    {
        shots.Add(new Shot());
        shots[0].name = "8 Way Normal Bullet at Player";

        shots[0].bullets = new List<RawBulletData>();
        for(int i = 0; i < 8; i++)
        {
            shots[0].bullets.Add(new RawBulletData());
            shots[0].bullets[i].speed = 5;
            shots[0].bullets[i].dir = i * 45;
            shots[0].bullets[i].dirBehavior = "moveFoward";
        }
    }

    static void CreateInitialPatterns()
    {
        Debug.Log("Made patterns");
        patterns.Add(new Pattern());
        patterns[0].name = "debug shot";

        patterns[0].shots = new List<Shot>();
        patterns[0].shots.Add(GetShot(0));

        patterns[0].shotTimes = new List<float>();
        patterns[0].shotTimes.Add(2);
    }

    static float PointToPlayer(Vector3 position)
    {
        float y = player.transform.position.y - position.y;
        float x = player.transform.position.x - position.x;
        return Mathf.Atan2(y, x);
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
}

public class Shot // A list of bullets that fire at the same time
{
    public string name;
    public List<RawBulletData> bullets;
}

public class RawBulletData // The raw data of a bullet, before it's applied to a specific instance
{
    public List<Movement> movements;
    public string name;                         // Name of the movement, for debug purposes
    public float x = 0;
    public float y = 0;
    public float dir = 0;                       // What direction the bullet moves in (degrees)
    public float speed = 1;                     // How fast the bullet will move when this movement is triggered (unity units/sec)
    public float acc = 0;                       // How fast the bullet accelerates (unity units/sec^2)
    public float time = 0;                      // How long has the bullet existed (seconds)
    public string dirBehavior = "MoveToPosition";  // The way the bullet figures out where to move
    public string posBehavior = "enemyPosition";
}

public class Movement // Stores bullet movements
{
    public string name;                         // Name of the movement, for debug purposes
    public float speed = 1;                     // How fast the bullet will move when this movement is triggered (unity units/sec)
    public float dir = 0;                       // What direction the bullet moves in (degrees)
    public float acc = 0;                       // How fast the bullet accelerates (unity units/sec^2)
    public float time = 0;                      // After how long the bullet has to exist before this movement triggers (seconds)
    public string behavior = "MoveFoward";  // The way the bullet figures out where to move
    /*
        Types of bullet move behaviors:
            -MoveFoward: Moves foward based on dir passed
            -MoveToPlayer: Points towards the players position and moves foward indefinitly
    */

}
