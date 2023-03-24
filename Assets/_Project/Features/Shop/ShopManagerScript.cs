using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
// using InventoryManager;
// using static UnityEngine.Event;

public class ShopManagerScript : MonoBehaviour
{
    public int [,] shopItems = new int[15,15];
    // TODO: change the above values according to how many tems there are
    //      - right now we have 6 items with 6 rows
    public GameObject goldCount; 
    public TextMeshProUGUI coinsTXT;
    public GameObject inventory;

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
        shopItems[1,7] = 7;
        shopItems[1,8] = 8;
        shopItems[1,9] = 9;
        shopItems[1,10] = 10;
        shopItems[1,11] = 11;
        shopItems[1,12] = 12;

        // Setting Price 
        shopItems[2,1] = 50;
        shopItems[2,2] = 70;
        shopItems[2,3] = 40;
        shopItems[2,4] = 50;
        shopItems[2,5] = 60;
        shopItems[2,6] = 35;
        shopItems[2,7] = 10;
        shopItems[2,8] = 5;
        shopItems[2,9] = 20;
        shopItems[2,10] = 5;
        shopItems[2,11] = 10;
        shopItems[2,12] = 15;

    }

    void Update()
    {
        coinsTXT.text = "Coins: $" + goldCount.GetComponent<GoldController>().gold.ToString();
    }

    
    public void Buy()
    {
        GameObject ButtonRef = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
       //  GameObject ButtonRef = GameObject.FindFirstObjectOfType(Event).GetComponent<Event>().currentSelectedGameObject;
        if ((goldCount.GetComponent<GoldController>().gold >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID])) { 
            // checking if we have enough coins to purchase our item and if the items is for buying or selling (second statement above)
            goldCount.GetComponent<GoldController>().SubtractGold(shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]); // subtract the amount it costed from the ammount of coins 
            coinsTXT.text = "Coins: $" + goldCount.GetComponent<GoldController>().gold.ToString();
            addToInventory(ButtonRef.GetComponent<ButtonInfo>().type, ButtonRef.GetComponent<ButtonInfo>().itemID, ButtonRef.GetComponent<ButtonInfo>().icon);
            // inventoryManager.GetComponent<InventoryManager>().add(, 1);
        } else {
            StartCoroutine(errorNoMoney());
        }
    }

    // TODO: fix this so that it displays a success message when added sucessfully and an error message if there aren't enough coins
    public IEnumerator errorNoMoney() {
        yield return new WaitForSeconds(4);
        coinsTXT.text = "Not enough money!";
        // coinsTXT.text = "Coins: $" + goldCount.GetComponent<GoldController>().gold.ToString();
    }

    public void Sell() 
    {
        GameObject ButtonRef = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (inventory.GetComponent<InventoryManager>().Contains(ButtonRef.GetComponent<ButtonInfo>().icon) != null) { 
        goldCount.GetComponent<GoldController>().AddGold(shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]); // subtract the amount it costed from the ammount of coins 
        coinsTXT.text = "Coins: $ " + goldCount.GetComponent<GoldController>().gold.ToString();
        removeFromInventory(ButtonRef.GetComponent<ButtonInfo>().icon);
        }
    }

    public void addToInventory(string item, int id, Item newItem) {
        // FurniturePlaceHolder newItem = new FurniturePlaceHolder();
        // newItem.setID(id);
        // newItem.setName(item);
        // newItem.setIcon(tile);
        // newItem.setTile(tile);
        inventory.GetComponent<InventoryManager>().Add(newItem, 1);
        // InventoryManager.Instance.Add(newItem, 1);
    }

     public void removeFromInventory(Item toRemove) {
        InventoryManager.Instance.Remove(toRemove);
        //inventory.GetComponent<InventoryManager>().Remove(toRemove);
    }
}