using UnityEngine;
using static BuildingManager;

public class Building : MonoBehaviour
{
    public Transform preview;
    protected Coordinate coordinate;

    public Coordinate C { get => coordinate; set => coordinate = value; }

    void Update()
    {
        CheckMoveAndDelete();
    }

    protected virtual void CheckMoveAndDelete()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        string tag = gameObject.tag;
        int buildingSize = DetermineBuildingSize(tag);
        if (buildingSize == 3)
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            if (x >= 1 && x < size - 1 && y >= 1 && y < size - 1)
            {
                if (buildings[x, y].gameObject.tag == tag && buildings[x, y + 1].gameObject.tag == tag && buildings[x, y - 1].gameObject.tag == tag &&
                buildings[x - 1, y].gameObject.tag == tag && buildings[x - 1, y + 1].gameObject.tag == tag && buildings[x - 1, y - 1].gameObject.tag == tag &&
                buildings[x + 1, y].gameObject.tag == tag && buildings[x + 1, y + 1].gameObject.tag == tag && buildings[x + 1, y - 1].gameObject.tag == tag)
                {
                    if (Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
                    {
                        if (ModeSwitch.modeType == ModeSwitch.moving)
                        {
                            buildings[x, y] = buildings[x, y + 1] = buildings[x, y - 1] =
                            buildings[x - 1, y] = buildings[x - 1, y + 1] = buildings[x - 1, y - 1] =
                            buildings[x + 1, y] = buildings[x + 1, y + 1] = buildings[x + 1, y - 1] = null;
                            Instantiate(preview, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                        else if (ModeSwitch.modeType == ModeSwitch.deleting)
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
                if (buildings[x, y].gameObject.tag == tag && buildings[x, y + 1].gameObject.tag == tag &&
                buildings[x + 1, y].gameObject.tag == tag && buildings[x + 1, y + 1].gameObject.tag == tag)
                {
                    if (Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
                    {
                        if (ModeSwitch.modeType == ModeSwitch.moving)
                        {
                            buildings[x, y] = buildings[x, y + 1] = buildings[x + 1, y] = buildings[x + 1, y + 1] = null;
                            Instantiate(preview, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                        else if (ModeSwitch.modeType == ModeSwitch.deleting)
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
                if (buildings[x, y].gameObject.tag == tag)
                {
                    if (Input.GetMouseButtonDown(0) && dist < 1 && !BuildingPreview.busy)
                    {
                        if (ModeSwitch.modeType == ModeSwitch.moving)
                        {
                            buildings[x, y] = null;
                            Instantiate(preview, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                        else if (ModeSwitch.modeType == ModeSwitch.deleting)
                        {
                            buildings[x, y] = null;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
}
