using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoNextScene : MonoBehaviour
{
    public void Skip()
    {
        SceneManager.LoadSceneAsync(2);
    }

}
