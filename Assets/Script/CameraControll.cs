using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform rocket;   // อ้างอิงไปที่จรวด
    public Vector3 offset = new Vector3(0, 2, -5); // ตำแหน่งกล้อง (ด้านหลังจรวด)
    public float smoothSpeed = 10f; // ความเร็วในการปรับตำแหน่งกล้อง

    void LateUpdate()
    {
        if (rocket == null) return;

        // คำนวณตำแหน่งกล้องให้ตามหลังยาน
        Vector3 targetPosition = rocket.position + rocket.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // หมุนกล้องให้หันไปทางเดียวกับยาน
        transform.rotation = Quaternion.Lerp(transform.rotation, rocket.rotation, Time.deltaTime * smoothSpeed);
    }
}