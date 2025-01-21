using UnityEngine;
using UnityEngine.UI;

public class ROCK : MonoBehaviour
{
    public GameObject removeButton;
    private static ROCK selectedRock;
    private Material originalMaterial;
    private Renderer rockRenderer;

    void Start()
    {
        rockRenderer = GetComponent<Renderer>();
        removeButton?.SetActive(false);
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
        removeButton.GetComponent<Button>().onClick.AddListener(RemoveSelectedRock);
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
