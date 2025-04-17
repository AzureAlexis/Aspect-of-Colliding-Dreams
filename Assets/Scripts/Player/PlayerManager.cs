using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;
    public static bool inBattle = false;

    void Start()
    {
        PlayerManager.player = gameObject;

        PlayerStats.EquipItem(ItemManager.GetItemByName("Sonic Wave"), 0);
        PlayerStats.EquipItem(ItemManager.GetItemByName("Magic Missile"), 1);
        PlayerStats.EquipItem(ItemManager.GetItemByName("Riftbinder"), 2);
        PlayerStats.EquipItem(ItemManager.GetItemByName("Life Stone"), 3);
        PlayerStats.EquipItem(ItemManager.GetItemByName("Anima Dust"), 4);
        PlayerStats.EquipItem(ItemManager.GetItemByName("Anima Shard"), 5);
        PlayerStats.EquipItem(ItemManager.GetItemByName("Rising Inferno"), 6);
    }
    void Update()
    {
        PlayerManager.UpdateStatic(gameObject);
    }

    static void UpdateStatic(GameObject playerObj)
    {
        AssignPlayerReferences(playerObj);
        PlayerShoot.Update();
        PlayerMove.Update();
        PlayerStats.Update();
    }

    static public void AssignPlayerReferences(GameObject playerObj)
    {
        PlayerManager.player = playerObj;
        PlayerShoot.player = playerObj;
        PlayerMove.player = playerObj;
        PlayerStats.player = playerObj;
    }

    public static void EnterBattle()
    {
        inBattle = true;
        PlayerMove.EnterBattle();
        //player.GetComponent<Animator>().SetBool("Battle", true);
    }

    public static void ExitBattle()
    {
        inBattle = false;
        PlayerMove.ExitBattle();
        //player.GetComponent<Animator>().SetBool("Battle", false);
    }

    public static void TakeDamage(float damage)
    {
        PlayerStats.TakeDamage(damage);
    }
}
