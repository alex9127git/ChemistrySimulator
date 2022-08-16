using UnityEngine;
using static BuildingManager;

public class Building : MonoBehaviour
{
    public Transform preview;

    void Update()
    {
        CheckMoveAndDelete();
    }

    protected void CheckMoveAndDelete()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        string tag = gameObject.tag;
        int buildingSize = DetermineBuildingSize();
        if (buildingSize == 3)
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            if (x >= 1 && x < size - 1 && y >= 1 && y < size - 1)
            {
                if (buildings[x, y].tag == tag && buildings[x, y + 1].tag == tag && buildings[x, y - 1].tag == tag &&
                buildings[x - 1, y].tag == tag && buildings[x - 1, y + 1].tag == tag && buildings[x - 1, y - 1].tag == tag &&
                buildings[x + 1, y].tag == tag && buildings[x + 1, y + 1].tag == tag && buildings[x + 1, y - 1].tag == tag)
                {
                    if (Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
                    {
                        if (ModeSwitch.modeType == 1)
                        {
                            buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] =
                            buildings[x - 1, y] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] =
                            buildings[x + 1, y] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = null;
                            Instantiate(preview, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                        else if (ModeSwitch.modeType == 2)
                        {
                            buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] = buildings[x - 1, y
                                    ] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] = buildings[x + 1, y
                                    ] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = null;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
        else if (buildingSize == 2)
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            if (x >= 0 && x < size - 1 && y >= 0 && y < size - 1)
            {
                if (buildings[x, y].tag == tag && buildings[x, y + 1].tag == tag &&
                buildings[x + 1, y].tag == tag && buildings[x + 1, y + 1].tag == tag)
                {
                    if (Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
                    {
                        if (ModeSwitch.modeType == 1)
                        {
                            buildings[x, y] = buildings[x, y + 1] = buildings[x + 1, y] = buildings[x + 1, y + 1] = null;
                            Instantiate(preview, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                        else if (ModeSwitch.modeType == 2)
                        {
                            buildings[x, y] = buildings[x, y + 1] = buildings[x + 1, y] = buildings[x + 1, y + 1] = null;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
        else if (buildingSize == 1)
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                if (buildings[x, y].tag == tag)
                {
                    if (Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
                    {
                        if (ModeSwitch.modeType == 1)
                        {
                            buildings[x, y] = null;
                            Instantiate(preview, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                        else if (ModeSwitch.modeType == 2)
                        {
                            buildings[x, y] = null;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    public int DetermineBuildingSize()
    {
        int buildingSize = 0;
        switch (tag)
        {
            case "WaterExtractor":
            case "GasExtractor":
                buildingSize = 3;
                break;
            case "ReagentMixer":
                buildingSize = 2;
                break;
            case "ElectroSeparator":
                buildingSize = 1;
                break;
        }
        return buildingSize;
    }
}
