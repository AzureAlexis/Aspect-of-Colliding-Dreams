/*
    An instance of DanmakuData stores all variables that will be assigned to a danmaku
    upon creation. Assignment is done this way because deep copying is hard.
*/

using UnityEngine;
using System.Collections.Generic;

public class DanmakuData // The raw data of a danmaku, before it's applied to a specific instance
{
    public List<Movement> movements;

    // General stuff
    public string type;
    public bool player = false;             // Who owns this? enemy = false, player = true
    public bool complex = false;            // Will this be batch rendered or instantiated as a gameobject? player danmaku is always complex
    public string name;                     // Name of the danmaku, for debug purposes
    public Material material;               // What material the danmaku is made of, if simple
    public GameObject prefab;               // What prefab the danmaku is made of, if complex

    // Positioning stuff
    public Vector2 position;                // Self explanitory
    public string posMod;                   // If not null, used in conjunction with pos for more complex movements
    public string posBehavior = "normal";   // The way danmaku figures out it's position
    /* Possible posBehaviors:
        normal: spawns at position of whatever fired this
        normalMod: spawns at position of whatever fired this, modified by posMod/position
    */

    // Direction stuff
    public float dir = 0;                  // What direction the danmaku moves in (degrees)
    public float dirAcc = 0;
    public string dirMod;                  // If not null, used in conjunction with dir for more complex movements
    public string dirBehavior = "normal";  // The way the danmaku figures out where to move
    /* Possible dirBehaviors:
        -normal: Strictly follows dir
    */

    // Speed stuff
    public float speed = 1;                     // How fast the danmaku will move when this movement is triggered (unity units/sec)
    public float speedAcc = 0;

    // Lightning stuff - only used for lightning bullets
    public float length = 1;                // How long the lightning is
    public float distance = 1;
}