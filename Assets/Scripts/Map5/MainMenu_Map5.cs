using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Map5 : MonoBehaviour
{
    public GameObject menuGame;

    // Cập nhật tên Scene của Map 5 vào đây. 
    // LƯU Ý: Tên này phải khớp chính xác với tên Scene bạn đặt trong mục Build Settings.
    public static string CurrentMap = "Map 5 - khu vực nhiễm xạ";

    void Start()
    {
        // Khởi tạo menu nếu cần

    }
    void Awake()
    {
        // Ép giá trị này đúng mỗi khi game load lại Menu
        CurrentMap = "Map 5 - khu vực nhiễm xạ";
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        // Kiểm tra xem Scene có tồn tại không trước khi load
        SceneManager.LoadScene("Map 5 - khu vực nhiễm xạ");
        //if (!string.IsNullOrEmpty(CurrentMap))
        //{
        //    SceneManager.LoadScene(CurrentMap);
        //}
        //else
        //{
        //    Debug.LogError("Tên Map chưa được thiết lập!");
        //}
    }

    public void sound()
    {
        Debug.Log("Sound Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}