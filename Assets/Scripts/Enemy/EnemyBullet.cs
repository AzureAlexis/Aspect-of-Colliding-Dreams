using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class EnemyBullet : MonoBehaviour
{
    Camera cam;
    GameObject player;

    public float time = 0;
    public float speed = 1;                     // How fast the bullet will move when this movement is triggered (unity units/sec)
    public float acc = 0;                       // How fast the bullet accelerates (unity units/sec^2)
    public List<Movement> movements;
    public string posBehavior = "enemy";        // How the initial position of this bullet is determined. Unused after initilization
    public string accBehavior = "compound";     // How acceleration is applied to the speed
    public string dirBehavior = "normal";       // How the direction is calculated
    public bool persist = false;                // If going out of bounds should not destroy this bullet (bullet is still destroyed when pattern ends)
    private bool destroy = false;               // If the bullet should be destroyed this frame (usually because it's out of bounds or pattern ended)

    void Start()
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        UpdateTime();
        UpdateMovement();
        UpdatePosition();
        UpdateSpeed();
        UpdateOutOfBounds();
        UpdateDestroy();
    }

    void UpdateTime()
    {
        time += Time.deltaTime;
    }

    void UpdateMovement()
    {
        for(int i = 0; i < movements.Count; i++)
        {
            float moveTime = movements[i].time;
            if(time - Time.deltaTime < moveTime && time > moveTime)
            {
                AssignMovement(movements[i]);
                break;
            }
        }
    }

    void UpdatePosition()
    {
        Vector3 moveVector;
        moveVector = transform.right;
        moveVector *= speed;
        moveVector *= Time.deltaTime;

        transform.position += moveVector;
    }

    void UpdateSpeed()
    {
        float adjustedAcc;
        switch(accBehavior)
        {
            case "compound":
                adjustedAcc = acc * Time.deltaTime;
                speed = speed + adjustedAcc;
                break;

            case "multiply":
                adjustedAcc = acc * Time.deltaTime - 1;
                speed = speed * adjustedAcc;
                break;
        }
    }

    void UpdateOutOfBounds()
    {
        Vector3 relativePosition = cam.WorldToViewportPoint(transform.position);
        if(Mathf.Abs(relativePosition.x) > 1.1)
        {
            destroy = true;
        }
        else if(Mathf.Abs(relativePosition.y) > 1.1)
        {
            destroy = true;
        }
    }

    void UpdateDestroy()
    {
        if(destroy)
        {
            Destroy(gameObject);
        }
    }

    void AssignMovement(Movement movement)
    {
        speed = movement.speed;
        acc = movement.acc;
        accBehavior = movement.accBehavior;
        dirBehavior = movement.dirBehavior;

        switch (dirBehavior)
        {
            case "toPlayer":
                transform.LookAt(player.transform);
                break;

            case "normal":
                transform.rotation = Quaternion.Euler(0, 0, movement.dir);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            destroy = true;
        }
    }


}
