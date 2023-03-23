using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Type;
    public GameObject shopManager;
    // Start is called before the first frame update
    void Update()
    {
        if (itemID > 6) {
             Price.text = "Earn: $ "+ shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
        } else {
            Price.text = "Price: $ "+ shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
        }
        // get the price from the second columns because that is where our price is located 
        // Price.text = "Name: " + shopManager.GetComponent.<ShopManagerScript>().shopItems[2, itemID].ToString(); 
        // get the price from the second columns because that is where our price is located 
          
    }

}
