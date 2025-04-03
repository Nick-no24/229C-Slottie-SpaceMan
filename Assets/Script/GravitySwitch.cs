using TMPro;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private bool playerNearby = false;
    private bool switchActive = false;
    public GameObject On;
    public GameObject Off;
    public GameObject Green;
    public GameObject Red;
    public Event1 eventScript;
    public AudioSource switchAudio;
    public AudioClip TurnOnSfx;
    public TextMeshProUGUI interactText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && switchActive == false)
        {
            Debug.Log("Tid lawee");
            switchActive = true;
            switchAudio.PlayOneShot(TurnOnSfx);
            On.SetActive(true);
            Green.SetActive(true);
            Off.SetActive(false);
            Red.SetActive(false);
            eventScript.RestoreGravity();
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
            interactText.gameObject.SetActive(false);
        }
    }
}
