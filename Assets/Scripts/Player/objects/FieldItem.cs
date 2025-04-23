using UnityEngine;

public class FieldItem : MonoBehaviour
{
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        transform.localPosition = new Vector3(0, AzalUtil.Bounce1D(1, time), 0);
        transform.Rotate(0, 0, Time.deltaTime * 720);

        if(time >= 1)
            Destroy(gameObject);
    }
}
