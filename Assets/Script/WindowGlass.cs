using UnityEngine;

public class WindowGlass : MonoBehaviour
{
    public float glassHealth = 10f; // เลือดของกระจก
    public AudioSource eventAudio;
    public AudioClip glassBreaking;
    public AudioClip glasscraking;
    public void TakeDamage(float damage)
    {
        glassHealth -= damage;
        Debug.Log($"🪟 กระจกโดนชน! เลือดที่เหลือ: {glassHealth}");

        if (glassHealth <= 0)
        {
            BreakGlass(); 
        }
        else { eventAudio.PlayOneShot(glasscraking); }
    }

    void BreakGlass()
    {
        
        Destroy(gameObject); 
        eventAudio.PlayOneShot(glassBreaking);
    }
}
