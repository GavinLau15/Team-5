using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages the overall inventory 
public class InventoryManager : MonoSingletonPersistent<InventoryManager>
{
    [SerializeField] private GameObject itemCursor;
    [SerializeField] private GameObject mainInventory;
    [SerializeField] private GameObject toolbarInventory;
    [SerializeField] public Item itemToAdd;
    [SerializeField] private Item itemToRemove;
    [SerializeField] private Slot[] startingItems;

    private Slot[] inventory;

    private GameObject[] slots;
    private GameObject[] toolbarSlots;

    private Slot movingSlot;
    private Slot tempSlot;
    private Slot originalSlot;
    bool isMovingItem;

    [SerializeField] private GameObject toolbarSelector;
    [SerializeField] private int selectedSlotIndex = 0;
    public Item selectedItem;

    // Tester method
    // In the future, will change to when an item is clicked on/player
    // runs into item to add to inventory
    private void Start()
    {
        slots = new GameObject[mainInventory.transform.childCount];
        inventory = new Slot[slots.Length];

        toolbarSlots = new GameObject[toolbarInventory.transform.childCount];

        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            toolbarSlots[i] = toolbarInventory.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = new Slot();
        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            inventory[i] = startingItems[i];
        }


        // sets all slots
        for (int i = 0; i < mainInventory.transform.childCount; i++)
        {
            slots[i] = mainInventory.transform.GetChild(i).gameObject;
        }

        RefreshUI();

        Add(itemToAdd, 1);
        Remove(itemToRemove);
    }

    // detects when mouse is clicked down
    private void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;

        if (isMovingItem)
        {
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
        }

        if (Input.GetMouseButtonDown(0))
        { // moue was clicked
          // find the closest slot (the slot that was clicked on)
            if (isMovingItem)
            {
                // end item move
                EndItemMove();
            }
            else
            {
                BeginItemMove();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex + 1, 0, toolbarSlots.Length - 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex - 1, 0, toolbarSlots.Length - 1);
        }
        toolbarSelector.transform.position = toolbarSlots[selectedSlotIndex].transform.position;
        selectedItem = inventory[selectedSlotIndex].GetItem();
    }

    // refreshes UI inventory by setting the item sprites and quanitities
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].GetItem().itemIcon;

                if (inventory[i].GetItem().isStackable)
                {
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].GetQuantity().ToString();
                }
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        RefreshToolbar();
    }

    public void RefreshToolbar()
    {
        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            try
            {
                toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].GetItem().itemIcon;

                if (inventory[i].GetItem().isStackable)
                {
                    toolbarSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].GetQuantity().ToString();
                }
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
            catch
            {
                toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                toolbarSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }


    // if the inventory is not full:
    //      - checks if the inventory already contains the item and if the item is stackable
    //      - if item isn't stackable or isn't in inventory, adds it to a new slot
    // else, does not add item to inventory
    public bool Add(Item item, int quantity)
    {
        Slot slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(quantity);
        }
        else
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].GetItem() == null)
                { // this is an empty slot
                    inventory[i].AddItem(item, quantity);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    }

    // if the inventory contains the item:
    //     - if the item is stackable and there is more than one in the inventory:
    //          - subtracts one from the item quantity
    //     - else, completely removes item from inventory
    public bool Remove(Item item)
    {
        Slot temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            {
                temp.SubtractQuantity(1);
            }
            else
            {
                int slotToRemoveIndex = 0;
                for (int i = 0; i < inventory.Length; i++)
                {
                    if (inventory[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                inventory[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }

    // if inventory contains slot with item, returns slot, else returns null
    public Slot Contains(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetItem() == item)
            {
                return inventory[i];
            }
        }
        return null;
    }

    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
        {
            return false; // there is no item to move
        }
        movingSlot = new Slot(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();

        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())
                { // they're the same item and should stack
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                { // swap items
                    tempSlot = new Slot(originalSlot);
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity());
                    RefreshUI();
                    return true;
                }
            }
            else
            { // place item as usual
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }
        isMovingItem = false;
        RefreshUI();
        return true;
    }

    // returns the closest slot
    private Slot GetClosestSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 29)
            {
                return inventory[i];
            }
        }
        return null;
    }
}
