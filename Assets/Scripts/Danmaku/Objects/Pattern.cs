using UnityEngine;
using System.Collections.Generic;

public class Pattern // Stores patterns that enemies fire
{
    public string name;
    public List<Shot> shots;
    public List<float> shotTimes;
    public float length;
}
