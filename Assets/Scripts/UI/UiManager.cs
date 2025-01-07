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
}
