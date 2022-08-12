using UnityEngine;
using TMPro;

public class ModeSwitch : MonoBehaviour
{
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
