using UnityEngine;
using UnityEngine.Diagnostics;

public class playerMove : MonoBehaviour
{
    PlayerStats playerStats;
    Animator animator;

    // Vars related to determining player control
    bool canControl = true;     // Can the player control themselves? Used for cutscenes

    // Vars related to forced movement
    public Vector3 forcedMoveTarget;   // Where the player is being forced to move to
    float forcedMoveSpeed;      // How fast the player force-moves
    float forcedMoveTime;       // How long until forced movement ends
    Vector3 oldPosition;

    public void ForceMovement(Vector3 target, float time)
    {
        canControl = false;
        oldPosition = transform.position;
        forcedMoveTarget = target;
        forcedMoveTime = time;
        forcedMoveSpeed = Vector3.Distance(target, transform.position);
    }
    public void EnterBattle()
    {
        ForceMovement(UIManager.cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3((float)(Screen.width * 0.5), (float)(Screen.height * 0.1))) + new Vector3(0, 0, 10), 1);
    }
    public void ExitBattle()
    {
        ForceMovement(oldPosition, 1);
    }

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(canControl && PlayerManager.inBattle)
        {
            DoBattleMovement();
        }
        else if(canControl && !PlayerManager.inBattle)
        {
            DoMapMovement();
        }
        else if(!canControl)
        {
            DoForcedMovement();
            UpdateForcedMovement();
        }
    }
    void DoMapMovement()
    {
        transform.position += MakeMoveVector();
        UpdateAnimation(MakeMoveVector());
    }
    void DoBattleMovement()
    {
        DoMapMovement();

        Vector3 bounds = UIManager.cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3((float)(Screen.width * 0.5), (float)(Screen.height * 0.5)));
        if(transform.position.x > bounds.x + 3.3)
        {
            transform.position = new Vector3(bounds.x + 3.3f, transform.position.y, 0);
        }
        if(transform.position.x < bounds.x - 3.3)
        {
            transform.position = new Vector3(bounds.x - 3.3f, transform.position.y, 0);
        }
        if(transform.position.y > bounds.y + 2.8)
        {
            transform.position = new Vector3(transform.position.x, bounds.y + 2.8f, 0);
        }
        if(transform.position.y < bounds.y - 3.7)
        {
            transform.position = new Vector3(transform.position.x, bounds.y - 3.7f, 0);
        }
    }
    void DoForcedMovement()
    {
        transform.position = Vector3.Lerp(transform.position, forcedMoveTarget, forcedMoveSpeed * Time.deltaTime);
    }
    void UpdateForcedMovement()
    {
        forcedMoveTime -= Time.deltaTime;
        if(forcedMoveTime <= 0)
        {
            canControl = true;
        }
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
