using UnityEngine;
using static GlobalVariables;

public class PlacedBuildingsScript : MonoBehaviour
{
    public Transform preview;

    void Update()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (ModeSwitcherScript.modeType == 1 && Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreviewScript.busy)
        {
            if (gameObject.name == "WaterExtractor(Clone)")
            {
                int x = ConvertWorldCoordsToListIndex(transform.position.x);
                int y = ConvertWorldCoordsToListIndex(transform.position.y);
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y] == "waterExtractor" && buildings[x, y + 1] == "waterExtractor" && buildings[x, y - 1] == "waterExtractor" &&
                    buildings[x - 1, y] == "waterExtractor" && buildings[x - 1, y + 1] == "waterExtractor" && buildings[x - 1, y - 1] == "waterExtractor" &&
                    buildings[x + 1, y] == "waterExtractor" && buildings[x + 1, y + 1] == "waterExtractor" && buildings[x + 1, y - 1] == "waterExtractor")
                    {
                        buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = buildings[x - 1, y
                            ] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] = buildings[x + 1, y
                            ] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = "";
                        Instantiate(preview, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
        }
        if (ModeSwitcherScript.modeType == 2 && Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreviewScript.busy)
        {
            if (gameObject.name == "WaterExtractor(Clone)")
            {
                int x = ConvertWorldCoordsToListIndex(transform.position.x);
                int y = ConvertWorldCoordsToListIndex(transform.position.y);
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y] == "waterExtractor" && buildings[x, y + 1] == "waterExtractor" && buildings[x, y - 1] == "waterExtractor" &&
                    buildings[x - 1, y] == "waterExtractor" && buildings[x - 1, y + 1] == "waterExtractor" && buildings[x - 1, y - 1] == "waterExtractor" &&
                    buildings[x + 1, y] == "waterExtractor" && buildings[x + 1, y + 1] == "waterExtractor" && buildings[x + 1, y - 1] == "waterExtractor")
                    {
                        buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = buildings[x - 1, y
                                    ] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] = buildings[x + 1, y
                                    ] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = "";
                        Destroy(gameObject);
                    }
                }
            }
            if (gameObject.name.Contains("Conveyor"))
            {
                int x = ConvertWorldCoordsToListIndex(transform.position.x);
                int y = ConvertWorldCoordsToListIndex(transform.position.y);
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y] == "conveyor")
                    {
                        buildings[x, y] = "";
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
