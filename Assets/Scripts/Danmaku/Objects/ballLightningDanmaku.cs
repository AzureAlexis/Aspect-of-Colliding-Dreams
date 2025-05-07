using Unity.VisualScripting;
using UnityEngine;

public class BallLightningDanmaku : ComplexDanmaku
{
    public GameObject owner;
    public float distance = 1;
    public float offset = 0;
    SpriteRenderer sprite;
    public Sprite[] materials;
    private int materialIndex = 0;
    private float animationTime = 0;

    new void Update()
    {
        UpdateTime();
        UpdatePosition();
        UpdateAnimation();
    }

    new public void AddData(Danmaku data)
    {
        base.AddData(data);

        sprite = GetComponent<SpriteRenderer>();
        owner = data.owner;
        distance = data.distance;
        offset = data.dir;
        Update();
    }

    void UpdateTime()
    {
        time += Time.deltaTime;
    }

    void UpdatePosition()
    {
        float y = Mathf.Sin(time * speed + offset) * distance;
        float x = Mathf.Cos(time * speed + offset) * distance;

        transform.position = owner.transform.position + new Vector3(x, y, 0);
    }

    void UpdateAnimation()
    {
        animationTime += Time.deltaTime;

        if(animationTime > 0.1)
        {
            animationTime -= 0.1f;

            if(materialIndex >= materials.Length - 1)
                materialIndex = 0;
            else
                materialIndex++;

            sprite.sprite = materials[materialIndex];
        }
    }
}
