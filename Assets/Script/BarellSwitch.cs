using TMPro;
using UnityEngine;

public class BarellSwitch : MonoBehaviour
{
    private bool playerNearby = false;
    private bool switchActive = false;
    public GameObject On;   
    public GameObject Off;
    public GameObject Green;
    public GameObject Red;
    public GameObject[] Barrels;
    public Rigidbody rb;
    
    public AudioSource switchAudio;
    public AudioClip TurnOnSfx;
    public TextMeshProUGUI interactText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Barrels.Length > 0)
        {
            rb = Barrels[Barrels.Length - 1].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && switchActive == false)
        {
           
            switchActive = true;
            switchAudio.PlayOneShot(TurnOnSfx);
            On.SetActive(true);
            Green.SetActive(true);
            Off.SetActive(false);
            Red.SetActive(false);
            foreach (GameObject barrel in Barrels)
            {
                if (barrel.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.useGravity = true;
                }
            }


        }
    }


    private void OnTriggerEnter(Collider other) // เมื่อผู้เล่นเข้าใกล้
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other) // เมื่อผู้เล่นเดินออก
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactText.gameObject.SetActive(false);
        }
    }
}
