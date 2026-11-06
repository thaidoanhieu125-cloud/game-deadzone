using UnityEngine;

public class PlayerAudioMap1 : MonoBehaviour
{
    public AudioSource footstepAudio;
    public PlayerControlerMap1 player;

    void Update()
    {
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
