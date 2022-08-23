using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuildingManager;

public class Factory : Building
{
    private ArrayList inputs = new ArrayList();
    private ArrayList outputs = new ArrayList();

    public ArrayList Outputs { get => outputs; set => outputs = value; }
    public ArrayList Inputs { get => inputs; set => inputs = value; }

    private Dictionary<ItemObject, int> inputItems = new Dictionary<ItemObject, int>();
    private Queue outputItems = new Queue(10);

    private const int inputCapacity = 10;
    private const int outputCapacity = 10;

    [SerializeField] private Item prefab;

    [SerializeField] private ReactionObject currentReaction;

    public ReactionObject Reaction { get => currentReaction; set => currentReaction = value; }

    void Start()
    {
        StartCoroutine(ProduceItems());
        GetComponentInChildren<FactoryUI>(true).CloseFactoryUI();
    }

    void Update()
    {
        CheckMoveAndDelete();
        UpdateUI();
        UpdateInputsAndOutputs();
        AcceptItems();
    }

    private void UpdateInputsAndOutputs()
    {
        inputs.Remove(null);
        outputs.Remove(null);
    }

    private void UpdateUI()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (Input.GetMouseButtonDown(0) && dist < DetermineBuildingSize(gameObject.tag) * 0.5f - 0.25f && ModeSwitch.modeType == ModeSwitch.spying)
        {
            GetComponentInChildren<FactoryUI>(true).OpenFactoryUI();
        }
    }

    private void AcceptItems()
    {
        foreach (Conveyor input in inputs)
        {
            if (input != null && input.HasItem())
            {
                Item item = input.GiveItem();
                ItemObject obj = item.ItemObj;
                if (!inputItems.ContainsKey(obj))
                {
                    inputItems[obj] = 0;
                }
                if (inputItems[obj] < inputCapacity)
                {
                    inputItems[obj] += 1;
                    Destroy(item.gameObject);
                }
            }
        }
    }

    IEnumerator ProduceItems()
    {
        while (true)
        {
            Dictionary<ItemObject, int> storage = CloneDictoinary(inputItems);
            bool canProduce = true;
            foreach (ItemObject input in currentReaction.Inputs)
            {
                int inputQuantity = currentReaction.GetInputQuantity(input);
                if (storage.ContainsKey(input) && storage[input] >= inputQuantity)
                {
                    storage[input] -= inputQuantity;
                }
                else
                {
                    canProduce = false;
                }
            }
            if (canProduce && outputItems.Count <= outputCapacity - currentReaction.Outputs.Length)
            {
                inputItems = CloneDictoinary(storage);
                foreach (ItemObject output in currentReaction.Outputs)
                {
                    outputItems.Enqueue(output);
                }
            }
            yield return new WaitForSeconds(currentReaction.ReactionTime);
        }
    }

    public Item GiveLastItem()
    {
        if (outputItems.Count > 0)
        {
            ItemObject obj = (ItemObject)outputItems.Peek();
            Item item = Instantiate(prefab, transform.position, Quaternion.identity);
            item.Setup(obj);
            outputItems.Dequeue();
            return item;
        }
        return null;
    }

    public int OutputItemsCount()
    {
        return outputItems.Count;
    }

    private Dictionary<ItemObject, int> CloneDictoinary(Dictionary<ItemObject, int> dict)
    {
        Dictionary<ItemObject, int> cloned = new Dictionary<ItemObject, int>();
        foreach (ItemObject itemObj in dict.Keys)
        {
            cloned[itemObj] = dict[itemObj];
        }
        return cloned;
    }
}
