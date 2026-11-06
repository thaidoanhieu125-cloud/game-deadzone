using UnityEngine;

public class PlayerAudio_Map2 : MonoBehaviour
{
    public AudioSource footstepAudio;
    public PlayerController_Map2 player;

    void Update()

    {
        if (player == null || player.MoveAction == null) return;
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
