using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : PlaceableObject
{
    private Dictionary<Crop, int> allCrops;
    private static int amount = 2; // default amonut of crops produced

    private SpriteRenderer sr;
    private Sprite emptyFieldSprite;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        emptyFieldSprite = sr.sprite;
    }

    private static void Initialize(Dictionary<Crop, int> crops) {
        allCrops = crops;
    }

    protected override void OnClick() {
        Debug.Log("Clicked on the field");
    }
}
