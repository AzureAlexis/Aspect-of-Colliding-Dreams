using UnityEngine;
using System;
using System.Collections.Generic;

public class Danmaku
{
    public List<Movement> movements;
    public string name;                         // Name of the movement, for debug purposes
    public Vector2 position;
    public Material material;
    public bool complex = false;
    public float dir = 0;                       // What direction the danmaku moves in (degrees)
    public float speed = 1;                     // How fast the danmaku will move when this movement is triggered (unity units/sec)
    public float time = 0;                      // How long has the danmaku existed (seconds)
    public string dirBehavior = "normal";  // The way the danmaku figures out where to move
    public string posBehavior = "normal";

    public void Update()
    {
        UpdatePosition();
        UpdateTime();
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
}



