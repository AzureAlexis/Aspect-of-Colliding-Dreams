using System;
using System.Diagnostics;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player;
    string state;
    float stateDelay = 0;   // If need to change state, wait this long before doing so
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        UpdateState();
        UpdatePosition();
    }

    void UpdateState()
    {
        string newState = state;
        float newStateDelay = 0;

        if(BattleManager.IsActive())
        {
            newState = "battle";
            newStateDelay = 1.2f;
        }
        else
        {
            newState = "normal";
            newStateDelay = 0;
        }

        if(newState != state && stateDelay <= 0)
        {
            state = newState;
            stateDelay = newStateDelay;
        }
        else if(newState != state && stateDelay > 0)
        {
            stateDelay -= Time.deltaTime;
        }
    }

    void UpdatePosition()
    {
        switch(state)
        {
            case "battle":
                break;

            case "normal":
                transform.position = player.transform.position + new Vector3(0, 0, -10);
                break;

        }
    }
}
