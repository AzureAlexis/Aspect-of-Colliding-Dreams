using UnityEngine;
using System.Collections.Generic;

public class PlayerPattern // Stores patterns that enemies fire
{
    public string name;                 // Name of the pattern
    public List<PlayerShot> shots = new List<PlayerShot>();            // List of shots
    public List<Vector3> shotTimes = new List<Vector3>();
    public float loopTime;              // If more than zero, repeats this pattern after looptime
    public float endTime;
}
