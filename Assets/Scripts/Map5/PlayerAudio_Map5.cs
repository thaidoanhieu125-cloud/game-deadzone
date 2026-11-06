using UnityEngine;

public class PlayerAudio_Map5 : MonoBehaviour
{
    public AudioSource footstepAudio;
    // Cập nhật tham chiếu tới Controller mới
    public PlayerController_Map5 player;

    void Update()
    {
        // Kiểm tra an toàn cho tham chiếu
        if (player == null) return;

        // Giả sử PlayerController_Map5 có thuộc tính MoveAction
        Vector2 move = player.MoveAction.ReadValue<Vector2>();

        if (move != Vector2.zero)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }
}