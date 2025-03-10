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

    public void Start()
    {
        if(storedWaypoints.Length > 0)
            storedWaypoints[0] = transform.localPosition;
    }
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

    public void Deactivate()
    {
        visible = false;
    }

    public void Deactivate(float time, int waypoint)
    {
        SetStoredWaypoint(waypoint, time);
        visible = false;
    }

    public void DeactivateAll()
    {
        Deactivate();
        if(transform.childCount > 0)
            foreach(Transform child in transform)
                if(child.GetComponent<UiElement>() != null)
                    child.GetComponent<UiElement>().DeactivateAll();
    }

    public void Activate()
    {
        visible = true;
    }

    public void Activate(float time, int waypoint)
    {
        SetStoredWaypoint(waypoint, time);
        visible = true;
    }

    public void ActivateAll()
    {
        Activate();
        if(transform.childCount > 0)
            foreach(Transform child in transform)
                if(child.GetComponent<UiElement>() != null)
                    child.GetComponent<UiElement>().ActivateAll();
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

    public UiElement GetChild(int id = 0)
    {
        return transform.GetChild(id).GetComponent<UiElement>();
    }

    public TextMeshProUGUI GetChildTMP(int id = 0)
    {
        return transform.GetChild(id).GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text = "")
    {
        transform.GetComponent<TextMeshProUGUI>().text = text;
    }
}
