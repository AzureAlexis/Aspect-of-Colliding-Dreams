using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    static public bool active;
    public bool activate;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (activate != BattleManager.active)
        {
            if (activate)
            {
                BattleManager.EnterBattle();
            }
            else
            {
                BattleManager.ExitBattle();
            }
        }

        if (PlayerStats.player.scene != SceneManager.GetActiveScene())
        {
            SceneManager.MoveGameObjectToScene(PlayerStats.player, SceneManager.GetActiveScene());
        }
    }

    static void EnterBattle()
    {
        active = true;
        PlayerManager.EnterBattle();
    }

    static void ExitBattle()
    {
        active = false;
        PlayerManager.ExitBattle();
    }

    public static bool IsActive()
    {
        return active;
    }
}
