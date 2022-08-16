using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/New Item Object", order = 1)]
public class ItemObject : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Transform prefabInstance;

    public Transform Prefab { get => prefabInstance; }
    public string ItemName { get => itemName; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ItemObject o = (ItemObject)obj;
        return itemName == o.itemName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return itemName;
    }
}
