using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class BuildingSystem : MonoSingletonPersistent<BuildingSystem>
{
    public enum Map { Static, Interactable, Ground, Water};

    [SerializeField] private TileDatabase database;

    public GridLayout gridLayout;
    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap staticMap;
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap waterMap;
    [SerializeField] private TileBase baseTileForGround;
    public TileBase takenTile;



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

    public void RemoveTile(Vector3Int position, Map map)
    {
        GetTileMap(map).SetTile(position, null);
    }

    #endregion
}

