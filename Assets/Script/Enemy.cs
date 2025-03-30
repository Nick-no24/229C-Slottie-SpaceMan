//using UnityEngine;
//using System.Collections;
//using System.Reflection;

//public class Enemy : MonoBehaviour
//{
//    public Transform player;          // ������� (������)
//    public float moveSpeed = 10f;     // ������������͹���
//    public float stopDistance = 30f;  // ���з����ش��͹�֧������
//    public float rotationSpeed = 5f;  // ����������ع
//    public GameObject missilePrefab;  // ���������ԧ
//    public Transform firePoint;       // �ش�ԧ������
//    public float fireRate = 2f;       // �ԧ�ء� ����Թҷ�
//    public float missileSpeed = 20f;  // ��������������

//    public AudioSource Audio;
//    public AudioClip fireSound;

//    private float nextFireTime = 0f;
    
//    void Update()
//    {
//        if (player == null) return;

//        // �ӹǳ������ҧ�ҡ������
//        float distance = Vector3.Distance(transform.position, player.position);

//        // ����ѧ���֧������ش �������͹�������Ҽ�����
//        if (distance > stopDistance)
//        {
//            MoveTowardsPlayer();
//        }

//        // ��Ҷ֧�����ԧ ��ж֧�����ԧ������
//        if (distance <= stopDistance && Time.time >= nextFireTime)
//        {
//            ShootMissile();
//            Audio.PlayOneShot(fireSound);
//            nextFireTime = Time.time + fireRate; // ��������ԧ���駵���
//        }
//    }

//    void MoveTowardsPlayer()
//    {
//        // ��ع����������
//        Vector3 direction = (player.position - transform.position).normalized;
//        Quaternion lookRotation = Quaternion.LookRotation(direction);
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

//        // ����͹���仢�ҧ˹��
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
//                rb.linearVelocity = firePoint.forward * missileSpeed; // �ԧ�͡�
//            }

//            // ��Ǩ�Ѻ��ê�
//            Missile missileScript = missile.AddComponent<Missile>();
//            missileScript.damage = 10; // ��˹������
//        }
//    }
//}
