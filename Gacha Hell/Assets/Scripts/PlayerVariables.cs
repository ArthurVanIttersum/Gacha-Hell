using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public int _playerHealth = 100;
    public int _playerMoney = 100;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnMoneyChanged;

    public int playerHealth
    {
        get => _playerHealth;
        set
        {
            if (Mathf.Abs(_playerHealth - value) > Mathf.Epsilon) // Check if value changed
            {
                _playerHealth = value;
                OnHealthChanged?.Invoke(_playerHealth); // Trigger event
            }
        }
    }

    public int playerMoney
    {
        get => _playerMoney;
        set
        {
            if (Mathf.Abs(_playerMoney - value) > Mathf.Epsilon) // Check if value changed
            {
                _playerMoney = value;
                OnMoneyChanged?.Invoke(_playerMoney); // Trigger event
            }
        }
    }
}