using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FullUI : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject menuPause;
    public GameObject menuSound;
    public GameObject menuGameOver;
    public GameObject menuMissionComplete;
    public TMP_Text mapText;
    public TMP_Text zombieDieText;
    public TMP_Text bossDieText;
     public TMP_Text mapTextWin;
    public TMP_Text zombieDieTextWin;
    public TMP_Text bossDieTextWin;
    public AudioSource audioSource;
    public AudioClip clickClip;
    // public AudioClip warningClip;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Time.timeScale == 0f)
                Resume();
            else
                Pause();
        }
    }
    public void Pause()
    {
        PlayClick();
        menuPause.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PlayClick();
        menuPause.SetActive(false);

        Time.timeScale = 1f;
    }

    public void mainMenu()
    {
        PlayClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void sound()
    {
        PlayClick();
        menuPause.SetActive(false);
        menuSound.SetActive(true);
    }
    public void ExitGame()
    {
        PlayClick();
        Application.Quit();
        print("exit");
    }
    public void ToggleShop()
    {
        PlayClick();
        bool isOpen = !shopPanel.activeSelf;
        shopPanel.SetActive(isOpen);
        if (isOpen)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
    public void playFullUI()
    {
        PlayClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu.CurrentMap);
    }

    public void NextMap()
    {
        PlayClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu_Map5.CurrentMap);
    }

    public void ShowGameOver(int zombieKilled, int totalZombie)
    {
        menuGameOver.SetActive(true);
        mapText.text = "Map: Phòng Thí Nghiệm";
        zombieDieText.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
        bossDieText.text = "Boss tiêu diệt: 0 / 1";
        Time.timeScale = 0f;
    }
    public void showMissionComplete(int zombieKilled, int totalZombie)
    {
        menuMissionComplete.SetActive(true);
        mapTextWin.text = "Map: Phòng Thí Nghiệm";
        zombieDieTextWin.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
        bossDieTextWin.text = "Boss tiêu diệt: 1 / 1";
        Time.timeScale = 0f;
    }

    public void backSound()
    {
        PlayClick();
        menuSound.SetActive(false);
        menuPause.SetActive(true);
    }
    public void PlayClick()
    {
        audioSource.PlayOneShot(clickClip,1f);
    }
}