using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform rocket;   
    public Vector3 offset = new Vector3(0, 2, -5); 
    public float smoothSpeed = 10f; 

    void LateUpdate()
    {
        if (rocket == null) return;

        
        Vector3 targetPosition = rocket.position + rocket.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        
        transform.rotation = Quaternion.Lerp(transform.rotation, rocket.rotation, Time.deltaTime * smoothSpeed);
    }
}