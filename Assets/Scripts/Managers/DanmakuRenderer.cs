using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public static class DanmakuRenderer
{
    public static void RenderBatch(DanmakuBatch batch)
    {
        RenderParams params = new RenderParams(batch.material)
        Mesh mesh = new Mesh();
        Material material = batch.material;
        List<Matrix4x4> matrix = BuildMatrix(batch);

        Graphics.RenderMeshInstanced(params, mesh, 0, matrix)
    }

    static List<Matrix4x4> BuildMatrix(List<Bullet> batch)
    {
        List<Matrix4x4> matrix = new List<Matrix4x4>(batch.Count);

        for(int i = 0; i < matrix.Count; i++)
        {
            matrix.Add(Matrix4x4.Translate(batch[i].position));
        }

        return matrix;
    }
}
