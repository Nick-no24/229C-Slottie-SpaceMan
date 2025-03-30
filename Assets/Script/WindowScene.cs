using UnityEngine;
using System.Collections;

public class WindowScene : MonoBehaviour
{
    public float focusDuration = 1.5f; // เวลาที่ต้องการให้กล้องโฟกัส
    public float closeSpot = 3f; // จุดที่หน้าต่างจะเลื่อนลงมาถึง
    public float speed = 2f; // ความเร็วในการเลื่อนหน้าต่าง
    public Transform playerCamera;
    private bool hasTriggered = false;
    public GameObject Dialogue;

    [SerializeField] private GameObject window;
    [SerializeField] private Transform focusPos;
    [SerializeField] private Transform windowTargetPos; // จุดที่หน้าต่างจะเลื่อนลงมา

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(StartEvent());
        }
    }

    IEnumerator StartEvent()
    {
        if (playerCamera != null)
        {
            Quaternion startRotation = playerCamera.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(windowTargetPos.position - playerCamera.position);
            float elapsedTime = 0f;

            // ให้กล้องหมุนไปยังจุดโฟกัสภายในเวลาที่กำหนด (focusDuration)
            while (elapsedTime < focusDuration)
            {
                  
                float t = elapsedTime / focusDuration;
                playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
                elapsedTime += Time.deltaTime;
                Dialogue.gameObject.SetActive(true);
                yield return null;

            }

            // ให้แน่ใจว่าหมุนเสร็จจริงๆ
            playerCamera.rotation = targetRotation;
        }

        // เริ่มเลื่อนหน้าต่างลง
        yield return StartCoroutine(MoveWindowDown());
        yield return new WaitForSeconds(2f);
        Dialogue.gameObject.SetActive(false);
    }

    IEnumerator MoveWindowDown()
    {
        Vector3 startPos = window.transform.position;
        Vector3 targetPos = windowTargetPos.position;
        float elapsedTime = 0f;
        float moveDuration = 2f; // เวลาที่ต้องการให้หน้าต่างเลื่อนลง

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            window.transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        window.transform.position = targetPos; // ตั้งค่าให้เป๊ะกับเป้าหมาย
    }
}
