using UnityEngine;
using static BuildingManager;

public class Conveyor : Building
{
    protected Building output;
    protected Building input;
    protected float moveSpeed;
    protected Vector3 direction;
    protected Item item;
    protected Coordinate inC, outC;

    public Building Output { get => output; set => output = value; }
    public Building Input { get => input; set => input = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Item Item { get => item; set => item = value; }
    public Coordinate InC { get => inC; set => inC = value; }
    public Coordinate OutC { get => outC; set => outC = value; }

    private bool deleted = false;

    void Update()
    {
        CheckMoveAndDelete();
        UpdateInputAndOutput();
        UpdateDirection();
        ReceiveItems();
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
            if (item != null) Destroy(item.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void UpdateDirection()
    {
        moveSpeed = 1f;
        Transform transform1 = gameObject.transform;
        if (output != null)
        {
            Transform transform2 = output.gameObject.transform;
            direction = new Vector3(transform2.position.x - transform1.position.x, transform2.position.y - transform1.position.y);
        }
        else
        {
            direction = new Vector3(0, 0, 0);
        }
    }

    protected virtual void ReceiveItems()
    {
        if (input != null && input is Factory && CanTakeItem())
        {
            item = ((Factory)input).GiveLastItem();
            if (item != null)
            {
                item.gameObject.SetActive(true);
                item.Conveyor = gameObject.GetComponent<Conveyor>();
            }
        }
    }

    public bool CanTakeItem()
    {
        return item == null;
    }

    public bool HasItem()
    {
        return item != null;
    }

    public virtual bool Full()
    {
        if (output == null || !(output is Conveyor)) return !CanTakeItem();
        return ((Conveyor)output).Full() && !CanTakeItem();
    }

    public Item GiveItem()
    { 
        Item i = item;
        item = null;
        return i;
    }
}
