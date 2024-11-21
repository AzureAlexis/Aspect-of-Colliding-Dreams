using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    static List<Pattern> patterns = new List<Pattern>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PatternManager.CreateInitialPatterns();
    }

    static void CreateInitialPatterns()
    {
        patterns.Add(new Pattern{
            name = "debug name",
            movements = new List<Movement> {
                new Movement{
                    name = "movementName"
                }
            },
        });
    }
}

public class Pattern // Stores patterns that enemies fire
{
    public string name;
    public List<Movement> movements;
}

public class Movement // Stores bullet movements
{
    public string name;
}
