using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcherScript : MonoBehaviour
{
    string[] modes = {"Spying Mode"};
    public int modeType = 0;
    public Text textField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            modeType += 1;
            modeType %= modes.Length;
        }
        textField.text = modes[modeType];
    }
}
