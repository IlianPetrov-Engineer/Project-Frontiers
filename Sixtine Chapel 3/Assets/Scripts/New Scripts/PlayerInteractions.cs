using UnityEngine;

/// <summary>
/// Handles player interactions with objects in the scene by raycasting from the center of the camera.
/// </summary>
public class PlayerInteractions : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("The maximum distance within which the player can interact with objects.")]
    public float interactionDistance;

    [Tooltip("Layer mask to determine which objects can be interacted with.")]
    public LayerMask layerMask;

    /// <summary>
    /// Stores the time when the right mouse button was first pressed (used to check hold duration).
    /// </summary>
    private float? _holdingStartTime;

    /// <summary>
    /// Reference to the main camera.
    /// </summary>
    private Camera _camera;

    private void Start()
    {
        // Retrieve the main camera at the start.
        _camera = Camera.main;
    }

    private void Update()
    {
        // Perform a raycast each frame to detect any interactable object in front of the camera.
        Raycasting();
    }

    /// <summary>
    /// Casts a ray from the center of the viewport and checks for an Interactable component on hit.
    /// </summary>
    private void Raycasting()
    {
        // Create a ray from the center of the camera's viewport.
        var centerRay = _camera.ViewportPointToRay(Vector3.one * 0.5f);

        // If the ray hits something within the specified interaction distance, on the given layer...
        if (Physics.Raycast(centerRay, out var hitInfo, interactionDistance, layerMask))
        {
            // Attempt to get an Interactable component from the hit collider.
            if (hitInfo.collider.TryGetComponent<Interactable>(out var interactableObj))
            {
                // If found, handle its hover/interaction logic.
                Hovering(interactableObj);
            }
        }
    }

    /// <summary>
    /// Handles the hover behavior, activation, and deactivation of the given Interactable object.
    /// </summary>
    /// <param name="interactableObj">The Interactable object that was detected by the raycast.</param>
    private void Hovering(Interactable interactableObj)
    {
        // Trigger the hover feedback (e.g., highlight, prompt).
        interactableObj.Hover();

        // Check if the right mouse button was just pressed to record the start of a hold.
        if (Input.GetMouseButtonDown(1))
        {
            _holdingStartTime = Time.time;
        }

        // If we have a recorded hold start time and the right button is still down...
        if (_holdingStartTime != null && Input.GetMouseButton(1))
        {
            // Determine how long we've been holding the button.
            var holdDuration = Time.time - _holdingStartTime.Value;

            // Get the required hold duration (activate or deactivate).
            var requiredDuration = interactableObj.active
                ? interactableObj.deactivateHoldingDuration
                : interactableObj.activateHoldingDuration;

            // Check if the hold duration is met or the required duration is zero.
            var holdingDurationMet = (holdDuration >= requiredDuration) || (requiredDuration == 0);

            if (!holdingDurationMet) return;
            // If the interactable is already active and requires repeated presses to deactivate...
            if (interactableObj.active && interactableObj.deactivationMode is DeactivationMode.ToggleMode)
            {
                interactableObj.Deactivate();
            }
            else
            {
                // Otherwise, activate it (or reactivate).
                interactableObj.Activate();
            }

            // Reset the hold start time after the action.
            _holdingStartTime = null;
        }
        // If the object is active, set to deactivate on button release, and the button is now released...
        else if (interactableObj.active
                 && (interactableObj.deactivationMode is DeactivationMode.HoldingMode)
                 && !Input.GetMouseButton(1))
        {
            // Deactivate the object upon releasing the button.
            interactableObj.Deactivate();
        }
        else
        {
            // If none of the above conditions match, reset the hold start time.
            _holdingStartTime = null;
        }
    }

    /// <summary>
    /// Draws debug gizmos in the Scene View to visualize the interaction ray and distance.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_camera == null) return;
        // Draw a green ray from the camera's center point to indicate the interaction direction.
        Gizmos.color = Color.cyan;
        var ray = _camera.ViewportPointToRay(Vector3.one * 0.5f);
        Gizmos.DrawRay(ray.origin, ray.direction * interactionDistance);

        // Draw a yellow wire sphere at the target distance.
        Gizmos.color = Color.red;
        var spherePosition = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, interactionDistance));
        Gizmos.DrawWireSphere(spherePosition, 0.1f);
    }
}