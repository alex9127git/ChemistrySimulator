using UnityEngine;
using UnityEngine.UI;

public class ModeSwitch : MonoBehaviour
{
    string[] modes = {"Spying Mode", "Moving Mode", "Deleting Mode"};
    public static int modeType = 0;
    public Text textField;

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
