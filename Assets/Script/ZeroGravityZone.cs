using UnityEngine;

public class ZeroGravityZone : MonoBehaviour
{
    public float explosionForce = 10f; // แรงระเบิด
    public Vector3 explosionDirection = Vector3.up; // ทิศทางของแรงระเบิด
    public float explosionRadius = 5f; // รัศมีของแรงระเบิด
    public string playerTag = "Player"; // Tag ของ Player

    private void OnEnable()
    {
        ApplyExplosionForce();
    }

    public void ApplyExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            // เช็คว่าไม่ใช่ Player
            if (col.CompareTag(playerTag))
                continue;

            if (col.attachedRigidbody != null)
            {
                Rigidbody rb = col.attachedRigidbody;

                // ปิดแรงโน้มถ่วง
                rb.useGravity = false;
                rb.angularDamping = 0;

                // เพิ่มแรงระเบิด
                rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
            }
        }
    }
    public void ResetGravity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            // เช็คว่าไม่ใช่ Player
            if (col.CompareTag(playerTag))
                continue;

            if (col.attachedRigidbody != null)
            {
                Rigidbody rb = col.attachedRigidbody;

                // เปิดแรงโน้มถ่วงกลับมา
                rb.useGravity = true;
                rb.angularDamping = 0.05f; // ตั้งค่ากลับเป็นค่าเริ่มต้น (แก้ตามต้องการ)

                // หยุดการเคลื่อนที่
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
