using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        float volume =
       PlayerPrefs.GetFloat("Volume", 1f);

        AudioListener.volume = volume;

        volumeSlider.SetValueWithoutNotify(volume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.audioSource.volume = volume;
        }

        PlayerPrefs.SetFloat("Volume", volume);
    }
}
