using System.Collections;
using UnityEngine;

// represents a slot in the inventory
[System.Serializable]
public class Slot {
  [SerializeField] private Item item;
  [SerializeField] private int quantity;

  // constructs an empty slot
  public Slot() {
    item = null;
    quantity = 0;
  }

  // constructs a slot with given item and quantity
  public Slot (Item _item, int _quantity) {
  	item = _item;
  	quantity = _quantity; 
  }

  // getters
  public Item GetItem() { return item; }
  public int GetQuantity() { return quantity; }

  // quantity setters
  public void AddQuantity(int _quantity) {
  	quantity += _quantity;
  }

  public void SubtractQuantity(int _quantity) {
    quantity -= _quantity;
  }
}
