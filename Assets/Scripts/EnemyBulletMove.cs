using UnityEngine;
using System;
using Unity.VisualScripting;

public class EnemyBulletMove : MonoBehaviour
{
    EnemyBulletStats bulletStats;
    Camera cam;
    private bool destroy = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletStats = GetComponent<EnemyBulletStats>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateOutOfBounds();
        UpdateDestroy();
    }

    void UpdateMovement()
    {
        Vector3 moveVector;
        moveVector = transform.right;
        moveVector *= bulletStats.speed;
        moveVector *= Time.deltaTime;

        transform.position += moveVector;
    }

    void UpdateOutOfBounds()
    {
        Vector3 relativePosition = cam.WorldToViewportPoint(transform.position);
        if(Mathf.Abs(relativePosition.x) > 1.1)
        {
            destroy = true;
        }
        else if(Mathf.Abs(relativePosition.y) > 1.1)
        {
            destroy = true;
        }
    }

    void UpdateDestroy()
    {
        if(destroy)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            destroy = true;
        }
    }


}