using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerMove
{
    // Refrences
    public static GameObject player;
    static PlayerStats playerStats;
    static Animator animator;

    // Vars related to determining player control
    static bool canControl = true;     // Can the player control themselves? Used for cutscenes

    // Vars related to forced movement
    static public Vector3 forcedMoveTarget;   // Where the player is being forced to move to
    static float forcedMoveSpeed;      // How fast the player force-moves
    static float forcedMoveTime;       // How long until forced movement ends
    static Vector3 oldPosition;

    static public void ForceMovement(Vector3 target, float time)
    {
        canControl = false;
        oldPosition = player.transform.position;
        forcedMoveTarget = target;
        forcedMoveTime = time;
        forcedMoveSpeed = Vector3.Distance(target, player.transform.position);
    }
    public static void EnterBattle()
    {
        ForceMovement(UIManager.cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3((float)(Screen.width * 0.5), (float)(Screen.height * 0.1))) + new Vector3(0, 0, 10), 1);
    }
    public static void ExitBattle()
    {
        ForceMovement(oldPosition, 1);
    }

    public static void Update()
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
    static void DoMapMovement()
    {
        player.transform.position += MakeMoveVector();
        // UpdateAnimation(MakeMoveVector());
    }
    static void DoBattleMovement()
    {
        DoMapMovement();

        Vector3 bounds = UIManager.cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3((float)(Screen.width * 0.5), (float)(Screen.height * 0.5)));
        if(player.transform.position.x > bounds.x + 3.3)
        {
            player.transform.position = new Vector3(bounds.x + 3.3f, player.transform.position.y, 0);
        }
        if(player.transform.position.x < bounds.x - 3.3)
        {
            player.transform.position = new Vector3(bounds.x - 3.3f, player.transform.position.y, 0);
        }
        if(player.transform.position.y > bounds.y + 2.8)
        {
            player.transform.position = new Vector3(player.transform.position.x, bounds.y + 2.8f, 0);
        }
        if(player.transform.position.y < bounds.y - 3.7)
        {
            player.transform.position = new Vector3(player.transform.position.x, bounds.y - 3.7f, 0);
        }
    }
    static void DoForcedMovement()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, forcedMoveTarget, forcedMoveSpeed * Time.deltaTime);
    }
    static void UpdateForcedMovement()
    {
        forcedMoveTime -= Time.deltaTime;
        if(forcedMoveTime <= 0)
        {
            canControl = true;
        }
    }
    static void UpdateAnimation(Vector3 moveVector)
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
    static Vector3 MakeMoveVector()
    {
        Vector3 moveVector = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x += Time.deltaTime * PlayerStats.totalSpeed * 0.4f;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x -= Time.deltaTime * PlayerStats.totalSpeed * 0.4f;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.y += Time.deltaTime * PlayerStats.totalSpeed * 0.4f;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.y -= Time.deltaTime * PlayerStats.totalSpeed * 0.4f;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveVector /= 3;
        }
        return moveVector;
    }
}
