using UnityEngine;
using System.Collections.Generic;

public class EnemyPattern // Stores patterns that enemies fire
{
    public string name;                 // Name of the pattern
    public bool spell = false;          // Is this a spell card? Used for cut ins
    public bool lastWord = false;       // Is this a last word? Used for hp display
    public List<EnemyShot> shots = new List<EnemyShot>();            // List of shots
    public float loopTime;              // If more than zero, repeats this pattern after looptime
    public string endCondition = "hp";  // What needs to happen for this pattern to end?
    public float endValue;              // After how much damage/time will this pattern end
    public string endEvent = "none";
    public int background = -1;
    public AudioSource sound;
}
