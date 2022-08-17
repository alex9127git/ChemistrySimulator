using UnityEngine;
using static BuildingManager;

public class Conveyor : Building
{
    private Building next;
    private Building previous;
    private float moveSpeed;
    private Vector3 direction;
    private Item item;
    private int inX, inY, outX, outY;

    public Building Next { get => next; set => next = value; }
    public Building Previous { get => previous; set => previous = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Item Item { get => item; set => item = value; }
    public int InX { get => inX; set => inX = value; }
    public int InY { get => inY; set => inY = value; }
    public int OutX { get => outX; set => outX = value; }
    public int OutY { get => outY; set => outY = value; }

    private bool deleted = false;

    void Start()
    {
    }

    void Update()
    {
        CheckMoveAndDelete();
        UpdateDirection();
        ReceiveItems();
        UpdateInputAndOutput();
    }

    void UpdateInputAndOutput()
    {
        previous = buildings[inX, inY] != null ? buildings[inX, inY].GetComponent<Building>() : null;
        next = buildings[outX, outY] != null ? buildings[outX, outY].GetComponent<Building>() : null;
        if (previous != null && previous is Factory)
        {
            ((Factory)previous).Outputs.Add(this);
        }
        if (previous != null && next is Factory)
        {
            ((Factory)next).Inputs.Add(this);
        }
    }

    protected override void CheckMoveAndDelete()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (dist < 0.25 && Input.GetMouseButtonDown(0) && ModeSwitch.modeType == ModeSwitch.deleting)
        {
            RecursiveDelete();
        }
    }

    private void RecursiveDelete()
    {
        if (!deleted)
        {
            deleted = true;
            if (previous != null)
            {
                if (previous is Conveyor)
                {
                    ((Conveyor)previous).RecursiveDelete();
                }
            }
            if (next != null)
            {
                if (next is Conveyor)
                {
                    ((Conveyor)next).RecursiveDelete();
                }
            }
            if (item != null) Destroy(item.gameObject);
            Destroy(gameObject);
        }
    }

    private void UpdateDirection()
    {
        moveSpeed = 1f;
        Transform transform1 = gameObject.transform;
        if (next != null)
        {
            Transform transform2 = next.gameObject.transform;
            direction = new Vector3(transform2.position.x - transform1.position.x, transform2.position.y - transform1.position.y);
        }
        else
        {
            direction = new Vector3(0, 0, 0);
        }
    }

    void ReceiveItems()
    {
        if (previous != null && previous is Factory && CanTakeItem())
        {
            item = ((Factory)previous).GiveLastItem();
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

    public bool Full()
    {
        if (next == null || !(next is Conveyor)) return !CanTakeItem();
        return ((Conveyor)next).Full() && !CanTakeItem();
    }

    public Item GiveItem()
    { 
        Item i = item;
        item = null;
        return i;
    }
}
