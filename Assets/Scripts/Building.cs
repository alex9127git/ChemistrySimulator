using UnityEngine;
using static Global;

public class Building : MonoBehaviour
{
    public Transform preview;

    void Update()
    {
        CheckMove();
        CheckDelete();
    }

    protected void CheckMove()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (ModeSwitch.modeType == 1 && Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
        {
            string tag = gameObject.tag;
            int buildingSize = 0;
            switch (tag)
            {
                case "WaterExtractor":
                    buildingSize = 3;
                    break;
            }
            if (buildingSize == 3)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y].tag == tag && buildings[x, y + 1].tag == tag && buildings[x, y - 1].tag == tag &&
                    buildings[x - 1, y].tag == tag && buildings[x - 1, y + 1].tag == tag && buildings[x - 1, y - 1].tag == tag &&
                    buildings[x + 1, y].tag == tag && buildings[x + 1, y + 1].tag == tag && buildings[x + 1, y - 1].tag == tag)
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
    }

    protected void CheckDelete()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (ModeSwitch.modeType == 2 && Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
        {
            string tag = gameObject.tag;
            int buildingSize = 0;
            switch (tag)
            {
                case "WaterExtractor":
                    buildingSize = 3;
                    break;
            }
            if (buildingSize == 3)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    if (buildings[x, y].tag == tag && buildings[x, y + 1].tag == tag && buildings[x, y - 1].tag == tag &&
                    buildings[x - 1, y].tag == tag && buildings[x - 1, y + 1].tag == tag && buildings[x - 1, y - 1].tag == tag &&
                    buildings[x + 1, y].tag == tag && buildings[x + 1, y + 1].tag == tag && buildings[x + 1, y - 1].tag == tag)
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
}
