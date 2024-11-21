using System.ComponentModel.Design;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    static public bool active;

    public static bool IsActive()
    {
        return active;
    }
}
