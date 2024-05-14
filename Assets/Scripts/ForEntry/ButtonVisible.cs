using UnityEngine;
using UnityEngine.UI;

public class ButtonVisible : MonoBehaviour
{
    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;
    public Button button;

    private void Start()
    {
        button.interactable = false;
        inputField1.onValueChanged.AddListener(OnInputFieldValueChanged);
        inputField2.onValueChanged.AddListener(OnInputFieldValueChanged);
        inputField3.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string text)
    {
        if (!string.IsNullOrEmpty(inputField1.text) && !string.IsNullOrEmpty(inputField2.text) && !string.IsNullOrEmpty(inputField3.text))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
