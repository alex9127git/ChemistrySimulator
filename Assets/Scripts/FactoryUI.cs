using UnityEngine;
using TMPro;
using System;

public class FactoryUI : MonoBehaviour
{
    private Factory factoryRef;
    public TMP_Dropdown input;
    public TMP_Dropdown secondaryInput;
    public TMP_Text inputCoef;
    public TMP_Text secondInputCoef;
    public TMP_Text output;
    public TMP_Text secondaryOutput;

    private ReactionManager reactionManager;

    void Start()
    {
        factoryRef = transform.parent.parent.gameObject.GetComponent<Factory>();
        // This is necessary because the parent of the FactoryUI is InternalUI panel. InternalUI panel has a factory prefab as a parent.
        // Accessing parent twice gives us factory reference which we can use later on.
        reactionManager = GameObject.Find("ReactionManager").GetComponent<ReactionManager>();
    }

    void Update()
    {
        UpdateReaction();
        UpdateText();
    }

    void UpdateText()
    {
        switch (factoryRef.gameObject.tag)
        {
            case "WaterExtractor":
                output.text = string.Format("Extracts Water.\nContains {0} Water out of 10 in the internal storage.", factoryRef.OutputItemsCount());
                break;
            case "GasExtractor":
                output.text = string.Format("Extracts Methane.\nContains {0} Methane out of 10 in the internal storage.", factoryRef.OutputItemsCount());
                break;
        }
    }

    void UpdateReaction()
    {
        ReactionObject reaction = null;
        switch (factoryRef.gameObject.tag)
        {
            case "ElectroSeparator":
                if (input.captionText.text == "H<sub>2</sub>O")
                {
                    output.text = "2 H<sub>2</sub>";
                    secondaryOutput.text = "O<sub>2</sub>";
                    inputCoef.text = "2";
                    reaction = reactionManager.FindReactionWithName("2H2O = 2H2 + O2");
                }
                else
                {
                    output.text = "---";
                    secondaryOutput.text = "---";
                    inputCoef.text = "-";
                    reaction = reactionManager.FindReactionWithName("No Reaction");
                }
                break;
            case "ReagentMixer":
                if (input.captionText.text == "CH<sub>4</sub>" && secondaryInput.captionText.text == "H<sub>2</sub>O")
                {
                    output.text = "CO";
                    secondaryOutput.text = "3 H<sub>2</sub>";
                    inputCoef.text = "1";
                    secondInputCoef.text = "1";
                    reaction = reactionManager.FindReactionWithName("CH4 + H2O = CO + 3H2");
                }
                else if (input.captionText.text == "CH<sub>4</sub>" && secondaryInput.captionText.text == "O<sub>2</sub>")
                {
                    output.text = "CO<sub>2</sub>";
                    secondaryOutput.text = "2 H<sub>2</sub>O";
                    inputCoef.text = "1";
                    secondInputCoef.text = "2";
                    reaction = reactionManager.FindReactionWithName("CH4 + 2O2 = CO2 + 2H2O");
                }
                else if (input.captionText.text == "H<sub>2</sub>O" && secondaryInput.captionText.text == "CO<sub>2</sub>")
                {
                    output.text = "H<sub>2</sub>CO<sub>3</sub>";
                    secondaryOutput.text = "---";
                    inputCoef.text = "1";
                    secondInputCoef.text = "1";
                    reaction = reactionManager.FindReactionWithName("H2O + CO2 = H2CO3");
                }
                else
                {
                    output.text = "---";
                    secondaryOutput.text = "---";
                    inputCoef.text = "-";
                    secondInputCoef.text = "-";
                    reaction = reactionManager.FindReactionWithName("No Reaction");
                }
                break;
            default:
                reaction = factoryRef.Reaction;
                break;
        }
        factoryRef.Reaction = reaction;
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
