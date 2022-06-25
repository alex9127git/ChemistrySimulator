using UnityEngine;

public class ButtonsOnClick : MonoBehaviour
{
    public GameObject buildingUI;
    public Transform waterExtractorPrefab;
    public Transform waterExtractorPreview;
    public Transform conveyorInputPreview;
    public Transform conveyorOutputPreview;

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
        switch (building)
        {
            case "WaterExtractor":
                Instantiate(waterExtractorPreview, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                break;
            case "Conveyor":
                Instantiate(conveyorInputPreview, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingUI.SetActive(!buildingUI.activeSelf);
        }
    }
}
