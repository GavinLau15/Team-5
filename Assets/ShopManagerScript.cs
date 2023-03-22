using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using static UnityEngine.Event;

public class ShopManagerScript : MonoBehaviour
{
    public int [,] shopItems = new int[7,7];
    // TODO: change the above values according to how many tems there are
    //      - right now we have 6 items with 6 rows
    public GameObject goldCount; 
    public TextMeshProUGUI coinsTXT;

    void Start()
    {
        coinsTXT.text = "Coins: $" + goldCount.GetComponent<GoldController>().gold.ToString();

        // setting up the IDs (column one will be the IDs)
        // change the amount of ID assignmnts depending on the amount of items you have 
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        shopItems[1,5] = 5;
        shopItems[1,6] = 6;

        // Setting Price 
        shopItems[2,1] = 10;
        shopItems[2,2] = 5;
        shopItems[2,3] = 20;
        shopItems[2,4] = 5;
        shopItems[2,5] = 10;
        shopItems[2,6] = 15;

        // // Setting Type as to sell or to buy (1 = buy, 0 = sell)
        // // buy -> furniture
        // // sell -> fish for money 
        // shopItems[3,1] = 1;
        // shopItems[3,2] = 1;
        // shopItems[3,3] = 1;
        // shopItems[3,4] = 1;
        // shopItems[3,5] = 0;
        // shopItems[3,6] = 0;

    }

    
    public void Buy()
    {
        GameObject ButtonRef = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
       //  GameObject ButtonRef = GameObject.FindFirstObjectOfType(Event).GetComponent<Event>().currentSelectedGameObject;
        if ((goldCount.GetComponent<GoldController>().gold >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID])) { 
            // checking if we have enough coins to purchase our item and if the items is for buying or selling (second statement above)
            goldCount.GetComponent<GoldController>().SubtractGold(shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]); // subtract the amount it costed from the ammount of coins 
            coinsTXT.text = "Coins: $" + goldCount.GetComponent<GoldController>().gold.ToString();

        }
    }

    // public void Sell() 
    // {
    //     GameObject ButtonRef = GameObject.FindObjectOfType(Event).GetComponent<Event>().currentSelectedGameObject;
    //     if (shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID] == 0) {  // TODO: need to add a check here to see if the player has a fish to sell 
    //         // checking if we have enough coins to purchase our item and if the items is for buying or selling (second statement above)
    //         goldCount.AddGold(goldCount.getGold() + shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]); // subtract the amount it costed from the ammount of coins 
    //         coinsTxt.text = "Coins: $" + goldCount.getGold().ToString();
            
    //     }
    // }
}
