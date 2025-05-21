using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool player = false;
    public string sceneName;

    void Start()
    {

    }

    void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(sceneName);
            PlayerStats.player.transform.position = new Vector3(0, 0, -10);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            player = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            player = false;
    }
}
