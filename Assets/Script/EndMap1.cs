using TMPro;
using UnityEngine;

public class EndMap1 : MonoBehaviour
{
    private bool playerNearby = false;
    public TextMeshProUGUI interactText;

    void Update()
    {
        if (playerNearby && Input.GetButtonDown("Interact"))
        {
            if (KeyCard.hasKey) // ✅ เช็คว่าเก็บ KeyCard แล้วหรือยัง
            {
                KeyCard.EndStage();
            }
            else
            {
                Debug.Log("❌ คุณยังไม่มี KeyCard!");
            }
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
