using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    private Vector3 origin;

    public BoundsInt area;

    public bool CanBePlaced() {
        Vector3Int positionInt = BuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        return BuildingSystem.current.CanTakeArea(areaTemp);
    }

    publicvoid Place() {
        Vector3Int positionInt = BuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        Placed = true;
        origin = transform.position;

        BuildingSystem.current.TakeArea(areaTemp);
    }

    public void CheckPlacement() {
        if (CanBePlaced()) {
            Place();
            origin = transform.position;
        }
        else {
            Destroy(transform.gameObject);
        }
    }

    protected virtual void OnClick() {
        
    }
}
