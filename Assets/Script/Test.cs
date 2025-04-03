using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    public Camera mainCamera;
    public float jumpFOV = 120f;
    public float normalFOV = 60f;
    public float effectDuration = 2f;

    private void Start()
    {
        StartCoroutine(DoHyperSpaceEffect());
    }

    private IEnumerator DoHyperSpaceEffect()
    {
        float elapsed = 0f;
        while (elapsed < effectDuration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(normalFOV, jumpFOV, elapsed / effectDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = normalFOV;
    }
}
