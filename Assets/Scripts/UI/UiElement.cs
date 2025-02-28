using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UiElement : MonoBehaviour
{
    public bool moving;
    public bool visible = true;
    public Vector2 oldWaypoint;
    public Vector2 newWaypoint;
    public float moveTime;
    public float maxTime;

    public Vector3[] storedWaypoints;

    public void Update()
    {
        UpdatePosition();
        UpdateVisibility();
    }
    
    public void UpdatePosition()
    {
        if(moving)
        {
            moveTime += Time.smoothDeltaTime;

            float factor = moveTime * (1 / maxTime);
            Vector2 position = AzalUtil.QuadOut(oldWaypoint, newWaypoint, factor);

            GetComponent<RectTransform>().anchoredPosition = position;

            if(moveTime >= maxTime)
                EndMove();
        }
    }

    public void UpdateVisibility()
    {
        int target;
        Color color;

        if(visible)
            target = 1;
        else
            target = -1;

        if(GetComponent<Image>() != null)
        {
            color = GetComponent<Image>().color;
            color.a = AzalUtil.Range(color.a += Time.deltaTime * 4 * target, 1, 0);
            GetComponent<Image>().color = color;
        }
        if(GetComponent<TextMeshProUGUI>() != null)
        {
            color = GetComponent<TextMeshProUGUI>().color;
            color.a = AzalUtil.Range(color.a += Time.deltaTime * 4 * target, 1, 0);
            GetComponent<TextMeshProUGUI>().color = color;
        }
    }

    public void SetNewWaypoint(Vector2 waypoint, float time)
    {
        newWaypoint = waypoint;
        oldWaypoint = GetComponent<RectTransform>().anchoredPosition;
        maxTime = time;
        moveTime = 0;
        moving = true;
    }

    public void SetStoredWaypoint(int index, float time)
    {
        newWaypoint = storedWaypoints[index];
        oldWaypoint = GetComponent<RectTransform>().anchoredPosition;
        maxTime = time;
        moveTime = 0;
        moving = true;
    }

    public void Deactivate(float time)
    {
        SetStoredWaypoint(0, time);
        visible = false;
    }

    public void Activate(float time)
    {
        SetStoredWaypoint(1, time);
        visible = true;
    }

    public void Home(float time)
    {
        SetStoredWaypoint(0, time);
    }

    public void EndMove()
    {
        moving = false;
        GetComponent<RectTransform>().anchoredPosition = newWaypoint;
    }
}
