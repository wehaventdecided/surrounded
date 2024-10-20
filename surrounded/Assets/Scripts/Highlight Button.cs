using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextMeshProHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;  // Reference to the TMP text component
    public Color normalColor = Color.white;  // Default text color
    public Color hoverColor = Color.yellow;  // Highlight color

    void Start()
    {
        if (buttonText != null)
            buttonText.color = normalColor;  // Set initial color
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = hoverColor;  // Change to highlight color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = normalColor;  // Revert to normal color
    }
}
