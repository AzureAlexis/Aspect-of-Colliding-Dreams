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

        shots[0].bullets = new List<RawBulletData>;
        shots[0].bullets
    }

    static void CreateInitialPatterns()
    {
        patterns.Add(new Pattern {
            name = "debug name",
            shots = new List<Shot> {
                
            }
        });
    }

    static float PointToPlayer(Vector3 position)
    {
        float y = player.transform.position.y - position.y;
        float x = player.transform.position.x - position.x;
        return Mathf.Atan2(y, x);
    }
}

public class Pattern // Stores patterns that enemies fire
{
    public string name;
    public List<Shot> shots;
}

public class Shot // A list of bullets that fire at the same time
{
    public string name;
    public List<RawBulletData> bullets;
}

public class RawBulletData // The raw data of a bullet, before it's applied to a specific instance
{
    public List<Movement> movements;
}

public class Movement // Stores bullet movements
{
    public string name;                         // Name of the movement, for debug purposes
    public float speed = 1;                     // How fast the bullet will move when this movement is triggered (unity units/sec)
    public float dir = 0;                       // What direction the bullet moves in (degrees)
    public float acc = 0;                       // How fast the bullet accelerates (unity units/sec^2)
    public float time = 0;                      // After how long the bullet has to exist before this movement triggers (seconds)
    public string behavior = "MoveToPosition";  // The way the bullet figures out where to move
    /*
        Types of bullet move behaviors:
            -MoveToPosition: Points towards a specific position (in cam space), and moves foward indefinitly
            -MoveFoward: Moves foward based on dir passed
            -MoveToPlayer: Points towards the players position and moves foward indefinitly
    */

}
