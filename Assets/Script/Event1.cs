using UnityEngine;
using System.Collections;
using TMPro;


public class Event1 : MonoBehaviour
{
    public AudioSource eventAudio;
    public Transform cameraTarget; 
    public float cameraFocusTime = 3f; 
    public ParticleSystem explosionEffect;
    public AudioClip explosionSfx;


    public GameObject player;
    private Rigidbody rb;

    public Transform playerCamera; // กล้องที่อยู่กับ Player
    private bool gravityRestored = false;
    private bool hasExplosion = false;

    public ZeroGravityZone explosionScript;
    public FPSMovement movement;
    private bool hasTriggered = false;

    public GameObject playermode1;
    public GameObject playermode2;
    
    public GameObject[] Dialogue;


    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(StartEvent());
        }
    }

    IEnumerator StartEvent()
    {
        if (playerCamera != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraTarget.position - playerCamera.position);
            float elapsedTime = 0f;
            while (elapsedTime < 2f)
            {
                Dialogue[0].SetActive(true);

                playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, elapsedTime);
                elapsedTime += Time.deltaTime;
               
                yield return null;
            }
        }

        while (!hasExplosion)
        {
            Dialogue[0].SetActive(false);
            explosionEffect.Play();
            eventAudio.PlayOneShot(explosionSfx);

            if (explosionScript != null)
            {
                explosionScript.ApplyExplosionForce();

            }

            yield return new WaitForSeconds(2f);
            explosionEffect.Stop();
            hasExplosion = true;
        }
        // เก็บค่าตำแหน่งและความเร็วของตัวละครก่อนเปลี่ยนโหมด
        Vector3 currentPosition = playermode1.transform.position;
        Quaternion currentRotation = playermode1.transform.rotation;

        Rigidbody rb1 = playermode1.GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb1 != null ? rb1.linearVelocity : Vector3.zero;
        Vector3 currentAngularVelocity = rb1 != null ? rb1.angularVelocity : Vector3.zero;

        Dialogue[1].SetActive(true);
        yield return new WaitForSeconds(2f);
        Dialogue[1].SetActive(false);

        playermode1.SetActive(false);
        playermode2.SetActive(true);

        
        playermode2.transform.position = currentPosition;
        playermode2.transform.rotation = currentRotation;


        Rigidbody rb2 = playermode2.GetComponent<Rigidbody>();
        if (rb2 != null)
        {
            rb2.linearVelocity = currentVelocity;
            rb2.angularVelocity = currentAngularVelocity;
        }
    }

  
    public void RestoreGravity()
    {
        explosionScript.ResetGravity();

       
        Vector3 currentPosition = playermode2.transform.position;

        Rigidbody rb2 = playermode2.GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb2 != null ? rb2.linearVelocity : Vector3.zero;
        Vector3 currentAngularVelocity = rb2 != null ? rb2.angularVelocity : Vector3.zero;

        
        playermode2.SetActive(false);
        playermode1.SetActive(true);
        Destroy(playermode2);

        
        playermode1.transform.position = currentPosition;
        playermode1.transform.rotation = Quaternion.identity;  

        
        Rigidbody rb1 = playermode1.GetComponent<Rigidbody>();
        if (rb1 != null)
        {
            rb1.linearVelocity = currentVelocity;
            rb1.angularVelocity = Vector3.zero; 
        }
    }
}
