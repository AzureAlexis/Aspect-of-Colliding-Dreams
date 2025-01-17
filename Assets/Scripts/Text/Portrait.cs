using System;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    // Public refrences
    public string charName;
    public Sprite[] emotions;
    public Portrait partner;

    // Bools used to make descicions about how to render
    public bool flipped;
    public bool active;
    public bool opening = true;
    public bool closing = false;

    // Vector to determine how big the textbox should be

    const float moveDistance = 28.842712475f;

    void Update()
    {
        UpdatePosition();
        UpdateColor();
        UpdateTextbox();
        UpdateStatus();
    }

    void UpdatePosition()
    {
        float speed = moveDistance * Time.deltaTime;
        Vector2 position = GetComponent<RectTransform>().anchoredPosition;
        Vector2 newPosition = GetComponent<RectTransform>().anchoredPosition;

        if(opening && !partner.Busy() && !flipped)
            newPosition = Vector2.Lerp(position, new Vector2(100, -200), speed * 0.5f);
        else if(opening && !partner.Busy() && flipped)
            newPosition = Vector2.Lerp(position, new Vector2(-100, -200), speed * 0.5f);
        else if(closing && !partner.Busy() && !Busy() && !flipped)
            newPosition = Vector2.Lerp(position, new Vector2(-400, -200), speed * 0.5f);
        else if(closing && !partner.Busy() && !Busy() && flipped)
            newPosition = Vector2.Lerp(position, new Vector2(400, -200), speed * 0.5f);
        else if(active && !partner.Busy() && !flipped)
            newPosition = Vector2.Lerp(position, new Vector2(300, 0), speed);
        else if(active && !partner.Busy() && flipped)
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
        RectTransform textbox = transform.GetChild(0).GetComponent<RectTransform>();

        if(active)
            textbox.localScale = Vector3.Min(textbox.localScale + new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime) * 10, Vector3.one);
        else if(!active)
            textbox.localScale = Vector3.Max(textbox.localScale - new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime) * 10, Vector3.zero);
    }

    void UpdateStatus()
    {
        if(GetComponent<RectTransform>().anchoredPosition.x >= 99 && !flipped && opening)
            opening = false;
        else if(GetComponent<RectTransform>().anchoredPosition.x <= -99 && flipped && opening)
            opening = false;
        if(GetComponent<RectTransform>().anchoredPosition.x <= -399 && !flipped && closing)
            Destroy(gameObject);
        else if(GetComponent<RectTransform>().anchoredPosition.x >= 399 && flipped && closing)
            Destroy(gameObject);
    }

    public void FirstActivation(bool isFlipped, Portrait isPartner)
    {
        flipped = isFlipped;
        partner = isPartner;

        if(flipped)
        {
            GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(400, -200);

            transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, -200);
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
        Vector2 padding = new Vector2(96, 144);

        textbox.sizeDelta = textSize + padding;

        
        if(flipped)
            tmp.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(textSize.x + 37, -37);
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
        closing = true;
    }

    public bool Busy()
    {
        return !(GetComponent<RectTransform>().anchoredPosition.y <= -199) && (opening || closing);
    }
}
