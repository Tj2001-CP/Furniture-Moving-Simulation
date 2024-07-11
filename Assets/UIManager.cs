using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Button changeColorButton;
    public Button rotateButton;
    public Button resetCameraButton; // New button to reset camera position
    public TMP_Text spaceLeftText;
    public TMP_Text spaceRightText;
    public TMP_Text spaceFromPositiveZText;

    private GameObject selectedObject;
    private float rotationAmount = 45.0f;
    private bool isRotating = false;

    private DragFurniture dragFurniture;
    private CameraFollow cameraFollow;

    void Start()
    {
        changeColorButton.onClick.AddListener(ChangeColor);
        rotateButton.onClick.AddListener(ToggleRotation);
        resetCameraButton.onClick.AddListener(ResetCameraPosition); 

       
        dragFurniture = FindObjectOfType<DragFurniture>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        
        StartCoroutine(RotateContinuously());
    }

    void Update()
    {
        if (selectedObject != null && dragFurniture != null)
        {
            float roomBoundary = dragFurniture.roomBoundary;

            // Calculate space left and right, and clamp to a minimum of 0
            float spaceLeft = Mathf.Max(0, selectedObject.transform.position.x - (-roomBoundary + selectedObject.transform.localScale.x / 2));
            float spaceRight = Mathf.Max(0, roomBoundary - (selectedObject.transform.position.x + selectedObject.transform.localScale.x / 2));
            float spaceFromPositiveZ = Mathf.Max(0, roomBoundary - (selectedObject.transform.position.z + selectedObject.transform.localScale.z / 2));

            // Update the text
            spaceLeftText.text = "Space Left: " + spaceLeft.ToString("F2") + " units";
            spaceRightText.text = "Space Right: " + spaceRight.ToString("F2") + " units";
            spaceFromPositiveZText.text = "Space Top: " + spaceFromPositiveZ.ToString("F2") + " units";
        }
    }

    public void SelectObject(GameObject obj)
    {
        selectedObject = obj;
        // Update the camera's target to the selected object
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(selectedObject.transform);
        }
    }

    void ChangeColor()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    void ToggleRotation()
    {
        isRotating = !isRotating; // Toggle the rotation state
    }

    IEnumerator RotateContinuously()
    {
        while (true)
        {
            if (isRotating && selectedObject != null)
            {
                selectedObject.transform.Rotate(Vector3.up, -rotationAmount * Time.deltaTime);
            }
            yield return null;
        }
    }

    void ResetCameraPosition()
    {
        // Reset the camera to its initial position and rotation
        if (cameraFollow != null)
        {
            cameraFollow.ResetCamera();
        }
    }
}
