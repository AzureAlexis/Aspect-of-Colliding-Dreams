using UnityEngine;
using System;

public class PlayerBulletMove : MonoBehaviour
{
    PlayerBulletStats bulletStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletStats = GetComponent<PlayerBulletStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        Vector3 moveVector;
        moveVector = transform.right;
        moveVector *= bulletStats.speed;
        moveVector *= Time.deltaTime;

        transform.position += moveVector;
    }


}
