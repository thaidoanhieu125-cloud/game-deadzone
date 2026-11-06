using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Map2 : MonoBehaviour
{
    public GameObject menuGame;
    public static string CurrentMap = "Map 2 - thành phố đổ nát";

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