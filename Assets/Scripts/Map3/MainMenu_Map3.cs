
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Map3 : MonoBehaviour
{
    public GameObject menuGame;
    public static string CurrentMap = "Map 3 - căn cứ quân sự";

    void Start()
    {
        // menuPanel.SetActive(false);
        // if (soundPanel != null)
        //     soundPanel.SetActive(false);
    }

    public void PlayGame()
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene(CurrentMap);
        // menuGame.SetActive(false);
    }

    public void sound()
    {
        // if (soundPanel != null)
        // {
        //     soundPanel.SetActive(!soundPanel.activeSelf);
        // }
        Debug.Log("Sound");
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Exit Game");
    }
}