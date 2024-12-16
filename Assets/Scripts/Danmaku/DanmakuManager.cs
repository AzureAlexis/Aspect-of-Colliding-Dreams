/* 
This is the superscript for danmaku, and instantiates/updates every other danmaku-related script.
DanmakuManager is the only danmaku-releated script that's a monobehavior, to provide a way to
update every script that needs to be updated via Unity's Update() function. There's probably
a more efficent way to do this, but I'm dumb so we're going with this :p
*/

using System.Collections.Generic;
using UnityEngine;

public class DanmakuManager : MonoBehaviour
{
    public static List<DanmakuBatch> simpleDanmaku;
    public static List<DanmakuBatch> complexDanmaku;
    public Mesh mesh;

    // Just called when the "DanmakuManager" starts so all the other danmaku scripts can start
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DanmakuManager.StartStatic(mesh);
    }

    void Update()
    {
        DanmakuManager.UpdateStatic();
    }

    static void StartStatic(Mesh mesh)
    {
        CreateBatches();
        DanmakuRenderer.Start(mesh);
    }

    static void UpdateStatic()
    {
        DanmakuRenderer.Update();
        UpdateBatches();
    }

    static void UpdateBatches()
    {
        foreach (DanmakuBatch batch in simpleDanmaku)
        {
            batch.Update();
        }
    }

    static void CreateBatches()
    {
        simpleDanmaku = new List<DanmakuBatch>(10);
        complexDanmaku = new List<DanmakuBatch>(10);
        for(int i = 0; i < 10; i++)
        {
            simpleDanmaku.Add(new DanmakuBatch(false, Resources.Load("material", typeof(Material)) as Material));
            complexDanmaku.Add(new DanmakuBatch(true, Resources.Load("material", typeof(Material)) as Material));
        }
    }
    
    public static void Fire(List<DanmakuData> danmakus, GameObject enemy)
    {
        for(int i = 0; i < danmakus.Count; i++)
        {
            DanmakuData danmakuData = danmakus[i];
            Danmaku danmaku = InitilizeDanmaku(danmakuData, enemy);
            AssignToBatch(danmaku);
        }
    }

    static Danmaku InitilizeDanmaku(DanmakuData danmakuData, GameObject enemy)
    {
        Danmaku danmaku = new Danmaku();

        danmaku.speed = danmakuData.speed;
        danmaku.complex = danmakuData.complex;
        danmaku.material = danmakuData.material;

        switch (danmakuData.posBehavior)
        {
            case "normal":
                danmaku.position = enemy.transform.position;
                break;
        }

        switch (danmakuData.dirBehavior)
        {
            case "normal":
                danmaku.dir = danmakuData.dir;
                break;
        }

        return danmaku;
    }

    static void AssignToBatch(Danmaku danmaku)
    {
        int index = FindMatchingBatch(danmaku);
        if(danmaku.complex)
        {
            complexDanmaku[index].Add(danmaku);
        }
        else
        {
            simpleDanmaku[index].Add(danmaku);
        }
    }

    static int FindMatchingBatch(Danmaku danmaku)
    {
        if(danmaku.complex)
        {
            for(int i = 0; i < complexDanmaku.Count; i++)
            {
                if(danmaku.material == complexDanmaku[i].material || complexDanmaku[i].Count == 0)
                {
                    return i;
                }
            }
            return -1;
        }
        else
        {
            for(int i = 0; i < simpleDanmaku.Count; i++)
            {
                if(danmaku.material == simpleDanmaku[i].material || simpleDanmaku[i].Count == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
