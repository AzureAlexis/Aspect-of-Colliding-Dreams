using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiList : UiElement
{
    List<Transform> elements = new List<Transform>();
    Transform cursor;

    public Vector2 bounds;
    public int index;
    bool active = false;
    public bool looping = false;
    public string type = "horizontal";

    public Transform this[int index] {get => elements[index];}
    public int Count {get => elements.Count;}

    new void Start()
    {
        base.Start();

        foreach(Transform child in transform)
            elements.Add(child);
        
        cursor = GameObject.Instantiate(Resources.Load("ui/cursor") as GameObject, transform).transform;
        cursor.localPosition = transform.parent.InverseTransformPoint(elements[0].position);
        cursor.GetComponent<UiElement>().visible = visible;

        MoveCursor();
    }

    new void Update()
    {
        base.Update();
        if(active)
        {
            UpdateIndex();
            //UpdateElements();
        }
    }

    void UpdateIndex()
    {
        if(AnyArrowKeyDown())
        {
            switch(type)
            {
                case "horizontal":
                    MakeHorizontalInput();
                    break;
                case "vertical":
                    MakeVerticalInput();
                    break;
                case "closest":
                    MakeClosestInput();
                    break;
                default:
                    Debug.LogError("Tried to update a list without a valid type");
                    break;
            }
            MoveCursor();
        }
    }

    void UpdateElements()
    {
        for(int i = 0; i < elements.Count - 1; i++)
        {
            if(InBounds(elements[i]))
                elements[i].GetComponent<UiElement>().Activate();
            else
                elements[i].GetComponent<UiElement>().Deactivate();
        }
    }

    void MoveCursor()
    {
        UiElement script = cursor.GetComponent<UiElement>();
        Vector2 waypoint = cursor.transform.parent.InverseTransformPoint(elements[index].position);
        Vector2 diff = new Vector2(cursor.localPosition.x, cursor.localPosition.y) - waypoint;
        Vector2 size = elements[index].GetComponent<RectTransform>().sizeDelta;

        if(InBounds(SelectedElement().transform))
            script.SetNewWaypointAndSize(waypoint, size, 0.1f);
        else
        {
            for(int i = 0; i < elements.Count - 1; i++)
            {
                elements[i].GetComponent<UiElement>().SetRelativeWaypoint(diff, 0.1f);
            }
        }
    }

    bool InBounds(Transform element)
    {
        Vector3[] bounds = new Vector3[4];
        Vector3[] corners = new Vector3[4];

        transform.GetComponent<RectTransform>().GetWorldCorners(bounds);
        element.GetComponent<RectTransform>().GetWorldCorners(corners);

        if(corners[0].x < bounds[0].x || corners[0].y < bounds[0].y)
            return false;
        if(corners[1].x < bounds[1].x || corners[1].y > bounds[1].y)
            return false;
        if(corners[2].x > bounds[2].x || corners[2].y > bounds[2].y)
            return false;
        if(corners[3].x > bounds[3].x || corners[3].y < bounds[3].y)
            return false;
        else
            return true;
    }

    public void ActivateList()
    {
        active = true;
    }

    public void DeactivateList()
    {
        active = false;
    }

    public UiElement SelectedElement()
    {
        return elements[index].GetComponent<UiElement>();
    }

    public int SelectedIndex()
    {
        return index;
    }

    public void AddElement(Transform newElement)
    {
        elements.Add(newElement);
    }

    public void RemoveElement(int targetIndex = -1)
    {
        if(targetIndex == -1)
            targetIndex = elements.Count - 1;

        elements.RemoveAt(targetIndex);
    }

    public void Clear()
    {
        foreach(Transform element in elements)
            Destroy(element.gameObject);
        elements.Clear();
    }

    void MakeHorizontalInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
            index += 1;
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            index -= 1;

        if(index >= elements.Count)
        {
            if(looping)
                index = 0;
            else
                index = elements.Count;
        }
        else if(index < 0)
        {
            if(looping)
                index = elements.Count;
            else
                index = 0;
        }
    }
    void MakeVerticalInput()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
            index += 1;
        else if(Input.GetKeyDown(KeyCode.UpArrow))
            index -= 1;

        if(index >= elements.Count)
        {
            if(looping)
                index = 0;
            else
                index = elements.Count;
        }
        else if(index < 0)
        {
            if(looping)
                index = elements.Count;
            else
                index = 0;
        }
    }
    void MakeClosestInput()
    {
        Vector3 bestPos = new Vector3(999999, 999999, 999999);
        Transform bestSpot = null;
        float bestPri = Mathf.Infinity;
        int bestIndex = -1;

        foreach(Transform spot in elements)
        {
            bestIndex++;

            Vector3 spotPosition = cursor.transform.parent.InverseTransformPoint(spot.position);
            Vector3 cursorPosition = cursor.transform.localPosition;
            float distance = Vector3.Distance(spotPosition, cursorPosition);
            float priority = 999999;

            // Check if spot is valid, givin the direction
            if(!(spotPosition.x - cursorPosition.x > 0) && Input.GetKeyDown(KeyCode.RightArrow))
                continue;
            if(!(spotPosition.x - cursorPosition.x < 0) && Input.GetKeyDown(KeyCode.LeftArrow))
                continue;
            if(!(spotPosition.y - cursorPosition.y > 0) && Input.GetKeyDown(KeyCode.UpArrow))
                continue;
            if(!(spotPosition.y - cursorPosition.y < 0) && Input.GetKeyDown(KeyCode.DownArrow))
                continue;
            if(distance <= 1)
                continue;
            
            // Make priority based on distance and angle
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                priority = MakePriority(cursorPosition, spotPosition, distance, "y");
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                priority = MakePriority(cursorPosition, spotPosition, distance, "x");
            
            // Assign if priority is best
            if(priority < bestPri)
            {
                bestPri = priority;
                index = bestIndex;
            }
        }
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
