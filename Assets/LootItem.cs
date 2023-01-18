using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem : ScriptableObject
{
    public string itemId = displayName + rarity.ToString();
    public string displayName { get; set; }
    public ItemType type { get; set; }
    public int dropChance { get; set; } // a prob distribution, not exact percentages, higher means more likely to drop compared to other items
    public double weight { get; set; }
    public Sprite icon { get; set; }
    public GameObject prefab { get; set; }


    // Runs when we start our game
    public void Start()
    {

    }

    void OnValidate()
    {

    }
}

public enum ItemType
{
    Fish,
    FishingRod,
    Food,
    Miscellaneous
}
