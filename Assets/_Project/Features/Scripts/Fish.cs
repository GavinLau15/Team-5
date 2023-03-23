using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Fish")]
public class Fish : Item
{
	public FishType fishType;
    public int dropChance;
    public int saleValue;
    
    public enum FishType 
    {
    	BlackCrappie,
    	Bluegill,
    	Walleye,
    	ChannelCatfish,
    	YellowPerch
    }

    public override Item getItem(){ return this; }
}
