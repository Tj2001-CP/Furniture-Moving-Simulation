using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset; 
    private Transform target; 
    private Vector3 initialPosition; 
    private Quaternion initialRotation; 
    public float followSpeed = 5.0f; 

    void Start()
    {
        // Store the initial position and rotation of the camera
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Smoothly transition the camera's position to follow the target with the specified offset
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

           
            transform.LookAt(target);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetCamera()
    {
        // Reset the camera to its initial position and rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        target = null; // Stop following any object
    }
}
