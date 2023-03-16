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

  // constructs a slot from a given slot
  public Slot (Slot slot) {
    this.item = slot.GetItem();
    this.quantity = slot.GetQuantity();
  }

  public void Clear() {
    this.item = null;
    this.quantity = 0;
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

  public void AddItem(Item item, int quantity) {
    this.item = item;
    this.quantity = quantity;
  }
}
