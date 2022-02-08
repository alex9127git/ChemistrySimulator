using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcherScript : MonoBehaviour
{
    string[] modes = {"Spying Mode", "Moving Mode", "Deleting Mode"};
    public static int modeType = 0;
    public Text textField;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !MouseFollowScript.busy)
        {
            modeType += 1;
            modeType %= modes.Length;
        }
        textField.text = modes[modeType];
    }
}
