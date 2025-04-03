using UnityEngine;
using System.Collections;

public class WindowScene : MonoBehaviour
{
    public float focusDuration = 10f; 
    public float closeSpot = 3f; 
    public float speed = 2f;
    public Transform playerCamera;
    private bool hasTriggered = false;
    public GameObject Dialogue;
    public CharacterController characterController;


    [SerializeField] private GameObject window;
    [SerializeField] private Transform focusPos;
    [SerializeField] private Transform windowTargetPos; 

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
        characterController.enabled = false;
        StartCoroutine(MoveWindowDown());
        if (playerCamera != null)
        {
            Quaternion startRotation = playerCamera.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(windowTargetPos.position - playerCamera.position);
            float elapsedTime = 0;

            while (elapsedTime < 3f)
            {
             
              playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
              elapsedTime += Time.deltaTime;
               
                yield return null;
               
            }

            
            playerCamera.rotation = targetRotation;
            
           
            Dialogue.gameObject.SetActive(true);
        }


        yield return new WaitForSeconds(2f);
        Dialogue.gameObject.SetActive(false);
        characterController.enabled = true;
    }


    IEnumerator MoveWindowDown()
    {
        Vector3 startPos = window.transform.position;
        Vector3 targetPos = windowTargetPos.position;
        float elapsedTime = 0f;
        float moveDuration = 2f; 
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            window.transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        window.transform.position = targetPos; 
    }
}
