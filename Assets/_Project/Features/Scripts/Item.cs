using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Item 
{
    public int id;
    public string name;
    public string description;
    public TileBase tileRepresentation;

    public virtual void Use() { }
}
