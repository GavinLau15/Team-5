using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public TileBase takenTile;

    #region Tilemap Management

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap) {
        TileBase[] array = new TileBase[array.size.x * array.size.y];
        int counter = 0;

        foreach (var v in area.allPositionsWithin) {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        
        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileBase tileBase, Tilemap tilemap) {
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];
        FillTiles(tileArray, tileBase);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(tileBase[] arr, TileBase tileBase) {
        for (int i = 0; i < arr.length; i++) {
            arr[i] = tileBase;
        }
    }

    public void ClearArea(BoundsInt area, TileMap tilemap) {
        SetTilesBlock(area, null, tilemap);        
    }

    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject building, Vector3 pos) {
        pos.z = 0;
        pos.y -= building.GetComponent<SpriteRenderer>().bounds.size.y /2f;
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        Vector3 position = gridLayout.CellToLocalInterpolated(cellPos);

        GameObject obj = Instantiate(building, position, Quaternion.identity);
        PlaceableObject temp = obj.transform.GetComponent<PlaceableObject>();
        temp.gameObject.AddComponent<ObjectDrag>();
    }

    public bool CanTakeArea(BoundsInt area) {
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray) {
            if (b == takenTile) {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area) {
        SetTilesBlock(area, takenTile, MainTilemap);
    }

    #endregion
}
