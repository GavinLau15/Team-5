using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
  [SerializeField] private GameObject mainInventory;
	[SerializeField] private Item itemToAdd;
	[SerializeField] private Item itemToRemove;
	public List<Slot> inventory = new List<Slot>();

   private GameObject[] slots;

	// Tester method
	// In the future, will change to when an item is clicked on/player
	// runs into item to add to inventory
	public void Start() {
      slots = new GameObject[mainInventory.transform.childCount];
   	
      // set all slots
      for (int i = 0; i < mainInventory.transform.childCount; i++) {
        slots[i] = mainInventory.transform.GetChild(i).gameObject;
      }

      RefreshUI();

      Add(itemToAdd);
      Remove(itemToRemove);
   }

   public void RefreshUI() {
    for (int i = 0; i < slots.Length; i++) {
      try {
        slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].GetItem().itemIcon;
        
        if (inventory[i].GetItem().isStackable) {
          slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].GetQuantity().ToString();
        } else {
          slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        }
      } catch {
        slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
        slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
      }
    }
  }

   public bool Add(Item item) {
    // check if the inventory already contains item


    Slot slot = Contains(item);
    if (slot != null && slot.GetItem().isStackable) {
      slot.AddQuantity(1);
      } else {
        if (inventory.Count < slots.Length) {
        inventory.Add(new Slot(item, 1));
        } else {
          return false;
        }
      }

      RefreshUI();
      return true;
   }

   public bool Remove(Item item) {
    Slot temp = Contains(item);
    if (temp != null) {
      if (temp.GetQuantity() > 1) {
        temp.SubtractQuantity(1);
        } else {
          Slot slotToRemove = new Slot();
          foreach (Slot slot in inventory) {
            if (slot.GetItem() == item) {
              slotToRemove = slot;
              break;
      }
    }
    inventory.Remove(slotToRemove);
  }
} else {
  return false;
}
RefreshUI();
return true;
}

   // if inventory contains slot with item, returns slot, else returns null
   public Slot Contains(Item item) {
    foreach (Slot slot in inventory) {
      if (slot.GetItem() == item) {
        return slot;
      }
    }

    return null;
   }
}
