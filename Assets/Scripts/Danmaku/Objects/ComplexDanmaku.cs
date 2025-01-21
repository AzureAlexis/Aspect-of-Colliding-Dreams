using UnityEngine;

public class ComplexDanmaku : MonoBehaviour
{
    public float speed = 1;                     // How fast the danmaku will move (unity units/sec)
    public bool player;
    public float cooldown = 0.1f;
    public bool active = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            UpdatePosition();
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
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(0.2f);
            Destroy(gameObject);
        }
    }
}
