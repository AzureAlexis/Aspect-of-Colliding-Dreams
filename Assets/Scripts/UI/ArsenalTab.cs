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
        base.UpdateActive();
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
            Vector3 spotLocalPosition = cursor.parent.InverseTransformPoint(spot.position);
            float distance = Vector3.Distance(spotLocalPosition, cursor.localPosition);

            Debug.Log(Vector3.Distance(closest, cursor.localPosition));
            if(spotLocalPosition.x - cursor.localPosition.x > 0 == x < 0 && x != 0)
                continue;
            if(spotLocalPosition.y - cursor.localPosition.y > 0 == y < 0 && y != 0)
                continue;
            if(distance <= 1)
                continue;
            if(distance >= Vector3.Distance(closest, cursor.localPosition))
                continue;
            
            closest = spotLocalPosition;
        }
        if(closest != new Vector3(999999, 999999, 999999))
            cursor.GetComponent<UiElement>().StartMove(closest, 0.1f);
    }
}
