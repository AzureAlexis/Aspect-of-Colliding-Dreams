using UnityEngine;
using System.Collections.Generic;

public class Pattern // Stores patterns that enemies fire
{
    public string name;                 // Name of the pattern
    public bool spell;                  // Is this a spell card? Used for cut ins
    public List<Shot> shots = new List<Shot>();            // List of shots
    public List<float> shotTimes = new List<float>();
    public float loopTime;              // If more than zero, repeats this pattern after looptime
    public string endCondition = "hp";  // What needs to happen for this pattern to end?
    public float endValue;              // After how much damage/time will this pattern end
    public string endEvent = "none";
}
