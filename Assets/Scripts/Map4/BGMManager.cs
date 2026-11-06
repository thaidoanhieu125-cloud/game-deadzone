using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource audioSource;

    public AudioClip menuMusic;

    public AudioClip[] gameMusics;
    public AudioClip clickClip;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMenuMusic();
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        AudioListener.volume = volume;
        audioSource.volume = volume;
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.loop = true;

        audioSource.volume =
            PlayerPrefs.GetFloat("Volume", 0.3f);

        audioSource.Play();
    }

    public void PlayRandomGameMusic()
    {
        audioSource.clip =
       gameMusics[Random.Range(0, gameMusics.Length)];

        audioSource.loop = true;

        audioSource.volume =
            PlayerPrefs.GetFloat("Volume", 0.3f);

        audioSource.Play();
    }
    public void PlayClick()
    {
        audioSource.PlayOneShot(clickClip, 1f);
    }
}
