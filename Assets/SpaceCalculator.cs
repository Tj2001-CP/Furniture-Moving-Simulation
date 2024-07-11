using UnityEngine;
using UnityEngine.UI;

public class SpaceCalculator : MonoBehaviour
{
    public Text leftSpaceText;
    public Text rightSpaceText;

    // Room dimensions (updated to reflect actual floor scale)
    public float roomWidth = 4.0f; // Width in units (based on floor scale)
    public float roomLength = 4.0f; // Length in units (based on floor scale)

    // Furniture dimensions (example values, adjust as needed)
    public float furnitureWidth = 2.0f; // Example width in units

    void Update()
    {
        // Calculate the space left on both sides of the furniture
        float leftSpace = transform.position.x;
        float rightSpace = roomWidth - (transform.position.x + furnitureWidth);

        // Update the UI text elements
        leftSpaceText.text = "Left Space: " + leftSpace.ToString("F2") + " units";
        rightSpaceText.text = "Right Space: " + rightSpace.ToString("F2") + " units";
    }
}
