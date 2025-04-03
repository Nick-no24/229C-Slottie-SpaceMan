using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class KeyCard : MonoBehaviour
{
    private bool playerNearby = false;
    public static bool hasKey = false;
    public TextMeshProUGUI interactText;
    public AudioSource v;
    public AudioClip warp;
    public AudioClip pickup;
    public VideoPlayer videoPlayer;  
   
    private AsyncOperation sceneLoading;


    private void Start()
    {
        
        sceneLoading = SceneManager.LoadSceneAsync("Galaxy");
        sceneLoading.allowSceneActivation = false;

        
        
    }

    void Update()
    {
        if (playerNearby && Input.GetButtonDown("Interact") && !hasKey)
        {
            hasKey = true;
            v.PlayOneShot(pickup);
            interactText.gameObject.SetActive(false);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
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

    public void EndStage()
    {
        if (hasKey)
        {
            Debug.Log("Hyperspace Jump!");
            
          
            v.PlayOneShot(warp);
            sceneLoading.allowSceneActivation = true;
            
        }
    }

    
}
