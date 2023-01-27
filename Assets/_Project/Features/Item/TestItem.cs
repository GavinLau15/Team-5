using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Items/TestItem")]
public class TestItem : Item
{   

    

    public override void Use()
    {
        //decorative item such as a flower vase.

        //what to acheive: Be able to place this item at tile in front of player. The target tile must be empty. Changing target
        //tile's tile sprite. The item will have a dimension in terms of tiles, for example (1 tile wide x 2 tile tall). assume 1x1

        //target tile = tile data in front of player

        GameObject player = GameObject.FindWithTag("Player");
        //BuildingSystem.Instance.SetTile(0, Vector3Int.RoundToInt(player.transform.position), BuildingSystem.Map.Ground);
        BuildingSystem.Instance.SetTile(tileRepresentation, Vector3Int.RoundToInt(player.transform.position + new Vector3(-0.5f,-0.5f,0f)), BuildingSystem.Map.Static);

        //TileBase targetTile = BuildingSystem.Instance.GetTile(Vector3Int.RoundToInt(player.transform.position), BuildingSystem.Map.Ground);

        //BuildingSystem.GetTileMap()

        //if target tile is empty

        //set target tile to item's tile representative


    }


}





