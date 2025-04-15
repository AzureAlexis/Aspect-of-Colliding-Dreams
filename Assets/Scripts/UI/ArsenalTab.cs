using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ArsenalTab : MenuTab
{
    public string state = "display";
    int selectedSlotIndex = 0;
    BattleSlotBase selectedItem;
    UiElement itemName;
    UiList arsenalSlotDisplay;
    UiElement statList;
    UiList topList;
    UiList catagoryList;
    UiList itemsList;
    List<UiElement> slots = new List<UiElement>();
    List<UiElement> stats = new List<UiElement>();
    List<UiElement> topListItems = new List<UiElement>();
    List<UiElement> catagories = new List<UiElement>();

    void Start()
    {
        itemName = GameObject.Find("ShotName").GetComponent<UiElement>();
        arsenalSlotDisplay = GameObject.Find("ArsenalSlotDisplay").GetComponent<UiList>();
        statList = GameObject.Find("Stat List").GetComponent<UiElement>();
        topList = GameObject.Find("Top List").GetComponent<UiList>();
        catagoryList = GameObject.Find("Item Catagories").GetComponent<UiList>();
        itemsList = GameObject.Find("Item List").GetComponent<UiList>();

        foreach(Transform child in GameObject.Find("ArsenalSlotDisplay").transform)
            slots.Add(child.GetComponent<UiElement>());

        foreach(Transform child in GameObject.Find("Stat List").transform)
            stats.Add(child.GetComponent<UiElement>());

        foreach(Transform child in GameObject.Find("Top List").transform)
            topListItems.Add(child.GetComponent<UiElement>());

        foreach(Transform child in GameObject.Find("Item Catagories").transform)
            catagories.Add(child.GetComponent<UiElement>());

        arsenalSlotDisplay.ActivateList();
    }

    new public void Update()
    {
        base.Update();
        if(UIManager.state == "menu")
        {
            if(UIManager.currentTab == "arsenal")
                UpdateActive();
            else
                UpdateInactive();
        }
    }

    new public void UpdateActive()
    {
        base.UpdateActive();
        UpdateAction();
        if(state == "display")
            UpdateDisplay();
        if(state == "itemList" || state == "catagoryList")
            UpdateItems();
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
                    switch(topList.index)
                    {
                        case 0:
                            newState = "catagoryList";
                            action = "refresh";
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
                    newState = "itemList";
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
                
                case "itemList":
                    newState = "display";
                    action = "equip";
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
                case "itemList":
                    newState = "catagoryList";
                    break;
            }
        }

        if(action != null)
            DoArsenalAction(action);

        if(newState != null)
            ChangeDisplayState(newState);
    }

    void ChangeDisplayState(string newState)
    {
        switch(newState)
        {
            case "display":
                topList.DeactivateAll(0.25f, 2);
                arsenalSlotDisplay.ActivateAll();
                statList.ActivateAll();
                itemsList.DeactivateAll();

                arsenalSlotDisplay.ActivateList();
                topList.DeactivateList();
                itemsList.DeactivateList();

                arsenalSlotDisplay.SelectedElement().Activate(0.25f, 0);

                break;

            case "topList":
                topList.ActivateAll(0.25f, 1);
                catagoryList.DeactivateAll(0.25f, 2);

                catagoryList.DeactivateAll();
                arsenalSlotDisplay.DeactivateAll();
                statList.DeactivateAll();

                topList.ActivateList();
                arsenalSlotDisplay.DeactivateList();
                catagoryList.DeactivateList();
                
                
                arsenalSlotDisplay.SelectedElement().ActivateAll(0.25f, 1);

                break;

            case "catagoryList":
                catagoryList.ActivateAll(0.25f, 1);
                topList.DeactivateAll(0.25f, 0);
                itemsList.DeactivateAll();

                catagoryList.ActivateList();
                topList.DeactivateList();
                itemsList.DeactivateList();

                break;

            case "itemList":
                itemsList.ActivateAll();
                catagoryList.DeactivateAll(0.25f, 0);

                itemsList.ActivateList();
                catagoryList.DeactivateList();

                break;
        }
        state = newState;
    }

    void DoArsenalAction(string action)
    {
        switch(action)
        {
            case "equip":
                switch(catagoryList.SelectedIndex())
                {
                    case 0:
                        EquipItem(PlayerStats.shots[itemsList.SelectedIndex()]);
                        break;
                    case 1:
                        EquipItem(PlayerStats.spells[itemsList.SelectedIndex()]);
                        break;
                    case 2:
                        EquipItem(PlayerStats.consumables[itemsList.SelectedIndex()]);
                        break;
                }
                break;

            case "refresh":
                RefreshItemList();
                break;
        }
    }

    void RefreshItemList()
    {
        itemsList.Clear();
    }

    void EquipItem(BattleSlotBase item)
    {
        int index = arsenalSlotDisplay.SelectedIndex();
        BattleSlotBase oldItem = PlayerStats.battleSlots[index];
        BattleSlotBase newItem = item;
        Transform oldItemTransform = slots[index].transform;
        Transform newItemTransform = itemsList.SelectedElement().GetChild(3).transform;
        Vector2 oldItemPos = oldItemTransform.position;
        Vector2 newItemPos = newItemTransform.position;
        Vector2 slotPos = slots[index].storedWaypoints[0];
        
        PlayerStats.battleSlots.Insert(index, newItem);
        switch(oldItem.GetType().ToString())
        {
            case "PlayerAttack":
                PlayerStats.shots.Insert(0, (PlayerAttack)oldItem);
                break;
            case "PlayerSpell":
                PlayerStats.spells.Insert(0, (PlayerSpell)oldItem);
                break;
            case "Consumable":
                PlayerStats.consumables.Insert(0, (Consumable)oldItem);
                break;
        }

        PlayerStats.battleSlots.RemoveAt(index + 1);
        switch(newItem.GetType().ToString())
        {
            case "PlayerAttack":
                PlayerStats.shots.Remove((PlayerAttack)newItem);
                break;
            case "PlayerSpell":
                PlayerStats.spells.Remove((PlayerSpell)newItem);
                break;
            case "Consumable":
                PlayerStats.consumables.Remove((Consumable)newItem);
                break;
        }

        newItemTransform.position = oldItemPos;
        oldItemTransform.position = newItemPos;

        oldItemTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(92, 92);
        newItemTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);

        slots[index].SetNewWaypointAndSize(slotPos, new Vector2(120, 120), 0.25f, 0);
        itemsList.SelectedElement().GetComponent<UiElement>().Deactivate();
    }
    
    void UpdateDisplay()
    {
        selectedItem = PlayerStats.battleSlots[arsenalSlotDisplay.index];
        for(int i = 0; i < 7; i++)
        {
            Debug.Log(slots[i].transform.childCount);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = PlayerStats.battleSlots[i].sprite;

        }
        
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
            stats[4].GetChild().SetText("Cost  -");
        }
    }

    void UpdateItemList()
    {
        switch(catagoryList.SelectedIndex())
        {
            case 0:
                for(int i = 0; i < PlayerStats.shots.Count; i++)
                {
                    itemsList[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerStats.shots[i].name;
                    itemsList[i].GetChild(3).GetChild(0).GetComponent<Image>().sprite = PlayerStats.shots[i].sprite;
                }
                break;
            case 1:
                for(int i = 0; i < PlayerStats.spells.Count; i++)
                {
                    itemsList[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerStats.spells[i].name;
                    itemsList[i].GetChild(3).GetChild(0).GetComponent<Image>().sprite = PlayerStats.spells[i].sprite;
                }
                break;
            case 2:
                for(int i = 0; i < PlayerStats.consumables.Count; i++)
                {
                    itemsList[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerStats.consumables[i].name;
                    itemsList[i].GetChild(3).GetChild(0).GetComponent<Image>().sprite = PlayerStats.consumables[i].sprite;
                }
                break;
        }
    }

    void UpdateItems()
    {
        MakeItemList();
        UpdateItemList();
    }

    void MakeItemList()
    {
        int diff = 0;
        switch(catagoryList.SelectedIndex())
        {
            case 0:
                diff = PlayerStats.shots.Count - itemsList.Count;
                break;
            case 1:
                diff = PlayerStats.spells.Count - itemsList.Count;
                break;
            case 2:
                diff = PlayerStats.consumables.Count - itemsList.Count;
                break;
        }

        if(diff > 0)
        {
            for(int i = 0; i < diff; i++)
            {
                Transform newElement = Instantiate(Resources.Load("ui/uiItem") as GameObject, itemsList.transform, false).transform;
                newElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, -100 * itemsList.Count - 46);
                Debug.Log(newElement.localPosition);
                itemsList.AddElement(newElement);
            }
        }
        else if(diff < 0)
        {
            for(int i = 0; i > diff; i--)
            {
                Transform targetElement = itemsList[itemsList.Count - 1];
                itemsList.RemoveElement();
                Destroy(targetElement.gameObject);
            }
        }
    }

}
