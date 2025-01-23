using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class TowerSelector : MonoBehaviour
{
    [SerializeField] private GameObject selectedTowerMenu;
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI targetingMethodText;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI targetingOptionText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    public LayerMask layerMask;
    private GameObject selectedTower;
    private GameObject previousSelectedTower;
    private GameObject selectedTowerRange;
    private PlayerVariables playerVariables;
    private TowerPlacer towerPlacer;
    public float sellValue = 0.5f;
    private TowerBase.TargetingOptions currentTargeting;

    void Start()
    {
        playerVariables = GameObject.Find("Castle").GetComponent<PlayerVariables>();
        if (playerVariables == null)
        {
            Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
        }
        towerPlacer = GetComponent<TowerPlacer>();
    }

    private void CheckForTower()
    {
        // Ignore input if the pointer is over UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            if (hit.collider.CompareTag("Tower"))
            {
                GameObject clickedTower = hit.collider.gameObject;

            if (clickedTower != selectedTower)
            {
                if (selectedTower != null)
                {
                    RemoveRange();
                }

                previousSelectedTower = selectedTower;
                selectedTower = clickedTower;

                ShowWrapper();
                UpdateMenuInfo();
            }
            }
        } 
        else
        {
            RemoveWrapper();
        }
    }

    private void UpdateMenuInfo()
    {
        if (selectedTower != null)
        {
            string towerScriptName = selectedTower.GetComponent<MonoBehaviour>().GetType().Name;

            TowerBase.TargetingOptions targeting = selectedTower.GetComponent<TowerBase>().currentTargeting;

            towerNameText.text = $"{towerScriptName}";
            targetingMethodText.text = $"{targeting}";
        }
    }

    public void SellSelectedTower()
    {
        if (selectedTower != null)
        {
            Destroy(selectedTower);
            playerVariables.playerMoney += (int)(selectedTower.GetComponent<TowerBase>().cost * sellValue); // Make into int so it works to do decimal values
            Vector3Int towerPosition = towerPlacer.theTilemap.WorldToCell(selectedTower.transform.position);
            towerPlacer.placedTowers.Remove(towerPosition); // Remove tower from dictionary
            RemoveMenu();
        }
        else
        {
            Debug.LogWarning("No tower selected to sell.");
        }
    }

    private void ShowWrapper()
    {
        if (selectedTower != null)
        {
            ShowMenu();
            ShowRange();
        }
    }

    private void RemoveWrapper() // Did this to only have 1 subscription to the event
    {
        RemoveMenu();
        RemoveRange();
    }

    private void ShowMenu()
    {
        selectedTowerMenu.SetActive(true);
    }

    private void RemoveMenu()
    {
        selectedTowerMenu.SetActive(false);
        selectedTower = null;
        previousSelectedTower = null;
    }

    private void ShowRange()
    {
        selectedTowerRange = selectedTower.transform.Find("TowerRange").gameObject;
        selectedTowerRange.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    private void RemoveRange()
    {
        if (selectedTowerRange != null)
        {
            selectedTowerRange.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    public void SwitchTargetingLeft()
    {
        currentTargeting--; // Decrement the enum value
        if ((int)currentTargeting < 0)
        {
            currentTargeting = TowerBase.TargetingOptions.Preferred; // Wrap around to the last value
        }
        ApplyTargetingToTower();
        UpdateMenuInfo();
    }

    public void SwitchTargetingRight()
    {
        currentTargeting++; // Increment the enum value
        if ((int)currentTargeting >= System.Enum.GetValues(typeof(TowerBase.TargetingOptions)).Length)
        {
            currentTargeting = TowerBase.TargetingOptions.First; // Wrap around to the first value
        }
        ApplyTargetingToTower();
        UpdateMenuInfo();
    }

    private void ApplyTargetingToTower()
    {
        if (selectedTower != null)
        {
            selectedTower.GetComponent<TowerBase>().currentTargeting = currentTargeting;
        }
    }
    
    // --------- Event Subscriptions ---------
    void OnEnable() // Subscribe to the event
    {
        InputManager.OnMouseLeftClick += CheckForTower;
        InputManager.OnMouseRightClick += RemoveWrapper;
    }

    void OnDisable() // Unsubscribe from the event if the object is disabled
    {
        InputManager.OnMouseLeftClick -= CheckForTower;
        InputManager.OnMouseRightClick += RemoveWrapper;
    }
}
