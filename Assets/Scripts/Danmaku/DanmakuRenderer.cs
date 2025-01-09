using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class DanmakuRenderer
{
    static SpriteAtlas spriteAtlas;
    static Mesh defaultMesh;
    public static void Start(Mesh debugMesh)
    {
        spriteAtlas = Resources.Load("_spriteAtlas", typeof(SpriteAtlas)) as SpriteAtlas;
        defaultMesh = debugMesh;
    }

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
        if(batch.Count > 0)
        {
            RenderParams rParams = new RenderParams(batch.material);
            Material material = batch.material;
            List<Matrix4x4> matrix = BuildMatrix(batch);
            // Mesh mesh = BuildMesh(batch); See "mesh stuff"
            Mesh mesh = defaultMesh;

            Graphics.RenderMeshInstanced(rParams, mesh, 0, matrix);
        }
    }

    static List<Matrix4x4> BuildMatrix(DanmakuBatch batch)
    {
        List<Matrix4x4> matrix = new List<Matrix4x4>(batch.Count);
        Sprite sprite = spriteAtlas.GetSprite(batch.material.name);

        for(int i = 0; i < batch.Count; i++)
        {
            Vector3 position = batch[i].position;
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Vector3 scale = new Vector3(sprite.rect.width / 96, sprite.rect.height / 96, 1);

            matrix.Add(Matrix4x4.TRS(position, rotation, scale));
        }

        return matrix;
    }

    /* Mesh stuff
    I need to figure out how to build a mesh at runtime. This current
    solution won't last. When I try to invoke this method, the mesh
    seems like it builds correctly but the sprite won't render?
    static Mesh BuildMesh(DanmakuBatch batch)
    {
        Mesh mesh = new Mesh();
        Sprite sprite = spriteAtlas.GetSprite(batch.material.name);

        mesh.SetVertices(ToV3List(sprite.vertices));
        mesh.SetTriangles(sprite.triangles, 0);

        return mesh;
    }

    static Vector3[] ToV3List(Vector2[] v2)
    {
        return System.Array.ConvertAll<Vector2, Vector3> (v2, V3Converter);
    }

    private static Vector3 V3Converter(Vector2 v2)
    {
        return new Vector3(v2.x, v2.y, 0);
    }
    */
}
