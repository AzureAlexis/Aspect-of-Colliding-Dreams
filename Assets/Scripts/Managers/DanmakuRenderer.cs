using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public static class DanmakuRenderer
{
    public static void RenderBatch(List<Bullet> batch)
    {
        List<Matrix4x4> matrix = BuildMatrix(batch);
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
