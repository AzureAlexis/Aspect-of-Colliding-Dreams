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
    public Mesh mesh;

    // Just called when the "DanmakuManager" starts so all the other danmaku scripts can start
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
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
        for(int i = 0; i < 10; i++)
        {
            simpleDanmaku.Add(new DanmakuBatch(false, Resources.Load("enemyBulletRed_0", typeof(Material)) as Material));
        }
    }
    
    public static void Fire(List<DanmakuData> danmakus, GameObject owner)
    {
        for(int i = 0; i < danmakus.Count; i++)
        {
            DanmakuData danmakuData = danmakus[i];
            Danmaku danmaku = InitilizeDanmaku(danmakuData, owner);

            if(danmaku.complex)
            {
                GameObject complexDanmaku = Instantiate(danmakuData.prefab, GameObject.Find("DanmakuManager").transform);
                complexDanmaku.GetComponent<ComplexDanmaku>().AddData(danmaku);
                if(PlayerShoot.spellActive)
                    complexDanmaku.GetComponent<ComplexDanmaku>().spell = true;
            }
            else
            {
                AssignToBatch(danmaku);
            }
        }
    }

    static Danmaku InitilizeDanmaku(DanmakuData danmakuData, GameObject owner)
    {
        Danmaku danmaku = new Danmaku();

        danmaku.speed = danmakuData.speed;
        danmaku.complex = danmakuData.complex || danmakuData.player == true;
        danmaku.material = danmakuData.material;
        danmaku.dirAcc = danmakuData.dirAcc;
        danmaku.speedAcc = danmakuData.speedAcc;

        switch (danmakuData.posBehavior)
        {
            case "normal":
                danmaku.position = owner.transform.position;
                break;

            case "normalMod":
                Vector3 mod = new Vector3(0,0,0);

                switch(danmakuData.posMod)
                {
                    case "sinX":
                        float x = Mathf.Sin(PlayerShoot.patternTime * 4);
                        mod += new Vector3(x * danmakuData.position.x,0,0);
                        break;
                    default:
                        mod = danmakuData.position;
                        break;
                }

                danmaku.position = owner.transform.position + mod;
                break;

            case "randomMod":
                float randX = Random.Range(-danmakuData.position.x, danmakuData.position.x);
                float randY = Random.Range(-danmakuData.position.y, danmakuData.position.y);

                danmaku.position = (Vector2)owner.transform.position + new Vector2(randX, randY);
                break;
        }

        switch (danmakuData.dirBehavior)
        {
            case "normal":
                danmaku.dir = danmakuData.dir;
                break;

            case "random":
                danmaku.dir = Random.Range(0, 360);
                break;

            case "player":
                Vector2 playerPos = PlayerStats.player.transform.position;
                Vector2 dif = danmaku.position - playerPos;
                danmaku.dir = (Mathf.Atan2(dif.x, dif.y) * 180 / Mathf.PI) + danmakuData.dir;
                break;

            case "enemy":
                var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
                Vector2 bestPos = danmaku.position + new Vector2(-1, 0);

                if(enemies.Length > 0)
                {
                    bestPos = enemies[0].transform.position;
                    float priority = Vector2.Distance(danmaku.position, bestPos);
                    for(int i = 0; i < enemies.Length; i++)
                    {
                        Vector2 tempPos = enemies[i].transform.position;
                        if(Vector2.Distance(danmaku.position, tempPos) < priority)
                        {
                            bestPos = tempPos;
                            priority = Vector2.Distance(danmaku.position, tempPos);
                        }
                    }
                }

                danmaku.dir = Mathf.Atan2(danmaku.position.x - bestPos.x, danmaku.position.y - bestPos.y) * 180 / Mathf.PI;
                break;
        }

        return danmaku;
    }

    static void AssignToBatch(Danmaku danmaku)
    {
        int index = FindMatchingBatch(danmaku);
        simpleDanmaku[index].Add(danmaku);
        danmaku.batch = simpleDanmaku[index];
    }

    static int FindMatchingBatch(Danmaku danmaku)
    {
        for(int i = 0; i < simpleDanmaku.Count; i++)
        {
            if(danmaku.material == simpleDanmaku[i].material || simpleDanmaku[i].Count == 0)
            {
                return i;
            }
        }
        Debug.LogAssertion("Failed to find a matching batch. Danmaku will not render or update");
        return -1;
    }

    public static void ClearAllBullets()
    {
        for(int i = 0; i < simpleDanmaku.Count; i++)
        {
            simpleDanmaku[i].batch.Clear();
        }
    }
}
