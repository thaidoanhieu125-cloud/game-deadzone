using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FullUI_Map5 : MonoBehaviour
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

    public AudioSource uiAudioSource; // Kéo AudioSource bạn vừa thêm vào đây
    public AudioClip gameOverClip;    // Kéo âm thanh thua vào đây
    public AudioClip winClip;         // Kéo âm thanh thắng vào đây

    void Update()
    {
        // Kiểm tra phím Escape sử dụng Input System mới
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
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
        Debug.Log("Game Exited");
    }

    public void ToggleShop()
    {
        bool isOpen = !shopPanel.activeSelf;
        shopPanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0f : 1f;
    }

    public void playFullUI()
    {
        Time.timeScale = 1f;
        MainMenu_Map5.CurrentMap = "Map 5 - khu vực nhiễm xạ";
        SceneManager.LoadScene(MainMenu_Map5.CurrentMap);
    }

    //public void ShowGameOver(int zombieKilled, int totalZombie)
    //{
    //    Debug.Log("ShowGameOver đang được gọi!");
    //    menuGameOver.SetActive(true);
    //    mapText.text = "Map: Khu Vực Nhiễm Xạ"; // Đã đổi tên Map
    //    zombieDieText.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
    //    bossDieText.text = "Boss tiêu diệt: 0 / 1";
    //    Time.timeScale = 0f;
    //}
    public void ShowGameOver(int zombieKilled, int totalZombie)
    {
        // Kiểm tra an toàn trước khi truy cập
        if (menuGameOver == null)
        {
            Debug.LogError("LỖI: menuGameOver chưa được gán trong Inspector của đối tượng " + gameObject.name);
            // Cố gắng tìm thủ công nếu bị mất tham chiếu
            menuGameOver = transform.Find("Tên_Của_Panel_GameOver_Trong_Hierarchy")?.gameObject;

            if (menuGameOver == null) return; // Thoát nếu vẫn không tìm thấy
        }

        menuGameOver.SetActive(true);

        // Kiểm tra an toàn cho các Text
        if (mapText != null) mapText.text = "Map: Khu Vực Nhiễm Xạ";
        if (zombieDieText != null) zombieDieText.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
        if (bossDieText != null) bossDieText.text = "Boss tiêu diệt: 0 / 1";
        if (uiAudioSource != null && gameOverClip != null)
        {
            uiAudioSource.PlayOneShot(gameOverClip);
        }
        Time.timeScale = 0f;
    }

    public void showMissionComplete(int zombieKilled, int totalZombie)
    {
        menuMissionComplete.SetActive(true);
        mapTextWin.text = "Map: Khu Vực Nhiễm Xạ"; // Đã đổi tên Map
        zombieDieTextWin.text = "Zombie tiêu diệt: " + zombieKilled + " / " + totalZombie;
        bossDieTextWin.text = "Boss tiêu diệt: 1 / 1";
        // --- PHÁT NHẠC THẮNG ---
        if (uiAudioSource != null && winClip != null)
        {
            uiAudioSource.PlayOneShot(winClip);
        }
        Time.timeScale = 0f;
    }

    public void backSound()
    {
        menuSound.SetActive(false);
        menuPause.SetActive(true);
    }
}