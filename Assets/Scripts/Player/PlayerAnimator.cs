using UnityEngine;

public class PlayerAnimator
{
    static GameObject item;

    public static void Start()
    {
        LoadResources();
    }

    static void LoadResources()
    {
        item = Resources.Load<GameObject>("bullets/fieldItem");
    }

    public static void Play(string id)
    {
        switch(id)
        {
            case "item":
                GameObject.Instantiate(item, PlayerStats.player.transform, false);
                break;
        }
    }
}
