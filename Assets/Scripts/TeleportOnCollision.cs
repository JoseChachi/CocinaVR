using UnityEngine;

public class DetectAndTeleport : MonoBehaviour
{
    // Define the new position for the camera (Object A) after intersection
    [SerializeField] public Vector3 newPosition;

    // Reference to the camera (Object A)
    [SerializeField] public GameObject playerCamera;

    private void Update()
    {
        // Get the bounds of the current object using its Transform component
        Bounds boundsB = GetBounds(transform);

        // Get the bounds of the camera object using its Transform component
        Bounds boundsA = GetBounds(playerCamera.transform);

        // Check if the bounds of the two objects intersect
        if (boundsB.Intersects(boundsA))
        {
            // Log for debugging to confirm the intersection detection
            Debug.Log("Intersection detected, teleporting the camera.");

            // Change the position of the camera to the new position
            playerCamera.transform.position = newPosition;

            // Log for debugging to confirm the position change
            Debug.Log("Camera teleported to: " + newPosition);
        }
    }

    // Calculate the bounds of an object based on its Transform component
    private Bounds GetBounds(Transform t)
    {
        // Default size in case the object doesn't have a Renderer component
        Vector3 size = t.localScale;

        // If the object has a Renderer component, use its bounds size
        Renderer renderer = t.GetComponent<Renderer>();
        if (renderer != null)
        {
            size = Vector3.Scale(t.localScale, renderer.bounds.size);
        }

        // Create and return the bounds
        return new Bounds(t.position, size);
    }
}
