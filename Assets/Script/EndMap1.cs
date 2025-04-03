using TMPro;
using UnityEngine;

public class EndMap1 : MonoBehaviour
{
    private bool playerNearby = false;
    public TextMeshProUGUI interactText;

    void Update()
    {
        if (playerNearby && Input.GetButtonDown("Interact") && KeyCard.hasKey)
        {
            FindFirstObjectByType<KeyCard>().EndStage();

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
