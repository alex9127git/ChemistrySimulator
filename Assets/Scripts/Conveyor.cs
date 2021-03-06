using UnityEngine;

public class Conveyor : Building
{
    private Building next;
    private Building previous;
    private float moveSpeed;
    private Vector3 direction;
    private Item item;

    public Building Next { get => next; set => next = value; }
    public Building Previous { get => previous; set => previous = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Item Item { get => item; set => item = value; }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        ReceiveItems();
    }

    private void Initialize()
    {
        moveSpeed = 1f;
        Transform transform1 = gameObject.transform;
        if (next != null)
        {
            Transform transform2 = next.gameObject.transform;
            direction = new Vector3(transform2.position.x - transform1.position.x, transform2.position.y - transform1.position.y);
        } else
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
