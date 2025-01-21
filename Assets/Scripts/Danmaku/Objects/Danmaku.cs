using UnityEngine;
using System;
using System.Collections.Generic;

public class Danmaku
{
    public List<Movement> movements;
    public string name;                         // Name of the movement, for debug purposes
    public Material material;
    public bool complex = false;

    // Positioning stuff
    public Vector2 position;                // Self explanitory
    public string posMod = "none";          // If not null, used in conjunction with pos for more complex movements
    public string posBehavior = "normal";   // The way danmaku figures out it's position
    /* Possible posBehaviors:
        normal: spawns at position of whatever fired this
        normalMod: spawns at position of whatever fired this, modified by posMod/position

       Possible posMods:
        sinX: modifies x position based on sin(patternTime), times pos
    */

    // Direction stuff
    public float dir = 0;                  // What direction the danmaku moves in (degrees)
    public string dirMod;                  // If not null, used in conjunction with dir for more complex movements
    public string dirBehavior = "normal";  // The way the danmaku figures out where to move
    /* Possible dirBehaviors:
        -normal: Strictly follows dir
    */

    public float speed = 1;                     // How fast the danmaku will move when this movement is triggered (unity units/sec)
    public float time = 0;                      // How long has the danmaku existed (seconds)
    public DanmakuBatch batch;

    public void Update()
    {
        UpdatePosition();
        UpdateTime();
        UpdateCollision();
        UpdateDestroy();
    }

    void UpdatePosition()
    {
        Vector2 moveVector = new Vector2(Mathf.Sin(dir*Mathf.PI/180), Mathf.Cos(dir*Mathf.PI/180));

        moveVector *= Time.deltaTime;
        moveVector *= speed;

        position += moveVector;
    }

    void UpdateTime()
    {
        time += Time.deltaTime;
    }

    void UpdateCollision()
    {
        Vector2 origin = position;
        float radius = 0.0625f;
        Vector2 direction = new Vector2(Mathf.Sin(dir), Mathf.Cos(dir));
        float distance = 0.001f;

        RaycastHit2D hits = Physics2D.CircleCast(origin, radius, direction, distance);

        if(hits.collider != null)
        {
            if(hits.collider.gameObject.GetComponent<PlayerStats>() != null && hits.collider.isTrigger)
            {
                hits.collider.gameObject.GetComponent<PlayerStats>().TakeDamage();
                batch.batch.Remove(this);
            }
        }
    }

    void UpdateDestroy()
    {
        Vector2 camPos = UIManager.cam.GetComponent<Camera>().WorldToViewportPoint(position);
        if(camPos.x > 1.1 || camPos.x < -0.1 || camPos.y > 1.1 || camPos.y < -0.1)
            batch.batch.Remove(this);
    }
}



