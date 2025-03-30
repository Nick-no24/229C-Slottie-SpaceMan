using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float speed = 2f;    // ความเร็วของอุกกาบาต
    public float rotationSpeed = 30f; // ความเร็วการหมุน
    public ParticleSystem explosionEffect; // เอฟเฟกต์ระเบิด

    private void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -speed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.one * rotationSpeed * Time.deltaTime); // หมุนให้อุกกาบาตดูสมจริง
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile")) // ถ้าถูกมิสไซล์ยิง
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play(); // เล่นเอฟเฟกต์ระเบิด
            Destroy(effect.gameObject, effect.main.duration); // ลบ Particle หลังจากเล่นจบ
            Destroy(gameObject); // ทำลายอุกกาบาต
        }
    }
}