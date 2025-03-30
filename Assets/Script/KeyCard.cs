using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCard : MonoBehaviour
{
    private bool playerNearby = false;
    public static bool hasKey = false; 
    public TextMeshProUGUI interactText;
    public AudioSource v;
    public AudioClip warp;
    public AudioClip pickup;

    void Update()
    {
        if (playerNearby && Input.GetButtonDown("Interact") && !hasKey)
        {
            hasKey = true;
            v.PlayOneShot(pickup);
            interactText.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Debug.Log("Ahh I got keycard I can go out of here");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
           
        }
    }

    public static void EndStage()
    {      
        SceneManager.LoadScene("Galaxy");
    }
}
