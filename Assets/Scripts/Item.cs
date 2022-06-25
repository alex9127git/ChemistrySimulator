using UnityEngine;

public class Item : MonoBehaviour
{
    private Conveyor conveyor;
    private Conveyor next;

    public Conveyor Conveyor { get => conveyor; set => conveyor = value; }
    public Conveyor Next { get => next; set => next = value; }

    void Start()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        if (conveyor.Next is Conveyor) next = (Conveyor) conveyor.Next;
    }

    void LateUpdate()
    {
        if (next.CanTakeItem())
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
}
