using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Furniture")]
public class Furniture : Item
{
    public override Item getItem(){ return this; }
    public override FishingRod getFishingRod() { return null; }
    public override Fish getFish() { return null; }
    public override Furniture getFurniture() { return this; }
}
