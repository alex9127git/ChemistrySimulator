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

    private int in1, in2, out1, out2;

    void Start()
    {
        InitializeDictionary();
        StartCoroutine(ProduceItems());
        GetComponentInChildren<FactoryUI>(true).CloseFactoryUI();
    }

    private void InitializeDictionary()
    {
        items.Clear();
        inputIDs.Clear();
        outputIDs.Clear();
        switch (tag)
        {
            case "WaterExtractor":
                items.Add(0, new ArrayList());
                outputIDs.Add(0);
                out1 = 0;
                break;
            case "GasExtractor":
                items.Add(3, new ArrayList());
                outputIDs.Add(3);
                out1 = 3;
                break;
            case "ElectroSeparator":
                items.Add(0, new ArrayList());
                items.Add(1, new ArrayList());
                items.Add(2, new ArrayList());
                inputIDs.Add(0);
                outputIDs.Add(1);
                outputIDs.Add(2);
                in1 = 0;
                out1 = 1;
                out2 = 2;
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
                in1 = 3;
                in2 = 0;
                out1 = 4;
                out2 = 1;
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
            GetComponentInChildren<FactoryUI>(true).OpenFactoryUI();
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
                    if (items[out1].Count < 10)
                    {
                        Item item = Instantiate(itemsList[out1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                        item.Id = out1;
                        items[out1].Add(item);
                        item.gameObject.SetActive(false);
                    }
                    break;
                case "GasExtractor":
                    yield return new WaitForSeconds(1);
                    if (items[out1].Count < 10)
                    {
                        Item item = Instantiate(itemsList[out1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                        item.Id = out1;
                        items[out1].Add(item);
                        item.gameObject.SetActive(false);
                    }
                    break;
                case "ElectroSeparator":
                    yield return new WaitForSeconds(1);
                    if (items[in1].Count >= 2)
                    {
                        if (items[out1].Count < 9 && items[out2].Count < 10)
                        {
                            Destroy(((Item)items[in1][0]).gameObject);
                            Destroy(((Item)items[in1][1]).gameObject);
                            items[in1].RemoveRange(0, 2);
                            Item item = Instantiate(itemsList[out1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = out1;
                            items[out1].Add(item);
                            item.gameObject.SetActive(false);
                            item = Instantiate(itemsList[out1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = out1;
                            items[out1].Add(item);
                            item.gameObject.SetActive(false);
                            item = Instantiate(itemsList[out2], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = out2;
                            items[out2].Add(item);
                            item.gameObject.SetActive(false);
                        }
                    }
                    break;
                case "ReagentMixer":
                    yield return new WaitForSeconds(2);
                    if (items[in1].Count >= 1 && items[in2].Count >= 1)
                    {
                        if (items[out1].Count < 10 && items[out2].Count < 8)
                        {
                            Destroy(((Item)items[in1][0]).gameObject);
                            Destroy(((Item)items[in2][0]).gameObject);
                            items[in1].RemoveAt(0);
                            items[in2].RemoveAt(0);
                            Item item = Instantiate(itemsList[out1], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                            item.Id = out1;
                            items[out1].Add(item);
                            item.gameObject.SetActive(false);
                            for (int i = 0; i < 3; i++) {
                                item = Instantiate(itemsList[out2], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                                item.Id = out2;
                                items[out2].Add(item);
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
