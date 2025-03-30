using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

public class WakeUpEffect : MonoBehaviour
{
    public Image wakeUpImage; // ใส่ Image สีดำ
    private Color fadeColor;
    public GameObject Dialogue;
    public AudioSource begin;
    public AudioClip Alarm;
    async void Start()
    {
        if (!Application.isPlaying) return; // กันไม่ให้รันตอนอยู่ใน Scene Editor

        fadeColor = wakeUpImage.color;
        fadeColor.a = 1f;
        wakeUpImage.color = fadeColor;
        begin.PlayOneShot(Alarm);
        await Task.Delay(3000);
        StartCoroutine(BlinkEffect());
        await Task.Delay(2000);
        Dialogue.SetActive(true);
        await Task.Delay(4000);
        Dialogue.SetActive(false);
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < 3; i++) // กระพริบ 3 ครั้ง
        {
            yield return Fade(1f, 0.1f); // ปิดตาเร็ว
            yield return Fade(0f, 0.2f); // ลืมตาช้า
        }

        yield return Fade(0f, 2f); // ลืมตาถาวร
        wakeUpImage.gameObject.SetActive(false); // ปิด UI
      
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        float startAlpha = wakeUpImage.color.a;

        while (elapsedTime < duration)
        {
            fadeColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            wakeUpImage.color = fadeColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeColor.a = targetAlpha;
        wakeUpImage.color = fadeColor;
    }
}
