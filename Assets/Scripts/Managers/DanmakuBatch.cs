using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DanmakuBatch
{
    bool complex;       // Marks this batch as either simple (false) or complex (true)
    Material material;  // Only bullets with the same material can be added
    List<Bullet> batch; // The actual list of bullets

    public DanmakuBatch(bool isComplex)
    {
        complex = isComplex;
    }

    public void Add(Bullet bullet)
    {
        if(batch.Capacity == batch.Count)
        {
            Debug.LogError("Tried to add a bullet to a batch without space");
            return;
        }
        if(batch.count == 0)
        {
            ReassignBatch(bullet);
            return;
        }
        if(bullet.material != material)
        {
            Debug.LogError("Tried to add a bullet to a batch with differing materials");
            return;
        }
        if(bullet.complex != complex)
        {
            Debug.LogError("Tried to add a bullet to a batch with differing complexities");
            return;
        }
        if(true)
        {
            batch.Add(bullet);
            return;
        }
    }

    void ReassignBatch(Bullet bullet)
    {
        complex = bullet.complex;
        material = bullet.material;
        batch.Add(bullet);
    }

    public List<Bullet> Batch()
    {
        return batch;
    }
}
