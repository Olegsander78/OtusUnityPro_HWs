using TMPro;
using UnityEngine;


public sealed class StackView : MonoBehaviour
{
    [Space]
    [SerializeField]
    private GameObject stackContainer;

    [SerializeField]
    private TextMeshProUGUI stackText;

    public void SetVisible(bool isVisible)
    {
        this.stackContainer.SetActive(isVisible);
    }

    public void SetAmount(int count, int max)
    {
        this.stackText.text = $"{count}/{max}";
    }
}