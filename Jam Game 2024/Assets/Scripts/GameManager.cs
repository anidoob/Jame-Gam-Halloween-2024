using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    public float restartDelay = 1f;

    public GameObject successUI;
    public GameObject failureUI;
    public void EndGame()
    {
        if (gameEnded == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameEnded = true;
            Debug.Log("Game Over");
            Invoke("GameOver", restartDelay);
        }

    }

    public void CompleteLevel()
    {
        Invoke("CompleteScreen", 1f);

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    void CompleteScreen()
    {
        SceneManager.LoadScene(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
