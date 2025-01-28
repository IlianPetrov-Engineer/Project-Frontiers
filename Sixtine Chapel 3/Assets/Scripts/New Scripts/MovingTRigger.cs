using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTRigger : MonoBehaviour
{
    public float smoothTime;
    public float smoothSpeed;

    public void Go(Transform targetPosition)
    {
        Vector3 pos = targetPosition.position;

        float newPositionX = Mathf.SmoothDamp(transform.position.x, pos.x, ref smoothSpeed, smoothTime);
        float newPositionY = Mathf.SmoothDamp(transform.position.y, pos.y, ref smoothSpeed, smoothTime);
        float newPositionZ = Mathf.SmoothDamp(transform.position.z, pos.z, ref smoothSpeed, smoothTime);

        transform.position = new Vector3(newPositionX, newPositionY, newPositionZ); 
    }
}
