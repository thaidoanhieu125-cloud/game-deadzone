using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuGame;
    public GameObject soundPanel;
    public GameObject menuInstructions;

    public static string CurrentMap = "Map 4 - phòng thí nghiệm ngầm";

    void Start()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayMenuMusic();
        }
    }

    public void PlayGameInstructions()
    {
       
        Time.timeScale = 1f;
        BGMManager.Instance.PlayClick();
        BGMManager.Instance.PlayRandomGameMusic();
        SceneManager.LoadScene(MainMenuMap1.CurrentMap);
        // menuGame.SetActive(false);
    }
    public void PlayGame()
    {
        Time.timeScale = 0f;
        BGMManager.Instance.PlayClick();
         if (menuInstructions != null)
        {
            BGMManager.Instance.PlayClick();
            menuInstructions.SetActive(!menuInstructions.activeSelf);
        }
    }

    public void sound()
    {
        if (soundPanel != null)
        {
            BGMManager.Instance.PlayClick();
            soundPanel.SetActive(!soundPanel.activeSelf);
        }
        Debug.Log("Sound");
    }

    public void ExitGame()
    {
        BGMManager.Instance.PlayClick();
        Application.Quit();
        Debug.Log("Exit Game");
    }
    public void backSound()
    {
        BGMManager.Instance.PlayClick();
        soundPanel.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}