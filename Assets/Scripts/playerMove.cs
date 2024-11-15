using UnityEngine;

public class playerMove : MonoBehaviour
{
    PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = MakeMoveVector();
        UpdatePosition(moveVector);
    }

    void UpdatePosition(Vector3 moveVector) 
    {
        transform.position += moveVector;
    }

    Vector3 MakeMoveVector()
    {
        Vector3 moveVector = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x += Time.deltaTime * playerStats.speed;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x -= Time.deltaTime * playerStats.speed;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.y += Time.deltaTime * playerStats.speed;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.y -= Time.deltaTime * playerStats.speed;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveVector /= 3;
        }
        return moveVector;
    }
}
