using UnityEngine;
using System;

public class PlayerDanmakuStats : MonoBehaviour
{
    public float cooldown; // The cooldown (in seconds) before this can be fired again
    public float speed; // Initial speed of the danmaku (in unity units/sec)

    // This class doesn't really do anythin on it's own, it just holds stats about a danmaku for other
    // scripts to use
}
