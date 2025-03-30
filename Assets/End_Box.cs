using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class End_Box : MonoBehaviour
{
    public GameObject canvasEnd;
    public GameObject thank;
    public GameObject cedit;

    private void Start()
    {
        canvasEnd.SetActive(false);
        thank.SetActive(false);
        cedit.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            StartCoroutine(DelayedActivation(2.0f)); // เรียกใช้ Coroutine เพื่อหน่วงเวลา 2 วินาที
        }
    }

    private IEnumerator DelayedActivation(float delay)
    {
        yield return new WaitForSeconds(delay); // รอเป็นเวลาที่กำหนด
        canvasEnd.SetActive(true);
        thank.SetActive(true);
        yield return new WaitForSeconds(delay);
        canvasEnd.SetActive(false);
        thank.SetActive(false);
        cedit.SetActive(true);

    }


}