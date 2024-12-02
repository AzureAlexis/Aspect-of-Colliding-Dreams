using UnityEngine;
using System;

public class EnemyBulletStats : MonoBehaviour
{
    public float speed = 1;                     // How fast the bullet will move when this movement is triggered (unity units/sec)
    public float acc = 0;                       // How fast the bullet accelerates (unity units/sec^2)

    // This class doesn't really do anythin on it's own, it just holds stats about a bullet for other
    // scripts to use
}
