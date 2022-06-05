using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public const int size = 100;
    public static GameObject[,] buildings = new GameObject[size, size];
    public static GameObject[,] conveyorItems = new GameObject[size, size];

    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                buildings[i, j] = null;
                conveyorItems[i, j] = null;
            }
        }
    }
}
