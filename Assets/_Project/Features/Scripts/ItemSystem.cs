using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSystem : MonoSingletonPersistent<ItemSystem>
{
     [SerializeField] private ItemDatabase itemDatabase;

    public Item GetItem(int id)
    {
        return itemDatabase.items.Find(item => item.id == id);
    }
}
