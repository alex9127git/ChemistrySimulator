using UnityEngine;
using static BuildingManager;

public class ConveyorPreview : MonoBehaviour
{
    public static bool busy;
    public Transform inputPrefab;
    public Transform outputPreview;
    public Conveyor upright, upleft, updown, leftup, leftright, leftdown,
        rightup, rightleft, rightdown, downup, downleft, downright;
    public Conveyor[,] conveyorPrefabs;

    void Start()
    {
        conveyorPrefabs = new Conveyor[4, 4]
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
        v.x = (int)v.x;
        v.y = (int)v.y;
        v.z = 0;
        transform.position = v;
        busy = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.tag == "ConveyorInputPreview")
            {
                Instantiate(inputPrefab, transform.position, transform.rotation);
                Instantiate(outputPreview, transform.position, Quaternion.identity);
                Destroy(gameObject);
                busy = false;
            }
            else if (gameObject.tag == "ConveyorOutputPreview")
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
        GameObject input = GameObject.FindWithTag("ConveyorInputPreview");
        Conveyor previous = null;
        int inRotation = (int)(input.transform.rotation.eulerAngles.z / 90);
        int outRotation = (int)(transform.rotation.eulerAngles.z / 90);
        for (int i = 0; i < path.Length / 2; i++) // I am dividing by 2 because path.Length would be 2 times larger than the number of conveyor segments in the path
        {
            int x = path[i, 0];
            int y = path[i, 1];
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
            if (i == path.Length / 2 - 1)
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
            Conveyor conv = Instantiate(conveyorPrefabs[from, to], new Vector3(x, y), Quaternion.identity);
            buildings[path[i, 0], path[i, 1]] = conv.gameObject;
            int inX = 0, inY = 0;
            switch (from)
            {
                case 0: inX = x + 1; inY = y; break;
                case 1: inX = x; inY = y + 1; break;
                case 2: inX = x - 1; inY = y; break;
                case 3: inX = x; inY = y - 1; break;
            }
            int outX = 0, outY = 0;
            switch (to)
            {
                case 0: outX = x + 1; outY = y; break;
                case 1: outX = x; outY = y + 1; break;
                case 2: outX = x - 1; outY = y; break;
                case 3: outX = x; outY = y - 1; break;
            }
            conv.InX = inX;
            conv.InY = inY;
            conv.OutX = outX;
            conv.OutY = outY;
            if (previous != null)
            {
                conv.Previous = previous;
                previous.Next = conv;
            }
            else
            {
                
                GameObject inObj = null;
                switch (from)
                {
                    case 0:
                        if (path[i, 0] > 0) inObj = buildings[path[i, 0] + 1, path[i, 1]];
                        inX = x + 1;
                        inY = y;
                        break;
                    case 1:
                        if (path[i, 1] > 0) inObj = buildings[path[i, 0], path[i, 1] + 1];
                        inX = x;
                        inY = y + 1;
                        break;
                    case 2:
                        if (path[i, 0] < size) inObj = buildings[path[i, 0] - 1, path[i, 1]];
                        inX = x - 1;
                        inY = y;
                        break;
                    case 3:
                        if (path[i, 1] < size) inObj = buildings[path[i, 0], path[i, 1] - 1];
                        inX = x;
                        inY = y - 1;
                        break;
                }
                if (inObj != null)
                {
                    if (inObj.GetComponent<Building>() is Factory)
                    {
                        conv.Previous = inObj.GetComponent<Building>();
                        inObj.GetComponent<Factory>().Outputs.Add(conv);
                    }
                    else if (inObj.GetComponent<Building>() is Conveyor)
                    {
                        conv.Previous = inObj.GetComponent<Building>();
                        inObj.GetComponent<Conveyor>().Next = conv;
                    }
                }
            }
            if (i == path.Length / 2 - 1)
            {
                GameObject outObj = null;
                switch (to)
                {
                    case 0:
                        if (path[i, 0] > 0) outObj = buildings[path[i, 0] + 1, path[i, 1]];
                        outX = x + 1;
                        outY = y;
                        break;
                    case 1:
                        if (path[i, 1] > 0) outObj = buildings[path[i, 0], path[i, 1] + 1];
                        outX = x;
                        outY = y + 1;
                        break;
                    case 2:
                        if (path[i, 0] < size) outObj = buildings[path[i, 0] - 1, path[i, 1]];
                        outX = x - 1;
                        outY = y;
                        break;
                    case 3:
                        if (path[i, 1] < size) outObj = buildings[path[i, 0], path[i, 1] - 1];
                        outX = x;
                        outY = y - 1;
                        break;
                }
                if (outObj != null)
                {
                    if (outObj.GetComponent<MonoBehaviour>() is Factory)
                    {
                        conv.Next = outObj.GetComponent<Building>();
                        outObj.GetComponent<Factory>().Inputs.Add(conv);
                    }
                    else if (outObj.GetComponent<MonoBehaviour>() is Conveyor)
                    {
                        conv.Previous = outObj.GetComponent<Building>();
                        outObj.GetComponent<Conveyor>().Next = conv;
                    }
                }
            }
            previous = conv;
        }
        Destroy(input);
    }

    public int[,] WaveAlgorithm()
    {
        GameObject input = GameObject.FindWithTag("ConveyorInputPreview");
        int inX = (int)input.transform.position.x;
        int inY = (int)input.transform.position.y;
        int outX = (int)transform.position.x;
        int outY = (int)transform.position.y;
        int[,] map = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == inX && j == inY)
                {
                    map[inX, inY] = 0;
                }
                else if (buildings[i, j] == null)
                {
                    map[i, j] = -1;
                }
                else map[i, j] = -2;
            }
        }
        int n = 0;
        while (map[outX, outY] == -1 && Contains(map, size, -1))
        {
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
