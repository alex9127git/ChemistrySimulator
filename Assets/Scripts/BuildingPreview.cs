using UnityEngine;
using static BuildingManager;

public class BuildingPreview : MonoBehaviour
{
    public static bool busy;
    public Building prefab;

    void Update()
    {
        string tag = gameObject.tag;
        int buildingSize = DetermineBuildingSize(tag);
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v.x = (int)v.x;
        v.y = (int)v.y;
        v.x += (buildingSize % 2 == 1 ? 0 : 1) * 0.5f;
        v.y += (buildingSize % 2 == 1 ? 0 : 1) * 0.5f;
        v.z = 0;
        transform.position = v;
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
                        Building obj = Instantiate(prefab, transform.position, transform.rotation);
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
                    if (buildings[x, y] == null && buildings[x, y + 1] == null &&
                    buildings[x + 1, y] == null && buildings[x + 1, y + 1] == null)
                    {
                        Building obj = Instantiate(prefab, transform.position, transform.rotation);
                        buildings[x, y] = buildings[x, y + 1] = buildings[x + 1, y] = buildings[x + 1, y + 1] = obj;
                        Destroy(gameObject);
                        busy = false;
                    }
                }
            }
            else if (buildingSize == 1)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                if (tag == "Sorter")
                {
                    if (buildings[x, y] is Conveyor)
                    {
                        Sorter obj = (Sorter)Instantiate(prefab, transform.position, transform.rotation);
                        Destroy(buildings[x, y].gameObject);
                        buildings[x, y] = obj;
                        int rotation = (int)(transform.rotation.eulerAngles.z / 90);
                        Coordinate right = new Coordinate(x + 1, y);
                        Coordinate left = new Coordinate(x - 1, y);
                        Coordinate up = new Coordinate(x, y + 1);
                        Coordinate down = new Coordinate(x, y - 1);
                        switch (rotation)
                        {
                            case 0:
                                obj.InC = left;
                                obj.FilterOutC = right;
                                obj.RestOutC1 = up;
                                obj.RestOutC2 = down;
                                break;
                            case 1:
                                obj.InC = down;
                                obj.FilterOutC = up;
                                obj.RestOutC1 = left;
                                obj.RestOutC2 = right;
                                break;
                            case 2:
                                obj.InC = right;
                                obj.FilterOutC = left;
                                obj.RestOutC1 = down;
                                obj.RestOutC2 = up;
                                break;
                            case 3:
                                obj.InC = up;
                                obj.FilterOutC = down;
                                obj.RestOutC1 = right;
                                obj.RestOutC2 = left;
                                break;
                        }
                    }
                    Destroy(gameObject);
                    busy = false;
                }
                else if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y] == null)
                    {
                        Building obj = Instantiate(prefab, transform.position, transform.rotation);
                        buildings[x, y] = obj;
                        Destroy(gameObject);
                        busy = false;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (tag == "Sorter")
            {
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
}
