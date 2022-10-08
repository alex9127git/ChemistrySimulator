using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public const int size = 100;
    public static Building[,] buildings = new Building[size, size];

    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                buildings[i, j] = null;
            }
        }
    }

    public static int DetermineBuildingSize(string tag)
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
            case "Sorter":
            case "Incinerator":
            case "ReagentOxidizer":
                buildingSize = 1;
                break;
        }
        return buildingSize;
    }
}
