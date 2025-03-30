using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {

        SceneManager.LoadScene("Base");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
