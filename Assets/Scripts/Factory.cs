using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global;

public class Factory : Building
{
    private ArrayList inputs = new ArrayList();
    private ArrayList outputs = new ArrayList();
    private Dictionary<int, ArrayList> items = new Dictionary<int, ArrayList>();
    private ArrayList inputIDs = new ArrayList();
    private ArrayList outputIDs = new ArrayList();

    public ArrayList Outputs { get => outputs; set => outputs = value; }
    public ArrayList Inputs { get => inputs; set => inputs = value; }

    void Start()
    {
        InitializeDictionary();
        StartCoroutine(ProduceItems());
        GetComponentInChildren<FactoryUI>(true).CloseFactoryUI();
    }

    private void InitializeDictionary()
    {
        switch (tag)
        {
            case "WaterExtractor":
                items.Add(0, new ArrayList());
                outputIDs.Add(0);
                break;
            case "GasExtractor":
                items.Add(3, new ArrayList());
                outputIDs.Add(3);
                break;
            case "ElectroSeparator":
                items.Add(0, new ArrayList());
                items.Add(1, new ArrayList());
                items.Add(2, new ArrayList());
                inputIDs.Add(0);
                outputIDs.Add(1);
                outputIDs.Add(2);
                break;
            case "ReagentMixer":
                items.Add(3, new ArrayList());
                items.Add(0, new ArrayList());
                items.Add(4, new ArrayList());
                items.Add(1, new ArrayList());
                inputIDs.Add(3);
                inputIDs.Add(0);
                outputIDs.Add(4);
                outputIDs.Add(1);
                break;
        }
    }

    void Update()
    {
        CheckMoveAndDelete();
        UpdateUI();
        AcceptItems();
    }

    private void UpdateUI()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (Input.GetMouseButtonDown(0) && dist < 1 && ModeSwitch.modeType == 0)
        {
            GetComponentInChildren<FactoryUI>(true).OpenFactoryUI(this);
        }
    }

    private void AcceptItems()
    {
        foreach (Conveyor input in inputs) { 
            if (input != null && input.HasItem())
            {
                Item item = input.Item;
                int id = item.Id;
                if (inputIDs.Contains(id))
                {
                    if (items[id].Count < 10)
                    {
                        item = input.GiveItem();
                        items[id].Add(item);
                        item.gameObject.SetActive(false);
                        item.transform.position = transform.position;
                    }
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
                case "GasExtractor":
                    yield return new WaitForSeconds(1);
                    if (items[3].Count < 10)
                    {
                        Item item = Instantiate(itemsList[3], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                        item.Id = 3;
                        items[3].Add(item);
                        item.gameObject.SetActive(false);
                    }
                    break;
                case "ElectroSeparator":
                    yield return new WaitForSeconds(1);
                    if (items[0].Count >= 2)
                    {
                        if (items[1].Count < 9 && items[2].Count < 10)
                        {
                            Destroy(((Item)items[0][0]).gameObject);
                            Destroy(((Item)items[0][1]).gameObject);
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
                case "ReagentMixer":
                    yield return new WaitForSeconds(2);
                    if (items[3].Count >= 1 && items[0].Count >= 1)
                    {
                        if (items[4].Count < 10 && items[1].Count < 8)
                        {
                            Destroy(((Item)items[3][0]).gameObject);
                            Destroy(((Item)items[0][0]).gameObject);
                            items[0].RemoveAt(0);
                            items[3].RemoveAt(0);
                            Item item = Instantiate(itemsList[4], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = 4;
                            items[4].Add(item);
                            item.gameObject.SetActive(false);
                            for (int i = 0; i < 3; i++) {
                                item = Instantiate(itemsList[1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                                item.Id = 1;
                                items[1].Add(item);
                                item.gameObject.SetActive(false);
                            }
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
