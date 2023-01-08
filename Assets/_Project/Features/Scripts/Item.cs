using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

abstract public class Item 
{
    public int id;
    public string name;
    public string description;
    public Tile tileRepresentation;

    abstract public void Use();
}
