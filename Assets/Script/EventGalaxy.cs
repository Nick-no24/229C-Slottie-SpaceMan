
using UnityEngine;

using UnityEngine.SceneManagement;

public class EventGalaxy : MonoBehaviour
{
    private bool playerNearby = false;


    void Update()
    {
        if (playerNearby)
        {
            SceneManager.LoadScene("Mars");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Collision detected with: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            
        }
    }
}

