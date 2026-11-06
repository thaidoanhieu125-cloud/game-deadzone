using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FullUI_Map2 : MonoBehaviour
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
        menuPause.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuPause.SetActive(false);

        Time.timeScale = 1f;
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void sound()
    {
        menuPause.SetActive(false);
        menuSound.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
        print("exit");
    }
    public void ToggleShop()
    {
        bool isOpen = !shopPanel.activeSelf;
        shopPanel.SetActive(isOpen);
        if (isOpen)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
    public void playFullUI()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu_Map2.CurrentMap);
    }

    public void NextMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu_Map3.CurrentMap);
    }

    public void ShowGameOver(int zombieKilled, int totalZombie)
    {
        menuGameOver.SetActive(true);
        mapText.text = "Map: Thành phố đổ nát";
        zombieDieText.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
        bossDieText.text = "Boss tiêu diệt: 0 / 1";
        Time.timeScale = 0f;
    }
    public void showMissionComplete(int zombieKilled, int totalZombie)
    {
        menuMissionComplete.SetActive(true);
        mapTextWin.text = "Map: Thành phố đổ nát";
        zombieDieTextWin.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
        bossDieTextWin.text = "Boss tiêu diệt: 1 / 1";
        Time.timeScale = 0f;
    }

    public void backSound()
    {
        menuSound.SetActive(false);
        menuPause.SetActive(true);
    }
}