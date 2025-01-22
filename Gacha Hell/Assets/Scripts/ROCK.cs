using UnityEngine;
using UnityEngine.UI;

public class ROCK : MonoBehaviour
{
    public GameObject removeButton;
    public int removeCost = 10;
    private static ROCK selectedRock;
    private Material originalMaterial;
    private Renderer rockRenderer;
    private PlayerVariables playerVariables;

    void Start()
    {
        rockRenderer = GetComponent<Renderer>();
        removeButton?.SetActive(false);

        playerVariables = FindObjectOfType<PlayerVariables>();

        if(playerVariables == null )
        {
            Debug.Log("PlayerVariables script not found in the scene!");
        }
    }

    void Update()
    {
        if (selectedRock == this && Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject != gameObject)
                {
                    DeselectRock();
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (selectedRock != this)
        {
            selectedRock?.DeselectRock();
            SelectRock();
        }
    }

    void SelectRock()
    {
        originalMaterial = rockRenderer.material;
        rockRenderer.material = new Material(originalMaterial);
        rockRenderer.material.color = Color.cyan;
        ShowRemoveButton();
        selectedRock = this;
    }

    void DeselectRock()
    {
        if (rockRenderer != null)
            rockRenderer.material = originalMaterial;
        removeButton?.SetActive(false);
        selectedRock = null;
    }

    void ShowRemoveButton()
    {
        removeButton?.SetActive(true);
        removeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        removeButton.GetComponent<Button>().onClick.AddListener(TryRemoveRock);
    }

    void TryRemoveRock()
    {
        if (playerVariables.playerMoney >= removeCost)
        {
            // Deduct currency and remove the rock
            playerVariables.playerMoney -= removeCost;
            RemoveSelectedRock();
        }
        else
        {
            Debug.Log("Not enough money to remove the rock!");
         
        }
    }
    void RemoveSelectedRock()
    {
        gameObject.SetActive(false);
        removeButton?.SetActive(false);
        selectedRock = null;
    }

    bool IsPointerOverUIObject()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
