using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArsenalTab : MenuTab
{
    string state = "display";
    int selectedSlotIndex = 0;
    BattleSlotBase selectedItem;
    Transform cursor;
    List<Transform> slots = new List<Transform>();
    List<Transform> stats = new List<Transform>();

    void Start()
    {
        cursor = transform.GetChild(0);

        foreach(Transform child in GameObject.Find("ArsenalSlotDisplay").transform)
            slots.Add(child);

        foreach(Transform child in GameObject.Find("Stat List").transform)
            stats.Add(child);
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
        UpdateAction();
        UpdateDisplay();
    }

    void UpdateAction()
    {
        string newState = null;
        string action = null;

        if(Input.GetKeyDown(KeyCode.Z))
        {
            switch(state)
            {
                case "display":
                    newState = "topList";
                    break;

                case "topList":
                    switch(selectedSlotIndex)
                    {
                        case 0:
                            newState = "change";
                            break;
                        case 1:
                            newState = "upgrade";
                            break;
                        case 2:
                            newState = "disenchant";
                            break;
                    }
                    break;

                case "change":
                    action = "equip";
                    break;

                case "upgrade":
                    switch(selectedSlotIndex)
                    {
                        case 0:
                            newState = "topList";
                            break;
                        case 1:
                            newState = "topList";
                            action = "upgrade";
                            break;
                    }
                    break;

                case "disenchant":
                    switch(selectedSlotIndex)
                    {
                        case 0:
                            newState = "topList";
                            break;
                        case 1:
                            newState = "display";
                            action = "disenchant";
                            break;
                    }
                    break;
            }
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            switch(state)
            {
                case "topList":
                    newState = "display";
                    break;
                case "disenchant":
                    newState = "topList";
                    break;
                case "change":
                    newState = "topList";
                    break;
                case "upgrade":
                    newState = "topList";
                    break;
            }
        }

        /*
        if(newState != null)
            ChangeDisplayState(newState);

        if(newAction != null)
            DoArsenalAction(action, selectedItem);
        */
    }

    void UpdateDisplay()
    {
        BattleSlotBase activeSlot = PlayerStats.battleSlots[selectedSlotIndex];
        stats[0].GetComponent<TextMeshProUGUI>().text = activeSlot.name;

        if(activeSlot.GetType().ToString() == "Consumable")
        {
            stats[1].GetComponent<TextMeshProUGUI>().text = activeSlot.count.ToString();
            stats[2].GetComponent<TextMeshProUGUI>().text = activeSlot.limit.ToString();
            stats[3].GetComponent<TextMeshProUGUI>().text = "";
            stats[4].GetComponent<TextMeshProUGUI>().text = "";
            stats[5].GetComponent<TextMeshProUGUI>().text = "";

            stats[1].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Count";
            stats[2].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
            stats[3].GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            stats[4].GetChild(0).GetComponent<TextMeshProUGUI>().text = activeSlot.flavorShort;
            stats[5].GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            stats[1].GetComponent<TextMeshProUGUI>().text = activeSlot.publicPower;
            stats[2].GetComponent<TextMeshProUGUI>().text = activeSlot.publicSpeed;
            stats[3].GetComponent<TextMeshProUGUI>().text = activeSlot.publicRange;
            stats[4].GetComponent<TextMeshProUGUI>().text = activeSlot.publicAccu;
            stats[5].GetComponent<TextMeshProUGUI>().text = activeSlot.publicCost;

            stats[1].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Power -";
            stats[2].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Speed -";
            stats[3].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Range -";
            stats[4].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Accu. -";
            stats[5].GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cost -";
        }
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

        Vector3 bestPos = new Vector3(999999, 999999, 999999);
        float bestPri = 999999;
        int index = -1;

        foreach(Transform spot in activeMoveSpots)
        {
            index++;

            Vector3 spotPosition = cursor.parent.InverseTransformPoint(spot.position);
            Vector3 cursorPosition = cursor.transform.localPosition;
            float distance = Vector3.Distance(spotPosition, cursor.localPosition);
            float priority = 999999;

            // Check if spot is valid, givin the direction
            if(spotPosition.x - cursor.localPosition.x > 0 == x < 0 && x != 0)
                continue;
            if(spotPosition.y - cursor.localPosition.y > 0 == y < 0 && y != 0)
                continue;
            if(distance <= 1)
                continue;
            
            // Make priority based on distance and angle
            if(x != 0)
                priority = MakePriority(cursorPosition, spotPosition, distance, "y");
            if(y != 0)
                priority = MakePriority(cursorPosition, spotPosition, distance, "x");
            
            // Assign if priority is best
            if(priority < bestPri)
            {
                bestPri = priority;
                bestPos = spotPosition;
                selectedSlotIndex = index;
            }
        }

        if(bestPos != new Vector3(999999, 999999, 999999))
            cursor.GetComponent<UiElement>().SetNewWaypoint(bestPos, 0.1f);
    }

    float MakePriority(Vector3 cursorPosition, Vector3 spotPosition, float distance, string cord)
    {
        float pri;
        float component = 0;

        if(cord == "x")
            component = Mathf.Abs(spotPosition.x - cursorPosition.x);
        else if(cord == "y")
            component = Mathf.Abs(spotPosition.y - cursorPosition.y);
        else
            Debug.LogError("Invalid direction");

        pri = 1 - (Mathf.Acos(component / distance) / Mathf.PI);

        Debug.Log(cursorPosition);
        return pri * distance;
    }
}
