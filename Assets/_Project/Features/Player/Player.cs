using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingletonPersistent<Player>
{
    public LayerMask teleportPlayer;
    [SerializeField] PlayerTileMovement playerTileMovement;
    
  
   
    void Update()
    {
        // press 1 to place tile directly under player
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemSystem.Instance.GetItem(0).Use();
        }

        //press 2 to remove tile directly under player
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BuildingSystem.Instance.RemoveTile(Vector3Int.RoundToInt(this.transform.position + new Vector3(-0.5f, 0.5f, 0f)), BuildingSystem.Map.Static);
        }
        
    }

    public void Teleport(Vector2 position)
    {
        playerTileMovement.Teleport(position);
    }





}
