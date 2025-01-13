using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    public string charName;
    public Image[] emotions;
    public bool flipped;
    public bool active;
    public bool done = false;
    public float activeness;
    const float moveDistance = 282.842712475f;

    void Start()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if(active)
        {
            float speed = moveDistance * Time.deltaTime;
            Vector2 position = GetComponent<RectTransform>().anchoredPosition;
            Vector2 target = new Vector2(200, 0);
        }
    }

    public void FirstActivation(bool isFlipped)
    {
        flipped = isFlipped;
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        if(flipped)
        {
            GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(1420, -200);
        }
        else
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -200);
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    public void Done()
    {
        active = false;
    }
}
