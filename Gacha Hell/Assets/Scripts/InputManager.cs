using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action OnMouseLeftClick;
    public static event Action OnMouseRightClick;
    public static event Action OnEscapeKeyPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Check if left mouse button is clicked
        {
            OnMouseLeftClick?.Invoke(); // Trigger event
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) // Check if left mouse button is clicked
        {
            OnMouseRightClick?.Invoke(); // Trigger event
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if escape key is pressed
        {
            OnEscapeKeyPressed?.Invoke(); // Trigger event
        }
    }
}
