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
        if (currentlySelectedTower == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100f);
            if (theTilemap.GetTile(theTilemap.WorldToCell(hit.point)) == tileBase[1] && playerVariables.playerMoney >= currentlySelectedTower.cost)
            {
                Instantiate(currentlySelectedTower, hit.point, Quaternion.identity, transform);
                playerVariables.playerMoney -= currentlySelectedTower.cost;
                Debug.Log(playerVariables.playerMoney);
            }
        }
    }

    public void SetCurrentTower(string towerName)
    {
        if (towerName == "Tower1")//placeholder name change this later so it makes sense
        {
            currentlySelectedTower = towerOptions[0];
        }
        else if (towerName == "Tower2")
        {
            currentlySelectedTower = towerOptions[1];
        }
        else if (towerName == "Tower3")
        {
            currentlySelectedTower = towerOptions[2];
        }
        else
        {
            Debug.Log("invalid tower name");
        }
    }
}
