using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

// This is the superscript for danmaku, and instantiates/updates every other danmaku-related script
public class DanmakuManager : MonoBehaviour
{
    public static List<DanmakuBatch> simpleDanmaku;
    public static List<DanmakuBatch> complexDanmaku;
    static GameObject player;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DanmakuManager.StartStatic();
    }
    void Update()
    {
        DanmakuManager.UpdateStatic();
    }
    static void StartStatic()
    {
        CreateBatches();
        player = GameObject.Find("Player");
    }

    static void UpdateStatic()
    {
        DanmakuRenderer.Update();
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
        Debug.Log("firing");
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
        danmaku.acc = danmakuData.acc;
        danmaku.complex = danmakuData.complex;

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
                if(danmaku.material == complexDanmaku[i].material || complexDanmaku[i].Count() == 0)
                {
                    return i;
                }
            }
            return -1;
        }
        else
        {

            Debug.Log("got here");
            for(int i = 0; i < simpleDanmaku.Count; i++)
            {
                if(danmaku.material == simpleDanmaku[i].material || simpleDanmaku[i].Count() == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
