using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/New Item Object", order = 1)]
public class ItemObject : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite texture;

    public Sprite Texture { get => texture; }
    public string ItemName { get => itemName; }

    public override string ToString()
    {
        return itemName;
    }
}
