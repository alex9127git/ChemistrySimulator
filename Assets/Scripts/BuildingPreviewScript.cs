using System;
using UnityEngine;
using static GlobalVariables;

public class BuildingPreviewScript : MonoBehaviour
{
    public static bool busy;
    public Transform prefab;

    void Update()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        v.x = (int)(v.x) - 0.5f;
        v.y = (int)(v.y) - 0.5f;
        v.z = 0;
        transform.position = v;
        transform.rotation = Quaternion.identity;
        busy = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.tag == "WaterExtractorPreview")
            {
                int x = ConvertWorldCoordsToListIndex(transform.position.x);
                int y = ConvertWorldCoordsToListIndex(transform.position.y);
                if (x >= 0 && x < size && y >= 0 && y < size)
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
        }
    }
}
