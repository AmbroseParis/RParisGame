using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishFlag : MonoBehaviour
{
    [SerializeField] private GameObject levelClearUI; // reference the level clear UI
    [SerializeField] private float delayBeforeNextLevel = 2f; // slight delay before loading next level

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null) // check if it was the player that triggered the fkag
        {
            TriggerLevelClear();
        }
    }

    private void TriggerLevelClear()
    {
        if (levelClearUI != null)
        {
            levelClearUI.SetActive(true); //display the level clear ui
        }

        Invoke("LoadNextLevel", delayBeforeNextLevel); // wait before loading the next level
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // load the next scene in the build index
    }
}