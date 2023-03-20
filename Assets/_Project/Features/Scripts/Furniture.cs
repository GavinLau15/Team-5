using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Furniture")]
public class Furniture : Item
{
    public override Item getItem(){ return this; }
}
