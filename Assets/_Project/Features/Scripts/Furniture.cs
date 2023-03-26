using UnityEngine;


[CreateAssetMenu(menuName = "Items/Furniture")]
public class Furniture : Item
{   

    public override void Use()
    {
        
        GameObject player = GameObject.FindWithTag("Player");

        BuildingSystem.Instance.SetTile(tileRepresentation, Vector3Int.RoundToInt(player.transform.position + new Vector3(-0.5f,0.5f,0f)), BuildingSystem.Map.Static);

    }

    public override Item getItem(){ return this; }
}





