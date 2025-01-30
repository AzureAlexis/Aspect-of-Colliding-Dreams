using UnityEngine;
using UnityEngine.U2D;

public class UIManager : MonoBehaviour
{
    public static GameObject battlebox;
    public static GameObject cam;
    public static GameObject inner;
    public static GameObject outer;
    public static GameObject grid;
    public static float tick = 0;


    static float innerAlpha = 0;
    static float outerAlpha = 0;
    static float gridAlpha = 0.5f;
    static Color effectColor = Color.white;

    void Start()
    {
        UIManager.battlebox = GameObject.Find("Battlebox");
        UIManager.cam = GameObject.Find("Camera");
        UIManager.inner = GameObject.Find("InnerCircle");
        UIManager.outer = GameObject.Find("OuterCircle");
        UIManager.grid = GameObject.Find("Grid");
    }

    void Update()
    {
        // UIManager.UpdateStatic();
    }

    public static void UpdateStatic()
    {
        tick += Time.deltaTime;

        grid.transform.localEulerAngles = new Vector3(-75, 0, tick * 45);
        inner.transform.localEulerAngles = new Vector3(Mathf.Sin(tick / 2) * 30 - 50, 45, tick * 45);
        outer.transform.localEulerAngles = new Vector3(Mathf.Cos(tick / 2) * 30 - 50, 45, tick * -45);

        Color gridColor = effectColor;
        Color innerColor = effectColor;
        Color outerColor = effectColor;
        
        gridColor.a = gridAlpha;
        innerColor.a = innerAlpha;
        outerColor.a = outerAlpha;

        grid.GetComponent<SpriteRenderer>().color = Color.Lerp(grid.GetComponent<SpriteRenderer>().color, gridColor, Time.deltaTime);
        inner.GetComponent<SpriteRenderer>().color = Color.Lerp(inner.GetComponent<SpriteRenderer>().color, innerColor, Time.deltaTime);
        outer.GetComponent<SpriteRenderer>().color = Color.Lerp(outer.GetComponent<SpriteRenderer>().color, outerColor, Time.deltaTime);
    }

    public static void UpdateBossHp(float hp, float mhp)
    {
        RectTransform bossHp = battlebox.transform.GetChild(0).GetComponent<RectTransform>();
        bossHp.sizeDelta = new Vector2((hp / mhp) * 1000, 10);
    }

    public static void UpdatePlayerHp(float hp)
    {
        RectTransform playerHp = battlebox.transform.GetChild(1).GetComponent<RectTransform>();
        playerHp.sizeDelta = new Vector2((hp / 5) * 1000, 10);
    }

    public static void ChangeBackgroundStatus(int id)
    {
        switch(id)
        {
            case 0:
                innerAlpha = 0;
                outerAlpha = 0;
                gridAlpha = 0.5f;
                effectColor = Color.white;
                break;

            case 1:
                innerAlpha = 0;
                outerAlpha = 0;
                gridAlpha = 1;
                effectColor = Color.white;
                break;
            
            case 2:
                innerAlpha = 1;
                outerAlpha = 1;
                gridAlpha = 1;
                effectColor = Color.cyan;
                break;

            case 3:
                innerAlpha = 1;
                outerAlpha = 1;
                gridAlpha = 1;
                effectColor = Color.red;
                break;

            case 4:
                innerAlpha = 1;
                outerAlpha = 1;
                gridAlpha = 1;
                effectColor = Color.magenta;
                GameObject.Find("Player").GetComponent<PlayerStats>().hit = false;
                break;
        }
    }
}
