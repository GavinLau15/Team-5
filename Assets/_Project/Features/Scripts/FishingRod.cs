using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/FishingRod")]
public class FishingRod : Item
{
    public override void Use() {
        GoFishing();
    }

    public Fish GoFishing()
    {
        //TODO: Figure out how to get a list of all fishes. This is assuming we already have a list of Fish items.
        List<Fish> fishDropRates = new List<Fish>();
        int total = 0;

        foreach (Fish fish in fishDropRates)
        {
            total += fish.dropChance;
        }

        int rng = Random.Range(0, total);

        foreach (Fish fish in fishDropRates)
        {
            if (rng <= fish.dropChance)
            {
                return fish;
            }
            else
            {
                rng -= fish.dropChance;
            }
        }
        return null;
    }
}
