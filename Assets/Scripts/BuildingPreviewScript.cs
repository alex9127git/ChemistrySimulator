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
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        v.x = (int)(v.x) - 0.5f;
        v.y = (int)(v.y) - 0.5f;
        v.z = 0;
        transform.position = v;
        transform.rotation = Quaternion.identity;
        busy = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.name == "WaterExtractorPreview(Clone)")
            {
                int x = (int)(transform.position.x + size / 2 - 0.5f);
                int y = (int)(transform.position.y + size / 2 - 0.5f);
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y] == "" && buildings[x, y + 1] == "" && buildings[x, y - 1] == "" &&
                    buildings[x - 1, y] == "" && buildings[x - 1, y + 1] == "" && buildings[x - 1, y + 1] == "" &&
                    buildings[x + 1, y] == "" && buildings[x + 1, y + 1] == "" && buildings[x + 1, y + 1] == "")
                    {
                        Instantiate(prefab, transform.position, Quaternion.identity);
                        buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = buildings[x - 1, y
                            ] = buildings[x - 1, y + 1] = buildings[x - 1, y + 1] = buildings[x + 1, y
                            ] = buildings[x + 1, y + 1] = buildings[x + 1, y + 1] = "waterExtractor";
                        Destroy(gameObject);
                        busy = false;
                    }
                }
            }
        }
    }
}
