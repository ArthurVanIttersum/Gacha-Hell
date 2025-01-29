using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerVariables playerVariables; 
    private bool isPaused = false;

    void Awake() // Not in start since we want to subscribe to the event before it is called
    {
        GameObject castle = GameObject.Find("Castle");
        if (castle == null)
        {
            Debug.LogError("Gameobject 'Castle' Does not exist!");
            
        }
        else
        {
            playerVariables = castle.GetComponent<PlayerVariables>();
            if (playerVariables == null)
            {
                Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
            }
        }
    }

    void GameOverCheck(int newHealth)
    {
        if (newHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("gameOverScene");
    }

    
    public void TogglePause()
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


    // --------- Event Subscriptions ---------
    void OnEnable() // Subscribe to the event
    {
        if (playerVariables != null)
        {
            playerVariables.OnHealthChanged += GameOverCheck;
        }
        
        InputManager.OnEscapeKeyPressed += TogglePause;
    }

    void OnDisable() // Unsubscribe from the event if the object is disabled
    {
        if (playerVariables != null)
        {
            playerVariables.OnHealthChanged -= GameOverCheck;
        }

        InputManager.OnEscapeKeyPressed -= TogglePause;
    }
}
