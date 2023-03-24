using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Type;
    public GameObject shopManager;
    public int PriceNum; 
    public string type; // the type of furniture item will be instantiated when it is clicked 
    public Item icon;

    // Start is called before the first frame update
    void Update()
    {
        // Price.text = "Price: " + shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
        if (itemID > 6) {
        Price.text = "Earn: $ "+ PriceNum.ToString();
       } else {
          Price.text = "Price: $ "+ PriceNum.ToString();
        }
        // get the price from the second columns because that is where our price is located 
        // Price.text = "Name: " + shopManager.GetComponent.<ShopManagerScript>().shopItems[2, itemID].ToString(); 
        // get the price from the second columns because that is where our price is located 
          
    }

}