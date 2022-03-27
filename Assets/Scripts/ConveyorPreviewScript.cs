using UnityEngine;
using static GlobalVariables;

public class ConveyorPreviewScript : MonoBehaviour
{
    public static bool busy;
    public Transform inputPrefab;
    public Transform outputPreview;
    public Transform upright, upleft, updown, leftup, leftright, leftdown,
        rightup, rightleft, rightdown, downup, downleft, downright;
    public Transform[,] conveyorPrefabs;

    void Start()
    {
        conveyorPrefabs = new Transform[4, 4] 
        {
            {rightleft, rightup, rightleft, rightdown}, 
            {upright, updown, upleft, updown},
            {leftright, leftup, leftright, leftdown}, 
            {downright, downup, downleft, downup}
        };
    }

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
        busy = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.name == "ConveyorInputPreview(Clone)")
            {
                Instantiate(inputPrefab, transform.position, transform.rotation);
                Instantiate(outputPreview, transform.position, Quaternion.identity);
                Destroy(gameObject);
                busy = false;
            }
            else if (gameObject.name == "ConveyorOutputPreview(Clone)")
            {
                int[,] path = WaveAlgorithm();
                InstantiateConveyorBelt(path);
                Destroy(gameObject);
                busy = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(new Vector3(0, 0, -90));
        }
    }

    private void InstantiateConveyorBelt(int[,] path)
    {
        GameObject input = GameObject.Find("ConveyorInputPlaced(Clone)");
        int inRotation = (int)(input.transform.rotation.z / 4);
        int outRotation = (int)(transform.rotation.z / 4);
        for (int i = 0; i < path.Length / 2; i++)
        {
            float x = ConvertListIndexToWorldCoords(path[i, 0]);
            float y = ConvertListIndexToWorldCoords(path[i, 1]);
            int from = 0, to = 0;
            if (i == 0)
            {
                from = (inRotation + 2) % 4;
            }
            else
            {
                if (path[i - 1, 0] < path[i, 0])
                {
                    from = 2;
                }
                else if (path[i - 1, 0] > path[i, 0])
                {
                    from = 0;
                }
                else
                {
                    if (path[i - 1, 1] < path[i, 1])
                    {
                        from = 3;
                    }
                    else if (path[i - 1, 1] > path[i, 1])
                    {
                        from = 1;
                    }
                }
            }
            if (i == (int)(path.Length / 2 - 1))
            {
                to = outRotation;
            }
            else
            {
                if (path[i + 1, 0] < path[i, 0])
                {
                    to = 2;
                }
                else if (path[i + 1, 0] > path[i, 0])
                {
                    to = 0;
                }
                else
                {
                    if (path[i + 1, 1] < path[i, 1])
                    {
                        to = 3;
                    }
                    else if (path[i + 1, 1] > path[i, 1])
                    {
                        to = 1;
                    }
                }
            }
            Instantiate(conveyorPrefabs[from, to], new Vector3(x, y), Quaternion.identity);
            buildings[path[i, 0], path[i, 1]] = "conveyor";
        }
        Destroy(input);
    }

    public int[,] WaveAlgorithm()
    {
        GameObject input = GameObject.Find("ConveyorInputPlaced(Clone)");
        int inX = ConvertWorldCoordsToListIndex(input.transform.position.x);
        int inY = ConvertWorldCoordsToListIndex(input.transform.position.y);
        int outX = ConvertWorldCoordsToListIndex(transform.position.x);
        int outY = ConvertWorldCoordsToListIndex(transform.position.y);
        int[,] map = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == inX && j == inY)
                {
                    map[inX, inY] = 0;
                }
                else if (buildings[i, j] == "")
                {
                    map[i, j] = -1;
                }
                else map[i, j] = -2;
            }
        }
        int n = 0;
        while (map[outX, outY] == -1 && Contains(map, size, -1)) {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j] == n)
                    {
                        if (i > 0 && map[i - 1, j] == -1) map[i - 1, j] = n + 1;
                        if (i < size - 1 && map[i + 1, j] == -1) map[i + 1, j] = n + 1;
                        if (j > 0 && map[i, j - 1] == -1) map[i, j - 1] = n + 1;
                        if (j < size - 1 && map[i, j + 1] == -1) map[i, j + 1] = n + 1;
                    }
                }
            }
            n += 1;
        }
        int[,] path = new int[n + 1, 2];
        path[n, 0] = outX;
        path[n, 1] = outY;
        for (int i = n; i > 0; i--)
        {
            int tileX = path[i, 0];
            int tileY = path[i, 1];
            if (tileX > 0 && map[tileX - 1, tileY] == i - 1)
            {
                path[i - 1, 0] = tileX - 1;
                path[i - 1, 1] = tileY;
                continue;
            }
            if (tileX < size - 1 && map[tileX + 1, tileY] == i - 1)
            {
                path[i - 1, 0] = tileX + 1;
                path[i - 1, 1] = tileY;
                continue;
            }
            if (tileY > 0 && map[tileX, tileY - 1] == i - 1)
            {
                path[i - 1, 0] = tileX;
                path[i - 1, 1] = tileY - 1;
                continue;
            }
            if (tileY < size - 1 && map[tileX, tileY + 1] == i - 1)
            {
                path[i - 1, 0] = tileX;
                path[i - 1, 1] = tileY + 1;
                continue;
            }
        }
        return path;
    }

    public bool Contains(int[,] m, int size, int n)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (m[i, j] == n) return true;
            }
        }
        return false;
    }
}
