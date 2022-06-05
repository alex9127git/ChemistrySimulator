using UnityEngine;
using static GlobalVariables;

public class ConveyorScript : MonoBehaviour
{
    private GameObject input, output;
    private float inputX, inputY, outputX, outputY;
    private int indexX, indexY;
    public Transform H2O;
    private bool hasItem, clogged;

    public int IndexX { get => indexX; }
    public int IndexY { get => indexY; }
    public float OutputX { get => outputX; }
    public float OutputY { get => outputY; }
    public float InputX { get => inputX; }
    public float InputY { get => inputY; }
    public GameObject Output { get => output; }
    public bool HasItem { get => hasItem; }
    public bool Clogged { get => clogged; }

    void Start()
    {
        UpdateConveyor();
    }

    void Update()
    {
        UpdateConveyor();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        if (conveyorItems[x, y] == null && input != null && input.tag == "WaterExtractor")
        {
            if (inputX < transform.position.x)
            {
                conveyorItems[x, y] = Instantiate(H2O, new Vector3(inputX + 0.1f, inputY), Quaternion.identity).gameObject;
            }
            else if (inputX > transform.position.x)
            {
                conveyorItems[x, y] = Instantiate(H2O, new Vector3(inputX - 0.1f, inputY), Quaternion.identity).gameObject;
            }
            else if (inputY < transform.position.y)
            {
                conveyorItems[x, y] = Instantiate(H2O, new Vector3(inputX, inputY + 0.1f), Quaternion.identity).gameObject;
            }
            else if (inputY > transform.position.y)
            {
                conveyorItems[x, y] = Instantiate(H2O, new Vector3(inputX, inputY - 0.1f), Quaternion.identity).gameObject;
            }
        }
        hasItem = conveyorItems[x, y] != null;
        GameObject nextConveyor = output;
        clogged = (hasItem && nextConveyor == null) || (hasItem && nextConveyor.GetComponent<ConveyorScript>().Clogged);
    }

    void UpdateConveyor()
    {
        string name = gameObject.name;
        indexX = (int)transform.position.x;
        indexY = (int)transform.position.y;
        if (name.StartsWith("Right") && indexX < size - 1)
        {
            input = buildings[indexX + 1, indexY];
            inputX = transform.position.x + 0.5f;
            inputY = transform.position.y;
        }
        if (name.StartsWith("Left") && indexX > 0)
        {
            input = buildings[indexX - 1, indexY];
            inputX = transform.position.x - 0.5f;
            inputY = transform.position.y;
        }
        if (name.StartsWith("Up") && indexY < size - 1)
        {
            input = buildings[indexX, indexY + 1];
            inputX = transform.position.x;
            inputY = transform.position.y + 0.5f;
        }
        if (name.StartsWith("Down") && indexY > 0)
        {
            input = buildings[indexX, indexY - 1];
            inputX = transform.position.x;
            inputY = transform.position.y - 0.5f;
        }
        if (name.Contains("Right") && !name.StartsWith("Right") && indexX < size - 1)
        {
            output = buildings[indexX + 1, indexY];
            outputX = transform.position.x + 0.5f;
            outputY = transform.position.y;
        }
        if (name.Contains("Left") && !name.StartsWith("Left") && indexX > 0)
        {
            output = buildings[indexX - 1, indexY];
            outputX = transform.position.x - 0.5f;
            outputY = transform.position.y;
        }
        if (name.Contains("Up") && !name.StartsWith("Up") && indexY < size - 1)
        {
            output = buildings[indexX, indexY + 1];
            outputX = transform.position.x;
            outputY = transform.position.y + 0.5f;
        }
        if (name.Contains("Down") && !name.StartsWith("Down") && indexY > 0)
        {
            output = buildings[indexX, indexY - 1];
            outputX = transform.position.x;
            outputY = transform.position.y - 0.5f;
        }
    }
}
