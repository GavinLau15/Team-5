using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public TileBase tileRepresentation;
    public Sprite itemIcon;

    public virtual void Use() { }

    public abstract Item getItem();
    public abstract FishingRod getFishingRod();
    public abstract Fish getFish();
    public abstract Furniture getFurniture();
}
