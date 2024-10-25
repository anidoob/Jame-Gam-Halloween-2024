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
            gameEnded = true;
            Debug.Log("Game Over");
            //Invoke("Restart", restartDelay);
        }

    }

    public void CompleteLevel()
    {
        Debug.Log("Completed");
        //Invoke("BackToMenu", 5f);

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
