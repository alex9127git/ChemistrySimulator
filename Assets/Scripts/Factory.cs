using System.Collections;
using UnityEngine;

public class Factory : Building
{
    private ArrayList inputs = new ArrayList();
    private ArrayList outputs = new ArrayList();

    public ArrayList Outputs { get => outputs; set => outputs = value; }
    public ArrayList Inputs { get => inputs; set => inputs = value; }

    private ArrayList inputItems = new ArrayList(10);
    private Queue outputItems = new Queue(10);

    private const int inputCapacity = 10;
    private const int outputCapacity = 10;

    [SerializeField] private Transform prefab;

    [SerializeField] private ReactionObject currentReaction;

    void Start()
    {
        StartCoroutine(ProduceItems());
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
        foreach (Conveyor input in inputs)
        {
            if (input != null && input.HasItem() && inputItems.Count < inputCapacity)
            {
                Item item = input.GiveItem();
                ItemObject obj = item.ItemObj;
                inputItems.Add(obj);
                Destroy(item.gameObject);
            }
        }
    }

    IEnumerator ProduceItems()
    {
        while (true)
        {
            ArrayList storage = (ArrayList)inputItems.Clone();
            bool canProduce = true;
            foreach (ItemObject input in currentReaction.Inputs)
            {              
                if (storage.Contains(input))
                {
                    storage.Remove(input);
                }
                else
                {
                    canProduce = false;
                }
            }
            if (canProduce && outputItems.Count <= outputCapacity - currentReaction.Outputs.Length)
            {
                inputItems = (ArrayList)storage.Clone();
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
            Item item = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Item>();
            item.Setup(obj);
            outputItems.Dequeue();
            return item;
        }
        return null;
    }
}
