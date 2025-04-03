using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float speed = 10f;    
    public float rotationSpeed = 30f; 
    public ParticleSystem explosionEffect; 

    private void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -speed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.one * rotationSpeed * Time.deltaTime); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play(); 
            Destroy(effect.gameObject, effect.main.duration);
            Destroy(gameObject);
        }
    }
}