using UnityEngine;
using System.Collections;

public class WindowScene : MonoBehaviour
{
    public float focusDuration = 1.5f; // ���ҷ���ͧ��������ͧ⿡��
    public float closeSpot = 3f; // �ش���˹�ҵ�ҧ������͹ŧ�Ҷ֧
    public float speed = 2f; // ��������㹡������͹˹�ҵ�ҧ
    public Transform playerCamera;
    private bool hasTriggered = false;
    public GameObject Dialogue;

    [SerializeField] private GameObject window;
    [SerializeField] private Transform focusPos;
    [SerializeField] private Transform windowTargetPos; // �ش���˹�ҵ�ҧ������͹ŧ��

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

            // �����ͧ��ع��ѧ�ش⿡���������ҷ���˹� (focusDuration)
            while (elapsedTime < focusDuration)
            {
                  
                float t = elapsedTime / focusDuration;
                playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
                elapsedTime += Time.deltaTime;
                Dialogue.gameObject.SetActive(true);
                yield return null;

            }

            // �����������ع���稨�ԧ�
            playerCamera.rotation = targetRotation;
        }

        // ���������͹˹�ҵ�ҧŧ
        yield return StartCoroutine(MoveWindowDown());
        yield return new WaitForSeconds(2f);
        Dialogue.gameObject.SetActive(false);
    }

    IEnumerator MoveWindowDown()
    {
        Vector3 startPos = window.transform.position;
        Vector3 targetPos = windowTargetPos.position;
        float elapsedTime = 0f;
        float moveDuration = 2f; // ���ҷ���ͧ������˹�ҵ�ҧ����͹ŧ

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            window.transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        window.transform.position = targetPos; // ��駤�������СѺ�������
    }
}
