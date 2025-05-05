using Unity.VisualScripting;
using UnityEngine;

public class LightningDanmaku : ComplexDanmaku
{
    public float length;
    public Material[] materials;
    private int materialIndex = 0;
    private float startMoveTime;
    public float dir;
    LineRenderer line;
    private float animationTime = 0;

    void Start()
    {
        startMoveTime = length / speed;
        line = GetComponent<LineRenderer>();
    }

    new public void AddData(Danmaku data)
    {
        base.AddData(data);
        line = GetComponent<LineRenderer>();

        length = data.length;
        dir = data.dir;
        startMoveTime = length / speed;
        transform.localPosition = new(0, 0);   
        line.SetPosition(0, data.position);
        line.SetPosition(1, data.position);  

        Debug.Log(dir);
    }

    void Update()
    {
        UpdateTime();
        UpdatePosition();
        UpdateAnimation();
    }

    void UpdateTime()
    {
        time += Time.deltaTime;
    }

    void UpdatePosition()
    {
        Vector3 moveVector = Quaternion.Euler(0, 0, -dir) * Vector2.down;

        moveVector *= Time.deltaTime;
        moveVector *= speed;

        line.SetPosition(0, line.GetPosition(0) + moveVector);
        if(time >= startMoveTime)
            line.SetPosition(1, line.GetPosition(1) + moveVector);
    }

    void UpdateAnimation()
    {
        animationTime += Time.deltaTime;

        if(animationTime > 0.1)
        {
            animationTime -= 0.1f;

            if(materialIndex >= materials.Length - 1)
            {
                line.material = materials[0];
                materialIndex = 0;
            }
            else
            {
                materialIndex++;
                line.material = materials[materialIndex];
            }
        }
    }
}
