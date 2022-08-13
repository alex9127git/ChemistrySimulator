using UnityEngine;
using TMPro;

public class FactoryUI : MonoBehaviour
{
    private Factory factoryRef;
    public TMP_Dropdown input;
    public TMP_Dropdown secondaryInput;
    public TMP_Text inputCoef;
    public TMP_Text secondInputCoef;
    public TMP_Text output;
    public TMP_Text secondaryOutput;

    void Start()
    {
        factoryRef = transform.parent.parent.gameObject.GetComponent<Factory>();
        // This is necessary because the parent of the FactoryUI is InternalUI panel. InternalUI panel has a factory prefab as a parent.
        // Accessing parent twice gives us factory reference which we can use later on.
    }

    void Update()
    {
        switch (factoryRef.gameObject.tag)
        {
            case "ElectroSeparator":
                if (input.captionText.text == "H<sub>2</sub>O")
                {
                    output.text = "2 H<sub>2</sub>";
                    secondaryOutput.text = "O<sub>2</sub>";
                    inputCoef.text = "2";
                }
                else
                {
                    output.text = "---";
                    secondaryOutput.text = "---";
                    inputCoef.text = "-";
                }
                break;
            case "ReagentMixer":
                if (input.captionText.text == "CH<sub>4</sub>" && secondaryInput.captionText.text == "H<sub>2</sub>O")
                {
                    output.text = "CO";
                    secondaryOutput.text = "3 H<sub>2</sub>";
                    inputCoef.text = "1";
                    secondInputCoef.text = "1";
                }
                else if (input.captionText.text == "CH<sub>4</sub>" && secondaryInput.captionText.text == "O<sub>2</sub>")
                {
                    output.text = "CO<sub>2</sub>";
                    secondaryOutput.text = "2 H<sub>2</sub>O";
                    inputCoef.text = "1";
                    secondInputCoef.text = "2";
                }
                else
                {
                    output.text = "---";
                    secondaryOutput.text = "---";
                    inputCoef.text = "-";
                    secondInputCoef.text = "-";
                }
                break;
        }
    }

    public void OpenFactoryUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseFactoryUI()
    {
        gameObject.SetActive(false);
    }
}
