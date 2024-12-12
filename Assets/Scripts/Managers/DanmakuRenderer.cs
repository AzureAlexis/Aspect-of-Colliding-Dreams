using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public static class DanmakuRenderer
{
    public static void Update()
    {
        for(int i = 0; i < DanmakuManager.simpleDanmaku.Count; i++)
        {
            RenderBatch(DanmakuManager.simpleDanmaku[i]);
        }
        for(int i = 0; i < DanmakuManager.complexDanmaku.Count; i++)
        {
            RenderBatch(DanmakuManager.complexDanmaku[i]);
        }
    }

    public static void RenderBatch(DanmakuBatch batch)
    {
        if(batch.Count() > 0)
        {
            RenderParams rParams = new RenderParams(batch.material);
            Mesh mesh = new Mesh();
            Material material = batch.material;
            List<Matrix4x4> matrix = BuildMatrix(batch);

            Graphics.RenderMeshInstanced(rParams, mesh, 0, matrix);
        }
    }

    static List<Matrix4x4> BuildMatrix(DanmakuBatch batch)
    {
        List<Matrix4x4> matrix = new List<Matrix4x4>(batch.Count());

        for(int i = 0; i < matrix.Count; i++)
        {
            matrix.Add(Matrix4x4.Translate(batch[i].position));
        }

        return matrix;
    }
}
