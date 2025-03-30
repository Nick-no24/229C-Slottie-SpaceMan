using UnityEngine;

public class moveForward : MonoBehaviour
{


    public float speed = 5f;
    public float delay = 3f;
    public ParticleSystem explosionEffect;

    public AudioSource boom;
    public AudioClip boomSound;

    void Start()
    {
        Destroy(gameObject, delay);
    }

    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // ถ้าถูกมิสไซล์ยิง
        {
           
                
            Destroy(collision.gameObject);
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play(); // เล่นเอฟเฟกต์ระเบิด
            boom.PlayOneShot(boomSound);
            Destroy(effect.gameObject, effect.main.duration); // ลบ Particle หลังจากเล่นจบ
            Destroy(gameObject); // ทำลายอุกกาบาต
        }
    }
}
