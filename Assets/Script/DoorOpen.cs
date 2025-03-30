using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public float openHeight = 3f; // ระยะที่ประตูเลื่อนขึ้น
    public float speed = 2f; // ความเร็วในการเลื่อน
    public float stayOpenTime = 3f; // เวลาที่ประตูค้างก่อนปิด
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;
    private bool playerNearby = false;
    
    public TextMeshProUGUI interactText;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
        
    }

    void Update()
    {
        if (playerNearby && Input.GetButtonDown("Interact") && !isOpening) // ใช้ปุ่ม Interact
        {
            StartCoroutine(OpenAndCloseDoor());
        }
    }

    System.Collections.IEnumerator OpenAndCloseDoor()
    {
        isOpening = true;

        // เลื่อนขึ้น
        while (Vector3.Distance(transform.position, openPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(stayOpenTime); // ค้างไว้ตามเวลา

        // เลื่อนลง
        while (Vector3.Distance(transform.position, closedPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, speed * Time.deltaTime);
            yield return null;
        }

        isOpening = false;
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


