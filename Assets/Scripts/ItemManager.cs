using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemObject[] items;

    public ItemObject FindItemWithName(string name)
    {
        string n = name;
        n = n.Replace("<sub>", "");
        n = n.Replace("</sub>", "");
        foreach (ItemObject item in items)
        {
            if (item.ItemName == n) return item;
        }
        return null;
    }
}
