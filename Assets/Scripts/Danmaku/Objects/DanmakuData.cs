/*
    An instance of DanmakuData stores all variables that will be assigned to a danmaku
    upon creation. Assignment is done this way because deep copying is hard.
*/

using UnityEngine;
using System.Collections.Generic;

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