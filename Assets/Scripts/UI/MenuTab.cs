using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MenuTab : UiElement
{
    public void Update()
    {
        UpdatePosition();
    }

    public void UpdateActive()
    {
        UpdateReminders(-1);
    }

    public void UpdateInactive()
    {
        UpdateReminders(1);
    }

    void UpdateReminders(int target)
    {
        Transform tabGuide = transform.GetChild(transform.childCount - 1);
        Color color = tabGuide.GetComponent<Image>().color;
        color.a += Time.deltaTime * 2 * target;
        color.a = Mathf.Min(1, color.a);
        color.a = Mathf.Max(0, color.a);

        tabGuide.GetComponent<Image>().color = color;
        tabGuide.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
        tabGuide.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = color;
        tabGuide.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = color;
        tabGuide.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = color;

    }

    public new void UpdatePosition()
    {
        base.UpdatePosition();
        ScrollTab();
        
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
        StartMove(position, 1f);
    }
}
