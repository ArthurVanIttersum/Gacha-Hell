using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{    
    public GameObject uiComponent;
    public void ToggleUIComponent()
    {
        if (uiComponent != null)
        {
            uiComponent.SetActive(!uiComponent.activeSelf);
        }
       
    }
}
