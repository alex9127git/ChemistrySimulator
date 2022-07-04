using UnityEngine;

/*
 * Items ID used:
 * 0 - H2O
 * 1 - H2
 * 2 - O2
 * 3 - CH4
 * 4 - CO
 */
public class Global : MonoBehaviour
{
    public const int size = 100;
    public static GameObject[,] buildings = new GameObject[size, size];
    public Transform[] items;

    public static Transform[] itemsList;

    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                buildings[i, j] = null;
            }
        }
        itemsList = items;
    }
}
