using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;
    public static bool inBattle = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerManager.player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void EnterBattle()
    {
        inBattle = true;
        player.GetComponent<playerMove>().EnterBattle();
    }

    public static void ExitBattle()
    {
        inBattle = false;
        player.GetComponent<playerMove>().ExitBattle();
    }
}
