using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class BuildingSystem : MonoBehaviour
{
    public enum Map { Static, Interactable, Ground, Water};

    [SerializeField] private TileDatabase database;
    public static BuildingSystem Instance;

    public GridLayout gridLayout;
    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap staticMap;
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap waterMap;
    public TileBase takenTile;


    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    #region Tilemap Management

    private Tilemap GetTileMap(Map map) 
    {
        switch (map)
        {
            case Map.Static:
                return staticMap;
            case Map.Interactable:
                return interactableMap;
            case Map.Ground:
                return groundMap;
            case Map.Water:
                return waterMap;

            default: return null;
        }
    }

    public TileBase GetTile(Vector3Int position, Map map)
    {
        return GetTileMap(map).GetTile(position);
    }


    public void SetTile(int id, Vector3Int position, Map map) 
    {
        GetTileMap(map).SetTile(position, null);
    }
    public void SetTile(TileBase tile, Vector3Int position, Map map)
    {
        GetTileMap(map).SetTile(position, tile);
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileBase tileBase, Tilemap tilemap)
    {
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];
        FillTiles(tileArray, tileBase);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileBase tileBase)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBase;
        }
    }

    public void ClearArea(BoundsInt area, Tilemap tilemap)
    {
        SetTilesBlock(area, null, tilemap);
    }

    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject building, Vector3 pos)
    {
        pos.z = 0;
        pos.y -= building.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        Vector3 position = gridLayout.CellToLocalInterpolated(cellPos);

        GameObject obj = Instantiate(building, position, Quaternion.identity);
        PlaceableObject temp = obj.transform.GetComponent<PlaceableObject>();
        //temp.gameObject.AddComponent<ObjectDrag>();
    }

    public bool CanTakeArea(BoundsInt area, Map map)
    {
        TileBase[] baseArray = GetTilesBlock(area, GetTileMap(map));

        foreach (var b in baseArray)
        {
            if (b == takenTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area, Map map)
    {
        SetTilesBlock(area, takenTile, GetTileMap(map));
    }

    #endregion
}

