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
            if (gameObject.tag == "WaterExtractor")
            {
                int x = (int) transform.position.x;
                int y = (int) transform.position.y;
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y].tag == "WaterExtractor" && buildings[x, y + 1].tag == "WaterExtractor" && buildings[x, y - 1].tag == "WaterExtractor" &&
                    buildings[x - 1, y].tag == "WaterExtractor" && buildings[x - 1, y + 1].tag == "WaterExtractor" && buildings[x - 1, y - 1].tag == "WaterExtractor" &&
                    buildings[x + 1, y].tag == "WaterExtractor" && buildings[x + 1, y + 1].tag == "WaterExtractor" && buildings[x + 1, y - 1].tag == "WaterExtractor")
                    {
                        buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = 
                            buildings[x - 1, y] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] = 
                            buildings[x + 1, y] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = null;
                        Instantiate(preview, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
        }
        if (ModeSwitcherScript.modeType == 2 && Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreviewScript.busy)
        {
            if (gameObject.tag == "WaterExtractor")
            {
                int x = (int) transform.position.x;
                int y = (int) transform.position.y;
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y].tag == "WaterExtractor" && buildings[x, y + 1].tag == "WaterExtractor" && buildings[x, y - 1].tag == "WaterExtractor" &&
                    buildings[x - 1, y].tag == "WaterExtractor" && buildings[x - 1, y + 1].tag == "WaterExtractor" && buildings[x - 1, y - 1].tag == "WaterExtractor" &&
                    buildings[x + 1, y].tag == "WaterExtractor" && buildings[x + 1, y + 1].tag == "WaterExtractor" && buildings[x + 1, y - 1].tag == "WaterExtractor")
                    {
                        buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = buildings[x - 1, y
                                    ] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] = buildings[x + 1, y
                                    ] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = null;
                        Destroy(gameObject);
                    }
                }
            }
            if (gameObject.tag == "Conveyor")
            {
                int x = (int) transform.position.x;
                int y = (int) transform.position.y;
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y].tag == "Conveyor")
                    {
                        buildings[x, y] = null;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
