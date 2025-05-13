using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DanmakuBatch
{
    public bool complex;       // Marks this batch as either simple (false) or complex (true)
    public Material material;  // Only danmakus with the same material can be added
    public List<Danmaku> batch = new List<Danmaku>(2048); // The actual list of danmakus

    public DanmakuBatch(bool isComplex, Material defaultMaterial)   // Simple constructer
    {
        complex = isComplex;
        material = defaultMaterial;
    }
   
    public Danmaku this[int index] {get => batch[index];}           // Gets danmaku at index
    public int Count {get => batch.Count;}                          // Gets # of danmaku in batch
    public int Capacity {get => batch.Capacity;}                       // Gets capacity of batch (usually 128)

    public void Update()
    {
        for(int i = 0; i < batch.Count; i++)
            batch[i].Update();
    }
    public void Add(Danmaku danmaku)
    {
        if(batch.Count == 0)
        {
            ReassignBatch(danmaku);
            return;
        }
        if(batch.Capacity == batch.Count)
        {
            Debug.LogError("Tried to add a danmaku to a batch without space. Failed to add");
            return;
        }
        if(danmaku.material != material)
        {
            Debug.LogError("Tried to add a danmaku to a batch with differing materials. Failed to add");
            return;
        }
        if(danmaku.complex != complex)
        {
            Debug.LogError("Tried to add a danmaku to a batch with differing complexities. Failed to add");
            return;
        }
        if(true)
        {
            danmaku.batch = this;
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

    
}
