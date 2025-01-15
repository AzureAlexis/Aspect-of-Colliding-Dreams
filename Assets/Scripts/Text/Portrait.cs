using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    // Public refrences
    public string charName;
    public Sprite[] emotions;

    // Bools used to make descicions about how to render
    public bool flipped;
    public bool active;
    public bool done = false;

    // Vector to determine how big the textbox should be
    Vector2 textboxSize;

    const float moveDistance = 282.842712475f;

    void Update()
    {
        UpdatePosition();
        UpdateColor();
        UpdateTextbox();
    }

    void UpdatePosition()
    {
        float speed = moveDistance * Time.deltaTime * 0.1f;
        Vector2 position = GetComponent<RectTransform>().anchoredPosition;
        Vector2 newPosition = GetComponent<RectTransform>().anchoredPosition;

        if(active && !flipped)
            newPosition = Vector2.Lerp(position, new Vector2(300, 0), speed);
        else if(active && flipped)
            newPosition = Vector2.Lerp(position, new Vector2(-300, 0), speed);
        else if(!active && !flipped)
            newPosition = Vector2.Lerp(position, new Vector2(100, -200), speed);
        else if(!active && flipped)
            newPosition = Vector2.Lerp(position, new Vector2(-100, -200), speed);


        GetComponent<RectTransform>().anchoredPosition = newPosition;
    }

    void UpdateColor()
    {
        float newColor = GetComponent<Image>().color.r;

        if(active)
            newColor = Mathf.Min(newColor + Time.deltaTime * 3, 1f);
        else if(!active)
            newColor = Mathf.Max(newColor - Time.deltaTime * 3, 0.40f);

        GetComponent<Image>().color = new Color(newColor, newColor, newColor, 1);
    }

    void UpdateTextbox()
    {
        float speed = moveDistance * Time.deltaTime * 0.1f;
        RectTransform textbox = transform.GetChild(0).GetComponent<RectTransform>();

        if(active)
            textbox.localScale = Vector2.Lerp(textbox.sizeDelta, textboxSize, speed);
        else if(!active)
            textbox.localScale = Vector2.Lerp(textbox.sizeDelta, new Vector2(0, 0), speed);
    }

    public void FirstActivation(bool isFlipped)
    {
        flipped = isFlipped;

        if(flipped)
        {
            GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, -200);
        }
        else
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(200, -200);
        }

    }

    public void SetEmotion(string id)
    {
        for(int i = 0; i < emotions.Length; i++)
        {
            if(emotions[i].name == id)
            {
                GetComponent<Image>().sprite = emotions[i];
                return;
            }
        }
    }

    void SetText(string text)
    {
        RectTransform textbox = transform.GetChild(0).GetComponent<RectTransform>();
        TextMeshProUGUI tmp = textbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        tmp.SetText(text);
        tmp.ForceMeshUpdate();

        Vector2 textSize = tmp.GetRenderedValues(false);
        Vector2 padding = new Vector2(96, 192);

        textboxSize = textSize + padding;
    }

    public void Activate()
    {
        active = true;
    }

    public void Activate(string text, string emotion)
    {
        active = true;
        SetEmotion(emotion);
        SetText(text);
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
