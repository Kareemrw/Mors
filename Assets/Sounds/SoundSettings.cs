using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioMixer masterMixer;

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100f));
    }


    public void SetVolume(float _value)
    {
        if(_value < 1)
        {
            _value = .001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(musicSlider.value);
    }

    public void RefreshSlider(float _value)
    {
        musicSlider.value = _value;
    }
}
