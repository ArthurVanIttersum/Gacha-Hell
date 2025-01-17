using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerVariables playerVariables; 
    private bool isPaused = false;

    void Start()
    {
        GameObject castleObject = GameObject.Find("Castle");
        if (castleObject != null)
        {
            playerVariables = castleObject.GetComponent<PlayerVariables>();
            if (playerVariables == null)
            {
                Debug.LogError("PlayerVariables component not found on the 'Castle' GameObject!");
            }
        }
    }

    void Update()
    {
        if (playerVariables != null && playerVariables.playerHealth <= 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("gameOverScene");
    }

    
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; 
            
        }
        else
        {
            Time.timeScale = 1; 
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

