using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    public GameObject buildingUI;
    public Transform[] buildingPreviews;

    public void OpenBuildingUI()
    {
        buildingUI.SetActive(true);
    }

    public void CloseBuildingUI()
    {
        buildingUI.SetActive(false);
    }

    public void StartBuilding(int buildingID)
    {
        CloseBuildingUI();
        Instantiate(buildingPreviews[buildingID], new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingUI.SetActive(!buildingUI.activeSelf);
        }
    }
}
