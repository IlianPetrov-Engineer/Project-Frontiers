using UnityEngine;

public class RotationTrigger : MonoBehaviour
{
    private Quaternion angularVelocity = Quaternion.identity;
    [Tooltip("Smooths out the rotation of the object")]
    public float smoothTime = 0.3f; // Smooth damp time

    private Quaternion targetRotation; // Target rotation
    private bool isRotating = false; // Check if rotation is ongoing

    [Tooltip("The target degrees for the rotation.")]
    public Vector3 targetEulerAngles; // Set the target rotation angles in the Inspector

    public void Rotate()
    {
        // Calculate the target rotation based on the desired Euler angles
        targetRotation = Quaternion.Euler(targetEulerAngles);
        isRotating = true;
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation, targetRotation, ref angularVelocity, smoothTime);

            // Stop rotating when close enough to the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }
}
