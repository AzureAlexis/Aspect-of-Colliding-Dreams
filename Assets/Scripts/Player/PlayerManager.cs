using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;
    public static bool inBattle = false;

    void Start()
    {
        PlayerManager.player = gameObject;
    }
    void Update()
    {
        PlayerManager.UpdateStatic(gameObject);
    }

    static void UpdateStatic(GameObject playerObj)
    {
        AssignPlayerReferences(playerObj);
        PlayerShoot.Update();
        PlayerMove.Update();
        PlayerStats.Update();
    }

    static public void AssignPlayerReferences(GameObject playerObj)
    {
        PlayerManager.player = playerObj;
        PlayerShoot.player = playerObj;
        PlayerMove.player = playerObj;
        PlayerStats.player = playerObj;
    }

    public static void EnterBattle()
    {
        inBattle = true;
        PlayerMove.EnterBattle();
        player.GetComponent<Animator>().SetBool("Battle", true);
    }

    public static void ExitBattle()
    {
        inBattle = false;
        PlayerMove.ExitBattle();
        player.GetComponent<Animator>().SetBool("Battle", false);
    }
}
