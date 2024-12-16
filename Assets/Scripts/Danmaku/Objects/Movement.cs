/*
    A movement stores new information for recalculating danamku paths after a danmaku has
    already been created. They're used for creating more detailed bullet patterns. If
    a danmaku doesn't need to change its path after it has been created, its
    "movements" variable is likely null.
*/

using UnityEngine;

public class Movement // Stores danmaku movements
{
    public string name;                         // Name of the movement, for debug purposes
    public float dir = 0;                       // What direction the danmaku moves in (degrees)
    public float speed = 1;                     // How fast the danmaku will move when this movement is triggered (unity units/sec)
    public float acc = 0;                       // How fast the danmaku accelerates (unity units/sec^2)
    public float time = 0;                      // At what time this movement should be executed
    public string dirBehavior = "normal";  
    public string posBehavior = "normal";
    /*
        Types of danmaku dir behaviors:
            -normal: Moves foward based on dir passed
            -player: Points towards the players position and moves foward indefinitly

        Types of danmaku pos behavior:
            -normal: Spawns at the enemy/shooter's position
            -enemyMod: Adds this danmaku's position value to the enemy's, and spawns there
            -absWorld: Spawns at this danmaku's position variable in world space
            -absCam: Spawns at this danmaku's position variable in cam space
    */

}