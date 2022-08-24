using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemObject item;

    public ItemObject ItemObj { get => item; set => item = value; }

    void Start()
    {
        
    }

    public void Setup(ItemObject obj)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = obj.Texture;
        ItemObj = obj;
    }
}
