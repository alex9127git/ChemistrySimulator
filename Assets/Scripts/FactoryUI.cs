using UnityEngine;
using UnityEngine.UI;

public class FactoryUI : MonoBehaviour
{
    public Text title;

    public void OpenFactoryUI(Factory factory)
    {
        gameObject.SetActive(true);
        title.text = factory.gameObject.tag;
    }

    public void CloseFactoryUI()
    {
        gameObject.SetActive(false);
    }
}
