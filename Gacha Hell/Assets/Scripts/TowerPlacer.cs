using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public TowerBase[ ]towerOptions;
    private TowerBase currentlySelectedTower;
    public DisplayIntValue valueDisplay;
    public Tilemap theTilemap;
    public TileBase[ ]tileBase = new TileBase[4];
    // Start is called before the first frame update
    void Start()
    {
        //placeholder
        currentlySelectedTower = towerOptions[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 10f);
            if (theTilemap.GetTile(theTilemap.WorldToCell(hit.point)) == tileBase[1])
            {
                Instantiate(currentlySelectedTower, hit.point, Quaternion.identity, transform);
                valueDisplay.ChangeValue(-currentlySelectedTower.cost);
            }
            
            
            
        }
    }
}
