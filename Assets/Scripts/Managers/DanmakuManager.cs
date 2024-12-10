using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

// This is the superscript for danmaku, and instantiates/updates every other danmaku-related script
public class DanmakuManager : MonoBehaviour
{
    static List<DanmakuBatch> simpleBullets;
    static List<DanmakuBatch> complexBullets;

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
    }

    static void UpdateStatic()
    {
        UpdateRendering();
    }

    static void UpdateRendering()
    {

    }

    static void CreateBatches()
    {
        for(int i = 0; i < 10; i++)
        {
            simpleBullets.Add(new DanmakuBatch(false));
            complexBullets.Add(new DanmakuBatch(true));
        }
    }
}
