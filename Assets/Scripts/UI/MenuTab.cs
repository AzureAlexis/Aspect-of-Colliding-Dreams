using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MenuTab : UiElement
{
    public void Update()
    {
        base.Update();
        ScrollTab();
    }
    
    public void UpdateActive()
    {
        UpdateReminders(false);
    }

    public void UpdateInactive()
    {
        UpdateReminders(true);
    }

    void UpdateReminders(bool active)
    {
        Transform tabGuide = transform.GetChild(transform.childCount - 1);
        tabGuide.GetComponent<UiElement>().visible = active;
        tabGuide.GetChild(0).GetComponent<UiElement>().visible = active;
        tabGuide.GetChild(0).GetChild(0).GetComponent<UiElement>().visible = active;
        tabGuide.GetChild(1).GetComponent<UiElement>().visible = active;
        tabGuide.GetChild(1).GetChild(0).GetComponent<UiElement>().visible = active;
    }

    public void ScrollTab()
    {
        RectTransform rect = GetComponent<RectTransform>();
        bool scrollDirection = newWaypoint.y > oldWaypoint.y;

        if(rect.anchoredPosition.y > 900 && scrollDirection)
        {
            rect.anchoredPosition -= new Vector2(0, 2250);
            newWaypoint -= new Vector2(0, 2250);
            oldWaypoint -= new Vector2(0, 2250);
        }
        else if(rect.anchoredPosition.y < -900 && !scrollDirection)
        {
            rect.anchoredPosition += new Vector2(0, 2250);
            newWaypoint += new Vector2(0, 2250);
            oldWaypoint += new Vector2(0, 2250);
        }
    }

    public void StartScroll(float distance)
    {
        Vector2 position = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, distance);
        SetNewWaypoint(position, 0.5f);
    }
}
