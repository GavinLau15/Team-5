using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public TileBase tileRepresentation;

    public virtual void Use() { }
}
