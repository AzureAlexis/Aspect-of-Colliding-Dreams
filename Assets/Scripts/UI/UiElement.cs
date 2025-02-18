using UnityEngine;

public class UiElement : MonoBehaviour
{
    public bool moving;
    public Vector2 oldWaypoint;
    public Vector2 newWaypoint;
    public float moveTime;
    public float maxTime;

    public void UpdatePosition()
    {
        if(moving)
        {
            moveTime += Time.smoothDeltaTime;

            float factor = 1 - (maxTime / moveTime);
            Vector2 position = AzalUtil.QuadOut(oldWaypoint, newWaypoint, factor);

            GetComponent<RectTransform>().anchoredPosition = position;

            if(moveTime >= maxTime)
                EndMove();
        }
    }

    public void StartMove(Vector2 waypoint, float time)
    {
        newWaypoint = waypoint;
        oldWaypoint = GetComponent<RectTransform>().anchoredPosition;
        maxTime = time;
        moveTime = 0;
        moving = true;
    }

    public void EndMove()
    {
        moving = false;
        GetComponent<RectTransform>().anchoredPosition = newWaypoint;
    }
}
