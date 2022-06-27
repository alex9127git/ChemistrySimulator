using System.Collections;
using UnityEngine;
using static Global;

public class Factory : Building
{
    private Conveyor output;
    private ArrayList items = new ArrayList();

    public Conveyor Output { get => output; set => output = value; }

    void Start()
    {
        StartCoroutine(ProduceItems());
    }

    void Update()
    {
        CheckMoveAndDelete();
    }

    IEnumerator ProduceItems()
    {
        while (true)
        {
            switch (tag)
            {
                case "WaterExtractor":
                    yield return new WaitForSeconds(1);
                    if (items.Count < 10)
                    {
                        Item item = Instantiate(itemsList[0], transform.position, Quaternion.identity).gameObject.GetComponent<Item>();
                        items.Add(item);
                        item.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public Item GiveLastItem()
    {
        if (items.Count > 0)
        {
            Item item = (Item)items[0];
            items.RemoveAt(0);
            return item;
        }
        else return null;
    }
}
