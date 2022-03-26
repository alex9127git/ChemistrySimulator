using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public const int size = 100;
    public static string[,] buildings = new string[size, size];

    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                buildings[i, j] = "";
            }
        }
    }
}
