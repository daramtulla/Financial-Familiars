using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public Text valueText;

    private void Start()
    {
        UpdateSlider();
    }

    private void OnEnable()
    {
        UpdateSlider(); //Fixes issue where slider would not update when menu was opened for the first time
    }

    private void UpdateSlider()
    {
        float value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = value;
        UpdateVolumeText(value);
    }

    public void OnChangeSlider(float value)
    {
        UpdateVolumeText(value);
        AudioManager.Instance.SaveVolume(value); // Save and apply volume globally
    }

    private void UpdateVolumeText(float value)
    {
        valueText.text = $"{(value).ToString("P0")}";
    }
}
