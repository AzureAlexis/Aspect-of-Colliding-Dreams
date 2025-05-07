using UnityEngine;

public class ComplexDanmaku : MonoBehaviour
{
    public float speed = 1;                     // How fast the danmaku will move (unity units/sec)
    public bool player;
    public float cooldown = 0.1f;
    public bool active = false;
    public bool spell = false;
        public float dirAcc = 0;
    public string dirMod;                  // If not null, used in conjunction with dir for more complex movements
    public string dirBehavior = "normal";  // The way the danmaku figures out where to move
    public float speedAcc = 0;
    public float time = 0;                      // How long has the danmaku existed (seconds)

    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if(active)
        {
            UpdatePosition();
            UpdateSpeed();
            UpdateDir();
            UpdateDestroy();
        }
    }

    void UpdatePosition()
    {
        Vector3 moveVector = transform.right;

        moveVector *= Time.deltaTime;
        moveVector *= speed;

        transform.position += moveVector;
    }

    void UpdateSpeed()
    {
        speed += speedAcc * Time.deltaTime;
    }

    void UpdateDir()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + dirAcc * Time.deltaTime);

        if(transform.childCount == 1)
            transform.GetChild(0).Rotate(0, 0, Time.deltaTime * 1080);
    }

    void UpdateDestroy()
    {
        Vector2 camPos = UIManager.cam.GetComponent<Camera>().WorldToViewportPoint(transform.position);
        if(camPos.x > 1.1 || camPos.x < -0.1 || camPos.y > 1.1 || camPos.y < -0.1)
            Destroy(gameObject);
    }

    public void AddData(Danmaku data)
    {
        transform.position = data.position;
        transform.rotation = Quaternion.Euler(0, 0, data.dir);
        
        speed = data.speed;
        speedAcc = data.speedAcc;
        dirAcc = data.dirAcc;
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(0.5f);
            Destroy(gameObject);
        }
    }
}
