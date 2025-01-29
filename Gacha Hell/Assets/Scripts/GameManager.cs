using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private PlayerVariables playerVariables; 
    [SerializeField] private TextMeshProUGUI PlayCycleText;
    // private ButtonToggle buttonToggle;

    public enum GameState
    {
        Play,
        DoubleSpeed,
        Pause,
    }
    public GameState currentState = GameState.Play;

    void Awake() // Not in start since we want to subscribe to the event before it is called
    {
        // buttonToggle = GameObject.Find("Start/Pause").GetComponent<ButtonToggle>();
        playerVariables = GameObject.Find("Castle").GetComponent<PlayerVariables>();
        if (playerVariables == null)
        {
            Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
        }
    }

    void Start()
    {
        ChooseGameState();
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

    
    public void ChooseGameState()
    {
        switch (currentState)
        {
            case GameState.Play:
                currentState = GameState.Play;
                Time.timeScale = 1;
                PlayCycleText.text = " ►";
                // buttonToggle.ToggleUIComponent();
                break;
            case GameState.DoubleSpeed:
                currentState = GameState.DoubleSpeed;
                Time.timeScale = 2;
                PlayCycleText.text = "►2x";
                break;
            case GameState.Pause:
                currentState = GameState.Pause;
                Time.timeScale = 0;
                PlayCycleText.text = "||";
                // buttonToggle.ToggleUIComponent();
                break;
        }
    }

    public void SwitchGameStateButton()
    {
        currentState = (GameState)(((int)currentState + 1) % System.Enum.GetValues(typeof(GameState)).Length);
        ChooseGameState();
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
        
        // InputManager.OnEscapeKeyPressed += TogglePause;
    }

    void OnDisable() // Unsubscribe from the event if the object is disabled
    {
        if (playerVariables != null)
        {
            playerVariables.OnHealthChanged -= GameOverCheck;
        }

        // InputManager.OnEscapeKeyPressed -= TogglePause;
    }
}