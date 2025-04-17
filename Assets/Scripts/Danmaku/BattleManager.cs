using System.ComponentModel.Design;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    static public bool active;
    public bool activate;

    void Update()
    {
        if(activate != BattleManager.active)
        {
            if(activate)
            {
                BattleManager.EnterBattle();
            }
            else
            {
                BattleManager.ExitBattle();
            }
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
