using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static GameObject battlebox;
    public static GameObject cam;

    void Start()
    {
        battlebox = GameObject.Find("Battlebox");
        cam = GameObject.Find("Camera");
    }

    public static void UpdateBossHp(float hp, float mhp)
    {
        RectTransform bossHp = battlebox.transform.GetChild(0).GetComponent<RectTransform>();
        bossHp.sizeDelta = new Vector2((hp / mhp) * 1000, 10);
    }
}
