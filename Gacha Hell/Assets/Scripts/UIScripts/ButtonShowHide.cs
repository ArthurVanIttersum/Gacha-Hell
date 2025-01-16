using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShowHide : MonoBehaviour
{
    public Button toggleButton;
    public GameObject uiComponent;
    // Start is called before the first frame update
    void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleUIComponent);
        }
    }

    void ToggleUIComponent()
    {
        if (uiComponent != null)
        {
            uiComponent.SetActive(!uiComponent.activeSelf);
        }
       
    }
}
