using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public TowerBase[ ]towerOptions;
    private TowerBase currentlySelectedTower = null;
    public PlayerVariables playerVariables;
    public Tilemap theTilemap;
    public TileBase[ ]tileBase = new TileBase[4];
    private bool hoveringOverButton;
    public LayerMask layerMask;

    public Material previewMaterial; // Grayed-out material
    public Material previewMaterialRed; // Red material for invalid placement
    private GameObject currentPreview; // The current preview object

    // A dictionary to keep track of the towers that have been placed
    private Dictionary<Vector3Int, GameObject> placedTowers = new Dictionary<Vector3Int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerVariables = GameObject.Find("Castle").GetComponent<PlayerVariables>();
        if (playerVariables == null)
        {
            Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ClearPreview();
        }
        if (currentlySelectedTower == null)
        {
            return;
        }
        if (hoveringOverButton)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100f, layerMask);
            Vector3Int gridPosition = theTilemap.WorldToCell(hit.point);

            if (theTilemap.GetTile(gridPosition) == tileBase[1] && playerVariables.playerMoney >= currentlySelectedTower.cost && !placedTowers.ContainsKey(gridPosition))
            {
                // Instantiate the tower
                GameObject newTower = Instantiate(currentlySelectedTower.gameObject, theTilemap.GetCellCenterWorld(gridPosition), Quaternion.identity, transform);

                // Register the tower position in the dictionary
                placedTowers[gridPosition] = newTower;
                
                playerVariables.playerMoney -= currentlySelectedTower.cost;
                ClearPreview();
            }
        }

        if (currentlySelectedTower != null)
        {
            UpdatePreviewPosition();
        }
    }

    public void SetCurrentTower(string towerName)
    {
        switch (towerName)
        {
            case "Tower1": // Change tower name so it makes sense
                currentlySelectedTower = towerOptions[0];
                ShowPreview();
            break;

            case "Tower2":
                currentlySelectedTower = towerOptions[1];
                ShowPreview();
            break;

            case "Tower3":
                currentlySelectedTower = towerOptions[2];
            break;

            default:
                Debug.Log("invalid tower name");
            break;
        }
    }

    public void setHoveringOverButton(bool newValue)
    {
        hoveringOverButton = newValue;
    }

    private void ShowPreview()
    {
        // Destroy the current preview
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        // Create a new preview instance
        currentPreview = Instantiate(currentlySelectedTower).gameObject;
        ApplyPreviewMaterial(currentPreview);
    }

    // Updates the preview position to snap to the grid under the mouse
    private void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Vector3Int gridPosition = theTilemap.WorldToCell(hit.point);
            currentPreview.transform.position = theTilemap.GetCellCenterWorld(gridPosition);

            // Check if the tower can be placed at the current position
            if (theTilemap.GetTile(gridPosition) == tileBase[1] && playerVariables.playerMoney >= currentlySelectedTower.cost && !placedTowers.ContainsKey(gridPosition))
            {
                ApplyPreviewMaterial(currentPreview);
            }
            else
            {
                ApplyPreviewMaterialRed(currentPreview);
            }
        }
    }

    // Apply the grayed-out material to all renderers in the preview object
    private void ApplyPreviewMaterial(GameObject preview)
    {
        foreach (Renderer renderer in preview.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterial;
            }
            renderer.materials = materials;
        }
    }

    private void ApplyPreviewMaterialRed(GameObject preview)
    {
        foreach (Renderer renderer in preview.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialRed;
            }
            renderer.materials = materials;
        }
    }

    // Remove the preview when done
    public void ClearPreview()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        currentlySelectedTower = null;
    }
}
