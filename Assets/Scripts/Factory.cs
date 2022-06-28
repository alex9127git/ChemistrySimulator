using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global;

public class Factory : Building
{
    private Conveyor input;
    private Conveyor output;
    private Dictionary<int, ArrayList> items = new Dictionary<int, ArrayList>();
    private ArrayList inputIDs = new ArrayList();
    private ArrayList outputIDs = new ArrayList();

    public Conveyor Output { get => output; set => output = value; }
    public Conveyor Input { get => input; set => input = value; }

    void Start()
    {
        InitializeDictionary();
        StartCoroutine(ProduceItems());
    }

    private void InitializeDictionary()
    {
        switch (tag)
        {
            case "WaterExtractor":
                items.Add(0, new ArrayList());
                outputIDs.Add(0);
                break;
            case "ElectroSeparator":
                items.Add(0, new ArrayList());
                items.Add(1, new ArrayList());
                items.Add(2, new ArrayList());
                inputIDs.Add(0);
                outputIDs.Add(1);
                outputIDs.Add(2);
                break;
        }
    }

    void Update()
    {
        CheckMoveAndDelete();
        AcceptItems();
    }

    private void AcceptItems()
    {
        if (Input != null && Input.HasItem())
        {
            Item item = Input.Item;
            int id = item.Id;
            if (inputIDs.Contains(id))
            {
                if (items[id].Count < 10)
                {
                    item = Input.GiveItem();
                    items[id].Add(item);
                    item.gameObject.SetActive(false);
                    item.transform.position = transform.position;
                }
            }
        }
    }

    IEnumerator ProduceItems()
    {
        while (true)
        {
            switch (tag)
            {
                case "WaterExtractor":
                    yield return new WaitForSeconds(1);
                    if (items[0].Count < 10)
                    {
                        Item item = Instantiate(itemsList[0], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                        item.Id = 0;
                        items[0].Add(item);
                        item.gameObject.SetActive(false);
                    }
                    break;
                case "ElectroSeparator":
                    yield return new WaitForSeconds(1);
                    if (items[0].Count >= 2)
                    {
                        if (items[1].Count < 9 && items[2].Count < 10)
                        {
                            items[0].RemoveRange(0, 2);
                            Item item = Instantiate(itemsList[1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = 1;
                            items[1].Add(item);
                            item.gameObject.SetActive(false);
                            item = Instantiate(itemsList[1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = 1;
                            items[1].Add(item);
                            item.gameObject.SetActive(false);
                            item = Instantiate(itemsList[2], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = 2;
                            items[2].Add(item);
                            item.gameObject.SetActive(false);
                        }
                    }
                    break;
            }
        }
    }

    public Item GiveLastItem()
    {
        foreach (int id in outputIDs)
        {
            if (items[id].Count > 0)
            {
                Item item = (Item)items[id][0];
                items[id].RemoveAt(0);
                return item;
            }
        }
        return null;
    }
}
