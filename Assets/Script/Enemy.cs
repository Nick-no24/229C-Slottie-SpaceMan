//using UnityEngine;
//using System.Collections;
//using System.Reflection;

//public class Enemy : MonoBehaviour
//{
//    public Transform player;          // เป้าหมาย (ผู้เล่น)
//    public float moveSpeed = 10f;     // ความเร็วเคลื่อนที่
//    public float stopDistance = 30f;  // ระยะที่หยุดก่อนถึงผู้เล่น
//    public float rotationSpeed = 5f;  // ความเร็วหมุน
//    public GameObject missilePrefab;  // มิสไซล์ที่ยิง
//    public Transform firePoint;       // จุดยิงมิสไซล์
//    public float fireRate = 2f;       // ยิงทุกๆ กี่วินาที
//    public float missileSpeed = 20f;  // ความเร็วมิสไซล์

//    public AudioSource Audio;
//    public AudioClip fireSound;

//    private float nextFireTime = 0f;
    
//    void Update()
//    {
//        if (player == null) return;

//        // คำนวณระยะห่างจากผู้เล่น
//        float distance = Vector3.Distance(transform.position, player.position);

//        // ถ้ายังไม่ถึงระยะหยุด ให้เคลื่อนที่เข้าหาผู้เล่น
//        if (distance > stopDistance)
//        {
//            MoveTowardsPlayer();
//        }

//        // ถ้าถึงระยะยิง และถึงเวลายิงมิสไซล์
//        if (distance <= stopDistance && Time.time >= nextFireTime)
//        {
//            ShootMissile();
//            Audio.PlayOneShot(fireSound);
//            nextFireTime = Time.time + fireRate; // ตั้งเวลายิงครั้งต่อไป
//        }
//    }

//    void MoveTowardsPlayer()
//    {
//        // หมุนไปหาเป้าหมาย
//        Vector3 direction = (player.position - transform.position).normalized;
//        Quaternion lookRotation = Quaternion.LookRotation(direction);
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

//        // เคลื่อนที่ไปข้างหน้า
//        transform.position += transform.forward * moveSpeed * Time.deltaTime;
//    }

//    void ShootMissile()
//    {
//        if (missilePrefab != null && firePoint != null)
//        {
//            GameObject missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
//            Rigidbody rb = missile.GetComponent<Rigidbody>();
//            if (rb != null)
//            {
//                rb.linearVelocity = firePoint.forward * missileSpeed; // ยิงออกไป
//            }

//            // ตรวจจับการชน
//            Missile missileScript = missile.AddComponent<Missile>();
//            missileScript.damage = 10; // กำหนดดาเมจ
//        }
//    }
//}
