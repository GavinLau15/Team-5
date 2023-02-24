using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   [SerializeField] private GameObject mainInventory;
   [SerializeField] private GameObject toolbar;
	[SerializeField] private Item itemToAdd;
	[SerializeField] private Item itemToRemove;
	public List<Item> inventory = new List<Item>();

   private GameObject[] slots;

	// Tester method
	// In the future, will change to when an item is clicked on/player
	// runs into item to add to inventory
	public void Start() {
      slots = new GameObject[toolbar.transform.childCount + mainInventory.transform.childCount];
   	
      // set all slots
   for (int i = 0; i < toolbar.transform.childCount; i++)
      slots[i] = toolbar.transform.GetChild(i).gameObject;

   for (int i = 0; i < mainInventory.transform.childCount; i++)
      slots[i + toolbar.transform.childCount] = mainInventory.transform.GetChild(i).gameObject;

      RefreshUI();

      Add(itemToAdd);
      Remove(itemToRemove);
   }

   public void RefreshUI()
   {
 //     for (int i = 0; i < slots.length; i++)
    //  {
    //     slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemIcon;
    //  }
  }

   public void Add(Item item) {
   	inventory.Add(item);
      RefreshUI();
   }

   public void Remove(Item item) {
   	inventory.Remove(item);
      RefreshUI();
   }
}
