using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    public GameObject buildingUI;
    public Transform[] previews;

    public void OpenBuildingUI()
    {
        buildingUI.SetActive(true);
    }

    public void CloseBuildingUI()
    {
        buildingUI.SetActive(false);
    }

    public void StartBuilding(string building)
    {
        CloseBuildingUI();
        int previewID = 0;
        switch (building)
        {
            case "WaterExtractor":
                previewID = 0;
                break;
            case "Conveyor":
                previewID = 1;
                break;
            case "ElectroSeparator":
                previewID = 2;
                break;
            case "GasExtractor":
                previewID = 3;
                break;
            case "ReagentMixer":
                previewID = 4;
                break;
            case "Sorter":
                previewID = 5;
                break;
            case "Incinerator":
                previewID = 6;
                break;
        }
        Instantiate(previews[previewID], new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingUI.SetActive(!buildingUI.activeSelf);
        }
    }
}
