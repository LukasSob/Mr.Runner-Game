using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    float mainVolume;
    float musicVolume;

    private void Start()
    {
        mixer.GetFloat("mainVolume", out mainVolume);
        mixer.GetFloat("musicVolume", out musicVolume);

        mainVolumeSlider.value = Mathf.Pow(10f, mainVolume / 20);
        musicVolumeSlider.value = Mathf.Pow(10f, musicVolume / 20);
    }

    public void SetMainVolume()
    {
        float volume = mainVolumeSlider.value;
        mixer.SetFloat("mainVolume", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        mixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }

}
