using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
   
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}

