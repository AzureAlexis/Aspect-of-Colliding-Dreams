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
        GetComponent<RectTransform>().sizeDelta = new Vector2(openingTick * 900, openingTick * 900);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, openingTick * 180);
        openingTick = Mathf.Min(openingTick + Time.deltaTime, 1);
    }

    void UpdateClosing()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(openingTick * 900, openingTick * 900);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, openingTick * 180);
        openingTick = Mathf.Max(openingTick - Time.deltaTime, 0);
    }

}
