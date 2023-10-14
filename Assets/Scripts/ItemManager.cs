using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemObject> items;

    public List<ItemObject> Items { get => items; }

    public ItemObject FindItemWithName(string name)
    {
        foreach (ItemObject item in items)
        {
            if (item.ItemNameDecorated == name) return item;
        }
        return null;
    }
}
