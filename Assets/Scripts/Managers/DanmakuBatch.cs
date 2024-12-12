using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DanmakuBatch
{
    public bool complex;       // Marks this batch as either simple (false) or complex (true)
    public Material material;  // Only danmakus with the same material can be added
    public List<Danmaku> batch = new List<Danmaku>(128); // The actual list of danmakus

    public DanmakuBatch(bool isComplex, Material defaultMaterial)
    {
        complex = isComplex;
        material = defaultMaterial;
    }

    public Danmaku this[int index] {get => batch[index];}
    public void Add(Danmaku danmaku)
    {
        if(batch.Count == 0)
        {
            ReassignBatch(danmaku);
            return;
        }
        if(batch.Capacity == batch.Count)
        {
            Debug.LogError("Tried to add a danmaku to a batch without space");
            return;
        }
        if(danmaku.material != material)
        {
            Debug.LogError("Tried to add a danmaku to a batch with differing materials");
            return;
        }
        if(danmaku.complex != complex)
        {
            Debug.LogError("Tried to add a danmaku to a batch with differing complexities");
            return;
        }
        if(true)
        {
            batch.Add(danmaku);
            return;
        }
    }

    void ReassignBatch(Danmaku danmaku)
    {
        complex = danmaku.complex;
        material = danmaku.material;
        batch.Add(danmaku);
    }

    public List<Danmaku> Batch()
    {
        return batch;
    }

    public int Count()
    {
        return batch.Count;
    }
}
