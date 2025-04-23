using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Image circleInner;
    Image circleOuter;
    Image lineInner; 
    Image lineOuter; 
    Image anima;

    float hp;
    float mhp;

    void Start()
    {
        circleOuter = transform.GetChild(0).GetComponent<Image>();
        circleInner = circleOuter.transform.GetChild(0).GetComponent<Image>();
        lineOuter = transform.GetChild(1).GetComponent<Image>();
        lineInner = lineOuter.transform.GetChild(0).GetComponent<Image>();
        anima = transform.GetChild(2).GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        UpdateStats();
        UpdateCircle();
        UpdateLine();
    }

    void UpdateStats()
    {
        hp = PlayerStats.hp;
        mhp = PlayerStats.mhp;
    }

    void UpdateCircle()
    {
        circleOuter.fillAmount = Mathf.Clamp((mhp + 2) / 60, 0, 1);
        circleInner.fillAmount = Mathf.Clamp(hp / 60, 0, 1);
    }

    void UpdateLine()
    {
        lineOuter.fillAmount = Mathf.Clamp((mhp - 60) / 140, 0, 1);
        lineInner.fillAmount = Mathf.Clamp((hp - 60) / 140, 0, 1);
    }
}
