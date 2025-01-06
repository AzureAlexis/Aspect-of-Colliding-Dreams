using UnityEngine;

public class playerMove : MonoBehaviour
{
    PlayerStats playerStats;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = MakeMoveVector();
        UpdatePosition(moveVector);
        UpdateAnimation(moveVector);
    }

    void UpdatePosition(Vector3 moveVector) 
    {
        transform.position += moveVector;
    }

    void UpdateAnimation(Vector3 moveVector)
    {
        Vector2 animVector = new(0, 0);

        if(moveVector.x > 0)
        {
            animVector.x = 1;
        }
        else if(moveVector.x < 0)
        {
            animVector.x = -1;
        }
        
        if(moveVector.y > 0)
        {
            animVector.y = 1;
        }
        else if(moveVector.y < 0)
        {
            animVector.y = -1;
        }

        animator.SetFloat("X", animVector.x);
        animator.SetFloat("Y", animVector.y);
    }

    Vector3 MakeMoveVector()
    {
        Vector3 moveVector = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x += Time.deltaTime * playerStats.speed * 0.4f;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x -= Time.deltaTime * playerStats.speed * 0.4f;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.y += Time.deltaTime * playerStats.speed * 0.4f;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.y -= Time.deltaTime * playerStats.speed * 0.4f;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveVector /= 3;
        }
        return moveVector;
    }
}
