using UnityEngine;
using static Global;

public class BuildingPreview : MonoBehaviour
{
    public static bool busy;
    public Transform prefab;

    void Update()
    {
        string tag = gameObject.tag;
        int buildingSize = 0;
        switch (tag)
        {
            case "WaterExtractorPreview":
            case "GasExtractorPreview":
                buildingSize = 3;
                break;
            case "ReagentMixerPreview":
                buildingSize = 2;
                break;
            case "ElectroSeparatorPreview":
                buildingSize = 1;
                break;
        }
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v.x = (int)v.x;
        v.y = (int)v.y;
        v.z = 0;
        transform.position = v;
        transform.rotation = Quaternion.identity;
        busy = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (buildingSize == 3)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                if (x >= 1 && x < size - 1 && y >= 1 && y < size - 1)
                {
                    if (buildings[x, y] == null && buildings[x, y + 1] == null && buildings[x, y - 1] == null &&
                    buildings[x - 1, y] == null && buildings[x - 1, y + 1] == null && buildings[x - 1, y - 1] == null &&
                    buildings[x + 1, y] == null && buildings[x + 1, y + 1] == null && buildings[x + 1, y - 1] == null)
                    {
                        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity).gameObject;
                        buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = buildings[x - 1, y
                            ] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] = buildings[x + 1, y
                            ] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = obj;
                        Destroy(gameObject);
                        busy = false;
                    }
                }
            }
            else if (buildingSize == 2)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                if (x >= 1 && x < size && y >= 1 && y < size)
                {
                    if (buildings[x, y] == null && buildings[x, y - 1] == null &&
                    buildings[x - 1, y] == null && buildings[x - 1, y - 1] == null)
                    {
                        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity).gameObject;
                        buildings[x, y] = buildings[x, y - 1] = buildings[x - 1, y] = buildings[x - 1, y - 1] = obj;
                        Destroy(gameObject);
                        busy = false;
                    }
                }
            }
            else if (buildingSize == 1)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y] == null)
                    {
                        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity).gameObject;
                        buildings[x, y] = obj;
                        Destroy(gameObject);
                        busy = false;
                    }
                }
            }
        }
    }
}
