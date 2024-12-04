using System;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    float debugTick = 0;
    GameObject player;
    Vector3 waypointOld = new Vector3(0, 0, 0);
    Vector3 waypointPosition = new Vector3(0, 0, 0);
    float waypointDuration = 0;
    float waypointTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerStats.player;
    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerStats.player;
        UpdatePosition();
        debugTick += Time.deltaTime;
        if(debugTick >= 1.5)
        {
            debugTick -= 1.5f;
            MakeWaypoint("toPlayer");
        }
    }

    void UpdatePosition()
    {
        if(waypointPosition != transform.position)
        {
            float factor;
            factor = waypointTime / waypointDuration;
            factor = Mathf.SmoothStep(0, 1, factor);

            transform.position = Vector3.Lerp(waypointOld, waypointPosition, factor);
            waypointTime += Time.deltaTime;
        }
    }

    void MakeWaypoint(string template)
    {
        float wayX = transform.position.x;;
        float wayY = transform.position.y;
        float wayD = 0;

        switch (template)
        {
            case "toPlayer" :
                wayX = player.transform.position.x;
                if(wayX < 0)
                {
                    wayX = Mathf.Max(wayX, transform.position.x - 2);
                }
                else
                {
                    wayX = Mathf.Min(wayX, transform.position.x + 2);
                }

                wayY = player.transform.position.y;
                if(wayY < 0)
                {
                    wayY = Mathf.Max(wayY, transform.position.y - 2);
                }
                else
                {
                    wayY = Mathf.Min(wayY, transform.position.y + 2);
                }

                wayD = 1;
                break;
        }

        waypointOld = transform.position;
        waypointDuration = wayD;
        waypointPosition = new Vector3(wayX, wayY, 0);
        waypointTime = 0;
    }
}
