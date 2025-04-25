using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Image circleInner;
    Image circleOuter;
    Image lineInner; 
    Image lineOuter; 
    Image animaInner;
    Image animaOuter;

    public float hp;
    public float mhp;
    public float ap;
    public float map;

    void Start()
    {
        circleOuter = transform.GetChild(0).GetComponent<Image>();
        circleInner = circleOuter.transform.GetChild(0).GetComponent<Image>();
        lineOuter = transform.GetChild(1).GetComponent<Image>();
        lineInner = lineOuter.transform.GetChild(0).GetComponent<Image>();
        animaOuter = transform.GetChild(2).GetComponent<Image>();
        animaInner = transform.GetChild(2).GetChild(0).GetComponent<Image>();

        hp = PlayerStats.hp;
        mhp = PlayerStats.mhp;
        ap = PlayerStats.ap;
        map = PlayerStats.map;
    }

    void Update()
    {
        UpdateStats();
        UpdateCircle();
        UpdateLine();
        UpdateAnima();
    }

    void UpdateStats()
    {
        hp = PlayerStats.hp;
        mhp = PlayerStats.mhp;
        ap = PlayerStats.ap;
        map = PlayerStats.map;
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

    void UpdateAnima()
    {
        animaOuter.fillAmount = Mathf.Clamp((map + 2) / 200, 0, 1);
        animaInner.fillAmount = Mathf.Clamp(ap / 200, 0, 1);
    }
}
