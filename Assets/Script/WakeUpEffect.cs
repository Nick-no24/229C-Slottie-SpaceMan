using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

public class WakeUpEffect : MonoBehaviour
{
    public Image wakeUpImage; 
    private Color fadeColor;
    public GameObject Dialogue;
    public AudioSource begin;
    public AudioClip Alarm;
    public CharacterController playerController;
    async void Start()
    {
        
        
        playerController.enabled = false;
        fadeColor = wakeUpImage.color;
        fadeColor.a = 1f;
        wakeUpImage.color = fadeColor;
        begin.volume = 0.2f;
        begin.PlayOneShot(Alarm);
        await Task.Delay(3000);
        StartCoroutine(BlinkEffect());
        playerController.enabled = true;    
        await Task.Delay(2000);
        Dialogue.SetActive(true);
        await Task.Delay(4000);
        Dialogue.SetActive(false);
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < 3; i++) 
        {
            yield return Fade(1f, 0.1f); 
            yield return Fade(0f, 0.2f); 
        }

        yield return Fade(0f, 2f); 
        wakeUpImage.gameObject.SetActive(false); 
      
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
