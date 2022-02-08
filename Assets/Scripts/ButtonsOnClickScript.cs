using UnityEngine;

public class ButtonsOnClickScript : MonoBehaviour
{
    public GameObject buildingUI;
    public Transform waterExtractorPrefab;

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
                Instantiate(waterExtractorPrefab, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                break;
        }
    }
}
