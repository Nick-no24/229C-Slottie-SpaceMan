using UnityEngine;

public class MetalBarrel : MonoBehaviour
{
    public float minImpactForce = 5f; // กำหนดแรงขั้นต่ำที่กระจกจะเริ่มเสียหาย
    public float damageMultiplier = 2f; // ตัวคูณความเสียหายตามแรงชน

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Glass")) // ตรวจสอบว่าชนกับกระจกไหม
        {
            // คำนวณแรงกระแทกจากความเร็วของถัง
            float impactForce = collision.relativeVelocity.magnitude;
           

            // เรียกใช้ฟังก์ชันลดเลือดของกระจก
            WindowGlass glass = collision.gameObject.GetComponent<WindowGlass>();
            if (glass != null)
            {
                float damage = impactForce * damageMultiplier;
                glass.TakeDamage(damage);
            }
        }
    }
}
