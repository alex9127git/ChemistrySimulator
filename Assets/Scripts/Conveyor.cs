using UnityEngine;
using static BuildingManager;

public class Conveyor : Building
{
    [SerializeField] protected Building output;
    [SerializeField] protected Building input;
    protected float moveSpeed;
    protected Vector3 direction;
    [SerializeField] protected Item holding;
    [SerializeField] protected Item transit;
    protected Coordinate inC, outC;

    public Building Output { get => output; set => output = value; }
    public Building Input { get => input; set => input = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Coordinate InC { get => inC; set => inC = value; }
    public Coordinate OutC { get => outC; set => outC = value; }
    public Item Holding { get => holding; set => holding = value; }
    public Item Transit { get => transit; set => transit = value; }

    private bool deleted = false;
    private float process;

    void Update()
    {
        CheckMoveAndDelete();
        UpdateInputAndOutput();
        ReceiveItems();
        MoveItems();
    }

    protected virtual void UpdateInputAndOutput()
    {
        input = buildings[inC.X, inC.Y];
        output = buildings[outC.X, outC.Y];
        if (input != null && input is Factory)
        {
            ((Factory)input).Outputs.Add(this);
        }
        if (output != null && output is Factory)
        {
            ((Factory)output).Inputs.Add(this);
        }
    }

    protected override void CheckMoveAndDelete()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (dist < 0.25 && UnityEngine.Input.GetMouseButtonDown(0) && ModeSwitch.modeType == ModeSwitch.deleting)
        {
            RecursiveDelete();
        }
    }

    private void RecursiveDelete()
    {
        if (!deleted)
        {
            deleted = true;
            if (input != null)
            {
                if (input is Conveyor)
                {
                    ((Conveyor)input).RecursiveDelete();
                }
            }
            if (output != null)
            {
                if (output is Conveyor)
                {
                    ((Conveyor)output).RecursiveDelete();
                }
            }
            if (holding != null) Destroy(holding.gameObject);
            if (transit != null) Destroy(transit.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void MoveItems()
    {
        if (HasHoldItem() && output is Conveyor && ((Conveyor)output).CanTakeItem())
        {
            holding.gameObject.SetActive(true);
            ((Conveyor)output).transit = holding;
            holding = null;
            process = 0f;
        }
        if (HasTransitItem())
        {
            process += Time.deltaTime;
            transit.transform.position = Vector3.Lerp(input.transform.position, transform.position, process);
            if (process >= 1)
            {
                transit.transform.position = transform.position;
                holding = transit;
                transit = null;
            }
        }
    }

    protected virtual void ReceiveItems()
    {
        if (input != null)
        {
            if (input is Factory && CanTakeItem())
            {
                transit = ((Factory)input).GiveLastItem();
                if (transit != null)
                {
                    transit.transform.position = transform.position;
                    process = 0f;
                }
            }
        }
    }

    public bool CanTakeItem()
    {
        if (holding != null) return false;
        if (output == null) return holding == null && transit == null;
        else return transit == null;
    }

    public bool HasHoldItem()
    {
        return holding != null;
    }

    public bool HasTransitItem()
    {
        return transit != null;
    }

    public virtual bool Full()
    {
        if (output == null || !(output is Conveyor)) return !CanTakeItem();
        return ((Conveyor)output).Full() && !CanTakeItem();
    }

    public Item GiveItem()
    { 
        Item i = holding;
        holding = null;
        process = 0f;
        return i;
    }
}
