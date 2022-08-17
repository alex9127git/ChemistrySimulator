using UnityEngine;
using TMPro;

public class ModeSwitch : MonoBehaviour
{
    public const int spying = 0;
    public const int moving = 1;
    public const int deleting = 2;

    string[] modes = {"Spying Mode", "Moving Mode", "Deleting Mode"};
    public static int modeType = 0;
    public TMP_Text textField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !BuildingPreview.busy)
        {
            modeType += 1;
            modeType %= modes.Length;
        }
        textField.text = modes[modeType];
    }
}
