using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArsenalTab : MenuTab
{
    public string state = "display";
    int selectedSlotIndex = 0;
    BattleSlotBase selectedItem;
    UiElement cursor;
    UiElement itemName;
    UiElement arsenalSlotDisplay;
    UiElement statList;
    UiElement topList;
    UiElement catagoryList;
    List<UiElement> slots = new List<UiElement>();
    List<UiElement> stats = new List<UiElement>();
    List<UiElement> topListItems = new List<UiElement>();
    List<UiElement> catagories = new List<UiElement>();

    void Start()
    {
        itemName = GameObject.Find("ShotName").GetComponent<UiElement>();
        cursor = transform.GetChild(0).GetComponent<UiElement>();
        arsenalSlotDisplay = GameObject.Find("ArsenalSlotDisplay").GetComponent<UiElement>();
        statList = GameObject.Find("Stat List").GetComponent<UiElement>();
        topList = GameObject.Find("Top List").GetComponent<UiElement>();
        catagoryList = GameObject.Find("Item Catagories").GetComponent<UiElement>();

        foreach(Transform child in GameObject.Find("ArsenalSlotDisplay").transform)
            slots.Add(child.GetComponent<UiElement>());

        foreach(Transform child in GameObject.Find("Stat List").transform)
            stats.Add(child.GetComponent<UiElement>());

        foreach(Transform child in GameObject.Find("Top List").transform)
            topListItems.Add(child.GetComponent<UiElement>());

        foreach(Transform child in GameObject.Find("Item Catagories").transform)
            catagories.Add(child.GetComponent<UiElement>());
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
        if(state == "display")
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
                            newState = "catagoryList";
                            break;
                        case 1:
                            newState = "upgrade";
                            break;
                        case 2:
                            newState = "disenchant";
                            break;
                    }
                    break;

                case "catagoryList":
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
                case "catagoryList":
                    newState = "topList";
                    break;
                case "upgrade":
                    newState = "topList";
                    break;
            }
        }

        if(newState != null)
            ChangeDisplayState(newState);

        /*
        if(newAction != null)
            DoArsenalAction(action, selectedItem);
        */
    }

    void ChangeDisplayState(string newState)
    {
        switch(newState)
        {
            case "display":
                ForceCursorMove(slots[0]);

                topList.DeactivateAll();
                arsenalSlotDisplay.ActivateAll();
                statList.ActivateAll();

                foreach(UiElement slot in slots)
                    slot.Activate(0.25f, 0);

                break;

            case "topList":
                ForceCursorMove(topListItems[0]);

                topList.ActivateAll();
                catagoryList.DeactivateAll();
                arsenalSlotDisplay.DeactivateAll();
                statList.DeactivateAll();
                
                slots[selectedSlotIndex].Activate(0.25f, 1);

                break;

            case "catagoryList":
                ForceCursorMove(catagories[0]);

                catagoryList.ActivateAll();

                break;
        }
        state = newState;
        selectedSlotIndex = 0;
    }
    void UpdateDisplay()
    {
        selectedItem = PlayerStats.battleSlots[selectedSlotIndex];
        itemName.SetText(selectedItem.name);
        
        if(selectedItem.GetType().ToString() == "Consumable")
        {
            stats[0].SetText(selectedItem.count.ToString());
            stats[1].SetText(selectedItem.limit.ToString());
            stats[2].SetText();
            stats[3].SetText();
            stats[4].SetText();

            stats[0].GetChild().SetText("Count");
            stats[1].GetChild().SetText("Owned");
            stats[2].GetChild().SetText();
            stats[3].GetChild().SetText(selectedItem.flavorShort);
            stats[4].GetChild().SetText();
        }
        else
        {
            stats[0].SetText(selectedItem.publicPower);
            stats[1].SetText(selectedItem.publicSpeed);
            stats[2].SetText(selectedItem.publicRange);
            stats[3].SetText(selectedItem.publicAccu);
            stats[4].SetText(selectedItem.publicCost);

            stats[0].GetChild().SetText("Power -");
            stats[1].GetChild().SetText("Speed -");
            stats[2].GetChild().SetText("Range -");
            stats[3].GetChild().SetText("Accu. -");
            stats[4].GetChild().SetText("Cost -");
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
        List<UiElement> activeMoveSpots = new List<UiElement>(); 

        switch(state)
        {
            case "display":
                activeMoveSpots = slots;
                break;
            case "topList":
                activeMoveSpots = topListItems;
                break;
            case "catagoryList":
                activeMoveSpots = catagories;
                break;
        }

        Vector3 bestPos = new Vector3(999999, 999999, 999999);
        UiElement bestSpot = null;
        float bestPri = 999999;
        int index = -1;

        foreach(UiElement spot in activeMoveSpots)
        {
            index++;

            Vector3 spotPosition = cursor.transform.parent.InverseTransformPoint(spot.transform.position);
            Vector3 cursorPosition = cursor.transform.localPosition;
            float distance = Vector3.Distance(spotPosition, cursorPosition);
            float priority = 999999;

            // Check if spot is valid, givin the direction
            if(spotPosition.x - cursorPosition.x > 0 == x < 0 && x != 0)
                continue;
            if(spotPosition.y - cursorPosition.y > 0 == y < 0 && y != 0)
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
                bestSpot = spot;
            }
        }

        if(bestSpot != null)
        {
            cursor.SetNewWaypoint(bestPos, 0.1f);
            cursor.transform.GetComponent<RectTransform>().sizeDelta = bestSpot.transform.GetComponent<RectTransform>().sizeDelta + new Vector2(20, 20);
        }
    }
    
    public void ForceCursorMove(UiElement spot)
    {
        Vector3 waypoint = cursor.transform.parent.InverseTransformPoint(spot.transform.position);
        Vector2 size = spot.transform.GetComponent<RectTransform>().sizeDelta + new Vector2(20, 20);
        cursor.SetNewWaypoint(waypoint, 0.05f);
        cursor.transform.GetComponent<RectTransform>().sizeDelta = size;
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

        return pri * distance;
    }
}
