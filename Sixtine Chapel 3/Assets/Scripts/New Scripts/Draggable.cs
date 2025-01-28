using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Camera _camera;
    private Vector3 velocity = Vector3.zero;
    private bool dragging = false;

    public float moveDuration = 1f; // Time to reach the target position
    public float distanceFromCamera = 5f; // Distance in front of the camera

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (dragging)
        {
            MoveObject();
        }
    }

    public void StartDragging() 
    { 
        dragging = true; 
    }

    public void StopDragging() 
    { 
        dragging = false; 
    }

    private void MoveObject()
    {
        Vector3 targetPosition = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, moveDuration);
    }
}
