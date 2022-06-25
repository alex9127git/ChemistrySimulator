using System;
using UnityEngine;
using static GlobalVariables;

public class Item : MonoBehaviour
{
    private int indexX, indexY;
    private float centerX, centerY, inputX, inputY, outputX, outputY;
    private float fromX, toX, fromY, toY;
    private bool reachedCenter;
    private string direction;

    public int IndexX { get => indexX; set => indexX = value; }
    public int IndexY { get => indexY; set => indexY = value; }

    void Start()
    {
        GetNewCoordinates();
    }

    void LateUpdate()
    {
        if (direction == "left")
        {
            transform.position += new Vector3(-Time.deltaTime, 0, 0);
        }
        else if (direction == "right")
        {
            transform.position += new Vector3(Time.deltaTime, 0, 0);
        }
        else if (direction == "up")
        {
            transform.position += new Vector3(0, Time.deltaTime, 0);
        }
        else if (direction == "down")
        {
            transform.position += new Vector3(0, -Time.deltaTime, 0);
        }
        CheckReach(toX, toY);
    }

    void GetNewCoordinates()
    {
        indexX = (int)decimal.Round((decimal)transform.position.x);
        indexY = (int)decimal.Round((decimal)transform.position.y);
        GameObject conveyor = buildings[indexX, indexY];
        conveyorItems[indexX, indexY] = gameObject;
        if (conveyor != null && conveyor.tag == "Conveyor")
        {
            centerX = conveyor.transform.position.x;
            centerY = conveyor.transform.position.y;
            inputX = conveyor.GetComponent<Conveyor>().InputX;
            inputY = conveyor.GetComponent<Conveyor>().InputY;
            outputX = conveyor.GetComponent<Conveyor>().OutputX;
            outputY = conveyor.GetComponent<Conveyor>().OutputY;
            fromX = inputX;
            fromY = inputY;
            toX = centerX;
            toY = centerY;
            reachedCenter = false;
            DefineDirection(fromX, fromY, toX, toY);
        }
        else
        {
            fromX = fromY = toX = toY = 0;
            DefineDirection(fromX, fromY, toX, toY);
        }
    }

    void DefineDirection(float x1, float y1, float x2, float y2)
    {
        if (x1 > x2 && y1 == y2)
        {
            direction = "left";
        }
        else if (x1 < x2 && y1 == y2)
        {
            direction = "right";
        }
        else if (x1 == x2 && y1 < y2)
        {
            direction = "up";
        }
        else if (x1 == x2 && y1 > y2)
        {
            direction = "down";
        }
        else if (x1 == x2 && x2 == y1 && y1 == y2 && y2 == 0)
        {
            direction = "stop";
        }
    }

    void CheckReach(float x, float y)
    {
        if ((direction == "left" && transform.position.x < x) || (direction == "right" && transform.position.x > x) ||
            (direction == "up" && transform.position.y > y) || (direction == "down" && transform.position.y < y))
        {
            reachedCenter = !reachedCenter;
            if (reachedCenter)
            {
                GameObject conveyor = buildings[(int)transform.position.x, (int)transform.position.y];
                GameObject nextConveyor = conveyor.GetComponent<Conveyor>().Output;
                if (nextConveyor == null || nextConveyor.GetComponent<Conveyor>().Clogged)
                {
                    fromX = fromY = toX = toY = 0;
                    DefineDirection(fromX, fromY, toX, toY);
                }
                else
                {
                    fromX = centerX;
                    fromY = centerY;
                    toX = outputX;
                    toY = outputY;
                    DefineDirection(fromX, fromY, toX, toY);
                }
            }
            else
            {
                conveyorItems[indexX, indexY] = null;
                GetNewCoordinates();
            }
        }
    }
}
