using UnityEngine;

public class BossAudio_Map5 : MonoBehaviour
{
    public AudioSource callSource;
    public AudioSource runSource;

    public AudioClip bossCallSound;
    public AudioClip bossRunSound;

    void Start()
    {
        InvokeRepeating("PlayCallSound", 2f, 5f);
    }

    void PlayCallSound()
    {
        // Kiểm tra Time.timeScale: Nếu game dừng (0), không phát âm thanh
        if (Time.timeScale == 0) return;

        if (bossCallSound != null && callSource != null)
        {
            callSource.PlayOneShot(bossCallSound);
        }
    }

    public void PlayRunSound()
    {
        // Kiểm tra Time.timeScale: Nếu game dừng (0), không phát âm thanh
        if (Time.timeScale == 0) return;

        if (bossRunSound != null && runSource != null)
        {
            if (!runSource.isPlaying)
            {
                runSource.clip = bossRunSound;
                runSource.Play();
            }
        }
    }

    // HÀM MỚI: Gọi hàm này từ Boss_Health_Map5 khi Boss chết
    public void StopAllBossAudio()
    {
        CancelInvoke("PlayCallSound"); // Dừng việc gầm định kỳ
        if (callSource != null) callSource.Stop();
        if (runSource != null) runSource.Stop();
    }
}