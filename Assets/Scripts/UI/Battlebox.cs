using UnityEngine;

public class Battlebox : MonoBehaviour
{
    float openingTick = 0;
    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        if(openingTick != 1 && active)
        {
            UpdateOpening();
        }
        if(openingTick != 0 && !active)
        {
            UpdateClosing();
        }
    }

    void UpdateState()
    {
        active = BattleManager.active;
    }

    void UpdateOpening()
    {
        openingTick = Mathf.Min(openingTick + Time.deltaTime, 1);
        GetComponent<RectTransform>().localScale = new Vector2(openingTick, openingTick);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, openingTick * 360);
    }

    void UpdateClosing()
    {
        openingTick = Mathf.Max(openingTick - Time.deltaTime, 0);
        GetComponent<RectTransform>().sizeDelta = new Vector2(openingTick * 1000, openingTick * 1000);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, openingTick * 360);
    }

}
