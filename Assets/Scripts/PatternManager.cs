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
        shots.Add(new Shot {
            name = "8 Way Normal Bullet at Player",
            bullets = new List<RawBulletData> {
                new RawBulletData {
                    speed = 
                }
            }
        });
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
    public string name;
    public float speed;
    public float minSpeed;

}
