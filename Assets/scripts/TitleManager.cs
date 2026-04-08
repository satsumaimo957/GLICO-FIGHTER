using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}