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

    public Material previewMaterial;
    public Material previewMaterialRed;
    private GameObject currentPreview;
    private Coroutine previewCoroutine;
    private GameObject currentPreviewTowerRange;

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

    public void PlaceTower()
    {
        if (currentlySelectedTower != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100f, layerMask);
            Vector3Int gridPosition = theTilemap.WorldToCell(hit.point);

            // Checks tile is grass tile & player has enough money & there is no tower already placed there through dictionary
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
                ShowPreview();
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

    // --------- Preview functions ---------

    private void ShowPreview()
    {
        // Destroy the current preview
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        currentPreview = Instantiate(currentlySelectedTower).gameObject;
        currentPreview.transform.position = new Vector3(0, 100, 0); // Move the preview higher up, off-screen to avoid appearing at grid 0,0
        currentPreviewTowerRange = currentPreview.transform.Find("TowerRange").gameObject;
        currentPreviewTowerRange.GetComponentInChildren<MeshRenderer>().enabled = true;
        currentPreviewTowerRange.GetComponentInChildren<SphereCollider>().isTrigger = false; // To fix bug where tower preview can shoot enemies
        ApplyPreviewMaterial(currentPreview);

        // Update the preview position
        UpdatePreviewPosition();
    }

    // Updates the preview position to snap to the grid under the mouse
    private void UpdatePreviewPosition()
    {
        if (previewCoroutine == null)
        {
            previewCoroutine = StartCoroutine(ContinuousRaycastPreview());
        }
    }

    private IEnumerator ContinuousRaycastPreview()
    {
        while (currentPreview != null)  // Continue while the preview is still active
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

            yield return null;
        }        
    }

    public void ClearPreview()
    {
        if (currentPreview != null)
        {
            StopCoroutine(previewCoroutine); // Stop the continuous raycast
            Destroy(currentPreview);
            currentPreview = null;
            previewCoroutine = null;
        }

        currentlySelectedTower = null;
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

    // --------- Event Subscriptions ---------
    void OnEnable()
    {
        // Subscribe to the Mouse0 click event when enabled (e.g., when the object is active)
        InputManager.OnMouseLeftClick += PlaceTower;
        InputManager.OnMouseRightClick += ClearPreview;
    }

    void OnDisable()
    {
        // Unsubscribe when the object is disabled to prevent memory leaks
        InputManager.OnMouseLeftClick -= PlaceTower;
        InputManager.OnMouseRightClick -= ClearPreview;
    }
}
