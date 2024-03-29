using TMPro;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    private Building buildingRef;
    public TMP_Dropdown input;

    private ItemManager itemManager;

    void Start()
    {
        buildingRef = transform.parent.parent.gameObject.GetComponent<Building>();
        // This is necessary because the parent of the FactoryUI is InternalUI panel. InternalUI panel has a factory prefab as a parent.
        // Accessing parent twice gives us factory reference which we can use later on.
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    void Update()
    {
        UpdateInput();
    }
    
    void UpdateInput()
    {
        switch (buildingRef.gameObject.tag)
        {
            case "Sorter":
                ItemObject obj = itemManager.FindItemWithName(input.captionText.text);
                ((Sorter)buildingRef).Filter = obj;
                break;
        }
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
