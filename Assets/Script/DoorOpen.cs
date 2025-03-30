using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public float openHeight = 3f; // ���з���е�����͹���
    public float speed = 2f; // ��������㹡������͹
    public float stayOpenTime = 3f; // ���ҷ���е٤�ҧ��͹�Դ
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
        if (playerNearby && Input.GetButtonDown("Interact") && !isOpening) // ����� Interact
        {
            StartCoroutine(OpenAndCloseDoor());
        }
    }

    System.Collections.IEnumerator OpenAndCloseDoor()
    {
        isOpening = true;

        // ����͹���
        while (Vector3.Distance(transform.position, openPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(stayOpenTime); // ��ҧ���������

        // ����͹ŧ
        while (Vector3.Distance(transform.position, closedPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, speed * Time.deltaTime);
            yield return null;
        }

        isOpening = false;
    }
    private void OnTriggerEnter(Collider other) // ����ͼ�����������
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) // ����ͼ������Թ�͡
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactText.gameObject.SetActive(false);
        }
    }
}


