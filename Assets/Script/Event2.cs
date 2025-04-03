using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Event2 : MonoBehaviour
{
    public Transform cameraTransform; 
    public Transform focusPoint; 
    public float focusTime = 2f; 
    public float shakeDuration = 1f; 
    public float shakeIntensity = 0.07f;

    public Image fadeImage;
    public float fadeDuration = 5f; 

    private bool hasTriggered = false; 

    public GameObject targetObject;
    public GameObject oldprop;
    public GameObject door;
    public GameObject Fog;
    public AudioSource eventAudio;
    public AudioClip metalSfx;
    public AudioClip ExplosionSfx;
    public AudioClip AlertSfx;
    public GameObject Dialogue;
    

    private Vector3 initialLocalPos;
    void Start()
    {
        initialLocalPos = cameraTransform.localPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered) 
        {
            StartCoroutine(FadeIn());
            hasTriggered = true;
            targetObject.SetActive(true);
            eventAudio.volume = 0.2f;
            Dialogue.SetActive(true);
            eventAudio.PlayOneShot(metalSfx);

           
            StartCoroutine(FocusAndShake());
            oldprop.SetActive(false);
            door.SetActive(false);
            eventAudio.loop = true;
            eventAudio.PlayOneShot(AlertSfx);
            //Fog.SetActive(true);   
        }
    }

    private IEnumerator FocusAndShake()
    {
        float shakeTime = 0;
        while (shakeTime < shakeDuration)
        {
            cameraTransform.localPosition = initialLocalPos + Random.insideUnitSphere * shakeIntensity;
            shakeTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        Dialogue.SetActive(false);

        cameraTransform.localPosition = initialLocalPos; 
    }
    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        eventAudio.PlayOneShot(ExplosionSfx);
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        yield return new WaitForSeconds(1);
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }

}
