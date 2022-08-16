using UnityEngine;

public class Item : MonoBehaviour
{
    private Conveyor conveyor;
    private Conveyor next;
    [SerializeField] private ItemObject item;

    public Conveyor Conveyor { get => conveyor; set => conveyor = value; }
    public Conveyor Next { get => next; set => next = value; }
    public ItemObject ItemObj { get => item; set => item = value; }

    void Start()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        transform.position = Conveyor.transform.position;
        if (conveyor.Next is Conveyor) next = (Conveyor) conveyor.Next;
    }

    void LateUpdate()
    {
        if (next != null && next is Conveyor && !next.Full())
        {
            transform.position += conveyor.Direction * conveyor.MoveSpeed * Time.deltaTime;
            if ((next.transform.position.x - transform.position.x) * conveyor.Direction.x < 0 || (next.transform.position.y - transform.position.y) * conveyor.Direction.y < 0)
            {
                conveyor.Item = null;
                next.Item = this;
                conveyor = next;
                UpdateData();
            }
        }
    }

    public void Setup(ItemObject obj)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = obj.Texture;
        ItemObj = obj;
    }
}
