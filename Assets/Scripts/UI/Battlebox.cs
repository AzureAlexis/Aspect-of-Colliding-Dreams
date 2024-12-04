using UnityEngine;

public class Battlebox : MonoBehaviour
{
    float openingTick = 0;
    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        if(openingTick != 1 && active)
        {
            UpdateOpening();
        }
        if(openingTick != 0 && !active)
        {
            UpdateClosing();
        }
    }

    void UpdateOpening()
    {
        openingTick = Mathf.Min(openingTick + Time.deltaTime, 1);
        GetComponent<RectTransform>().sizeDelta = new Vector2(openingTick * 1000, openingTick * 1000);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, openingTick * 180);
    }

    void UpdateClosing()
    {
        openingTick = Mathf.Max(openingTick - Time.deltaTime, 0);
        GetComponent<RectTransform>().sizeDelta = new Vector2(openingTick * 1000, openingTick * 1000);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, openingTick * 180);
    }

}
