using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonNameText; // Reference to the TextMeshProUGUI element for displaying the button name
    public string buttonName; 

    private Coroutine displayCoroutine; 

    private void Start()
    {
        buttonNameText.gameObject.SetActive(false); 

        // Add event listeners to the Button component
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnDestroy()
    {
        // Remove event listeners when the script is destroyed
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If there is already a running coroutine, stop it
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        buttonNameText.text = buttonName;
        buttonNameText.gameObject.SetActive(true); 

        // Start a new coroutine to hide the text after 10 seconds
        displayCoroutine = StartCoroutine(HideTextAfterDelay(10.0f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If the pointer exits, stop the coroutine and hide the text immediately
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
        buttonNameText.gameObject.SetActive(false); // Hide the text when not hovering
    }

    private void OnButtonClick()
    {
        
        Debug.Log("Button clicked: " + buttonName);
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonNameText.gameObject.SetActive(false); 
    }
}
