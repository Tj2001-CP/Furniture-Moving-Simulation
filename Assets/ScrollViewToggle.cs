using UnityEngine;
using UnityEngine.UI;

public class ScrollViewToggle : MonoBehaviour
{
    public Button toggleButton; 
    public GameObject scrollView; 
    private bool isScrollViewVisible = false; 

    void Start()
    {
        // Ensure the scroll view is initially hidden
        scrollView.SetActive(isScrollViewVisible);

        // Add listener to the button
        toggleButton.onClick.AddListener(ToggleScrollView);
    }

    void ToggleScrollView()
    {
        isScrollViewVisible = !isScrollViewVisible; // Toggle the state
        scrollView.SetActive(isScrollViewVisible); // Set the scroll view visibility
    }
}
