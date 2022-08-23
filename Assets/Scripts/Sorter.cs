using static BuildingManager;
using UnityEngine;

public class Sorter : Conveyor
{
    protected Building filterOutput, restOutput1, restOutput2;
    protected Coordinate restOutC2;
    protected Coordinate filterOutC;
    protected Coordinate restOutC1;
    private ItemObject filter;

    public Coordinate FilterOutC { get => filterOutC; set => filterOutC = value; }
    public Coordinate RestOutC1 { get => restOutC1; set => restOutC1 = value; }
    public Coordinate RestOutC2 { get => restOutC2; set => restOutC2 = value; }
    public ItemObject Filter { get => filter; set => filter = value; }

    void Start()
    {
        GetComponentInChildren<BuildingUI>(true).CloseUI();
        filter = GameObject.Find("ItemManager").GetComponent<ItemManager>().FindItemWithName("H2O");
    }

    void Update()
    {
        CheckMoveAndDelete();
        UpdateUI();
        UpdateInputAndOutput();
        UpdateDirection();
        ReceiveItems();
    }

    private void UpdateUI()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (UnityEngine.Input.GetMouseButtonDown(0) && dist < DetermineBuildingSize(gameObject.tag) * 0.5f - 0.25f && ModeSwitch.modeType == ModeSwitch.spying)
        {
            GetComponentInChildren<BuildingUI>(true).OpenUI();
        }
    }

    protected override void UpdateInputAndOutput()
    {
        input = buildings[InC.X, InC.Y];
        filterOutput = buildings[FilterOutC.X, FilterOutC.Y];
        restOutput1 = buildings[RestOutC1.X, RestOutC1.Y];
        restOutput2 = buildings[RestOutC2.X, RestOutC2.Y];
        if (input != null && input is Factory)
        {
            ((Factory)input).Outputs.Add(this);
        }
        if (filterOutput != null && filterOutput is Factory)
        {
            ((Factory)filterOutput).Inputs.Add(this);
        }
        if (restOutput1 != null && restOutput1 is Factory)
        {
            ((Factory)restOutput1).Inputs.Add(this);
        }
        if (restOutput2 != null && restOutput2 is Factory)
        {
            ((Factory)restOutput2).Inputs.Add(this);
        }
    }

    protected override void UpdateDirection()
    {
        moveSpeed = 1f;
        Transform transform1 = gameObject.transform;
        if (item != null) {
            if (item.ItemObj == filter)
            {
                if (filterOutput != null)
                {
                    output = filterOutput;
                    Transform transform2 = output.gameObject.transform;
                    direction = new Vector3(transform2.position.x - transform1.position.x, transform2.position.y - transform1.position.y);
                }
                else
                {
                    direction = new Vector3(0, 0, 0);
                }
            }
            else
            {
                if (restOutput1 != null)
                {
                    output = restOutput1;
                    Transform transform2 = output.gameObject.transform;
                    direction = new Vector3(transform2.position.x - transform1.position.x, transform2.position.y - transform1.position.y);
                }
                else if (restOutput2 != null)
                {
                    output = restOutput2;
                    Transform transform2 = output.gameObject.transform;
                    direction = new Vector3(transform2.position.x - transform1.position.x, transform2.position.y - transform1.position.y);
                } 
                else
                {
                    direction = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
