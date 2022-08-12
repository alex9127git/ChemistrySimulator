using UnityEngine;
using TMPro;

public class FactoryUI : MonoBehaviour
{
    private Factory factoryRef;
    public TMP_Dropdown input;
    public TMP_Dropdown secondaryInput;
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
                if (input.captionText.text == "H2O")
                {
                    output.text = "2H2";
                    secondaryOutput.text = "O2";
                }
                else
                {
                    output.text = "---";
                    secondaryOutput.text = "---";
                }
                break;
            case "ReagentMixer":
                if (input.captionText.text == "CH4" && secondaryInput.captionText.text == "H2O")
                {
                    output.text = "CO";
                    secondaryOutput.text = "3H2";
                }
                else
                {
                    output.text = "---";
                    secondaryOutput.text = "---";
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
