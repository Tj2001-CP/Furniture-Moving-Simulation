using UnityEngine;

public class DragFurniture : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private Vector3 initialPosition;
    private float liftAmount = 0.5f;

    public float roomBoundary = 18.0f;
    public float furnitureWidth = 2.0f;
    public float dragSpeed = 0.1f; // Speed factor to control drag speed

    private bool isDragging = false;

    private UIManager uiManager;
    private CameraFollow cameraFollow;

    void Start()
    {
        initialPosition = transform.position;

        // Find the UIManager in the scene
        uiManager = FindObjectOfType<UIManager>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;

        
        Vector3 liftedPosition = transform.position;
        liftedPosition.y += liftAmount;
        transform.position = liftedPosition;

        
        uiManager.SelectObject(gameObject);

        
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(transform);
        }
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 newPos = GetMouseWorldPos() + offset;
        newPos.y = transform.position.y; // Keep the Y position constant while dragging
        newPos.x = Mathf.Clamp(newPos.x, -roomBoundary + furnitureWidth / 2, roomBoundary - furnitureWidth / 2); // Ensure the furniture stays within room bounds
        newPos.z = Mathf.Clamp(newPos.z, -roomBoundary + furnitureWidth / 2, roomBoundary - furnitureWidth / 2); // Ensure the furniture stays within room bounds

        if (CanMoveTo(newPos))
        {
            
            transform.position = Vector3.Lerp(transform.position, newPos, dragSpeed);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        
        Vector3 originalPosition = transform.position;
        originalPosition.y = initialPosition.y;
        transform.position = originalPosition;

        // Notify the camera to stop following this object
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(null);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePoint);
        worldPoint.y = initialPosition.y; 
        return worldPoint;
    }

    private bool CanMoveTo(Vector3 position)
    {
        Vector3 halfExtents = transform.localScale / 2;
        Collider[] hits = Physics.OverlapBox(position, halfExtents, Quaternion.identity, LayerMask.GetMask("Furniture"));

        // Check if there are any colliders in the hit array that are not the current object
        foreach (Collider hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                return false;
            }
        }

        return true;
    }
}
