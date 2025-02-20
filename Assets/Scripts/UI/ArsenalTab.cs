using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArsenalTab : MenuTab
{
    string state = "display";
    Transform cursor;
    List<Transform> slots = new List<Transform>();

    void Start()
    {
        cursor = transform.GetChild(0);
        foreach(Transform child in GameObject.Find("ArsenalSlotDisplay").transform)
            slots.Add(child);
    }

    new public void Update()
    {
        base.Update();
        if(UIManager.state == "menu")
        {
            if(UIManager.currentTab == "arsenal")
            {
                UpdateActive();
            }
            else
            {
                UpdateInactive();
            }
        }
    }

    new public void UpdateActive()
    {
        UpdateCursor();
    }

    void UpdateCursor()
    {
        float x = 0;
        float y = 0;

        if(Input.GetKeyDown(KeyCode.LeftArrow))
            x += -1;
        if(Input.GetKeyDown(KeyCode.RightArrow))
            x += 1;
        if(Input.GetKeyDown(KeyCode.DownArrow))
            y += -1;
        if(Input.GetKeyDown(KeyCode.UpArrow))
            y += 1;

        if(x != 0 || y != 0)
            MoveCursor(x, y);
    }

    void MoveCursor(float x, float y)
    {
        List<Transform> activeMoveSpots = new List<Transform>(); 

        switch(state)
        {
            case "display":
                activeMoveSpots = slots;
                break;
        }

        Vector3 closest = new Vector3(999999, 999999, 999999);

        foreach(Transform spot in activeMoveSpots)
        {
            Vector3[] cursorWorldCorners = new Vector3[4];
            cursor.GetComponent<RectTransform>().GetWorldCorners(cursorWorldCorners);
            Vector3 cursorWorldPos = (cursorWorldCorners[0] + cursorWorldCorners[1] + cursorWorldCorners[2] + cursorWorldCorners[3]) / 4;

            Vector3[] spotWorldCorners = new Vector3[4];
            spot.GetComponent<RectTransform>().GetWorldCorners(spotWorldCorners);
            Vector3 spotWorldPos = (spotWorldCorners[0] + spotWorldCorners[1] + spotWorldCorners[2] + spotWorldCorners[3]) / 4;

            float distance = Vector3.Distance(spotWorldPos, cursorWorldPos);
            Debug.Log(cursor.InverseTransformPoint(spotWorldPos));

            if(spotWorldPos.x - cursorWorldPos.x > 0 == x > 0)
                continue;
            if(spotWorldPos.y - cursorWorldPos.y > 0 == y > 0)
                continue;
            if(distance == 0)
                continue;
            if(distance >= Vector3.Distance(closest, cursorWorldPos))
                continue;
            
            closest = spot.position;
        }
    }
}
