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
        ReceiveItems();
        MoveItems();
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
        input = CheckInputConnection(inC);
        filterOutput = CheckOutputConnection(filterOutC);
        restOutput1 = CheckOutputConnection(restOutC1);
        restOutput2 = CheckOutputConnection(restOutC2);
    }

    protected override void TransferHoldingToTransit()
    {
        if (holding != null)
        {
            if (holding.ItemObj == filter)
            {
                if (HasHoldItem() && filterOutput is Conveyor && ((Conveyor)filterOutput).CanTakeItem())
                {
                    ((Conveyor)filterOutput).Transit = holding;
                    holding = null;
                    process = 0f;
                }
            }
            else
            {
                if (HasHoldItem() && restOutput1 is Conveyor && ((Conveyor)restOutput1).CanTakeItem())
                {
                    ((Conveyor)restOutput1).Transit = holding;
                    holding = null;
                    process = 0f;
                }
                else if (HasHoldItem() && restOutput2 is Conveyor && ((Conveyor)restOutput2).CanTakeItem())
                {
                    ((Conveyor)restOutput2).Transit = holding;
                    holding = null;
                    process = 0f;
                }
            }
        }
    }
}
