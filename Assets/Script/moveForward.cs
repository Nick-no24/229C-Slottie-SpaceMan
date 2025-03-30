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
        if (collision.gameObject.CompareTag("Enemy")) // ��Ҷ١�������ԧ
        {
           
                
            Destroy(collision.gameObject);
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play(); // ����Ϳ࿡�����Դ
            boom.PlayOneShot(boomSound);
            Destroy(effect.gameObject, effect.main.duration); // ź Particle ��ѧ�ҡ��蹨�
            Destroy(gameObject); // ������ء�Һҵ
        }
    }
}
