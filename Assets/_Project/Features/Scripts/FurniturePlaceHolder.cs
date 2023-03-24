using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
// using InventoryManager;
// using static UnityEngine.Event;

[CreateAssetMenu(menuName = "Items/FurniturePlaceholder")]
public class FurniturePlaceHolder : Item
{
    public FurnitureType type;

    public enum FurnitureType 
    {
    	Chair,
    	Bed1,
    	Bed2,
    	Table,
    	Drawer,
        Stool
    }

    public override Item getItem(){ return this; }
}