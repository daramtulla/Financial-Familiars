using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public Text valueText;
    public JSONDatabaseOperations db;
    public AudioManager am;

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
        float value = db.currentPlayer.volume;
        volumeSlider.value = value;
        UpdateVolumeText(value);
    }

    public void OnChangeSlider(float value)
    {
        UpdateVolumeText(value);
        am.SaveVolume(value); // Save and apply volume globally
    }

    private void UpdateVolumeText(float value)
    {
        valueText.text = $"{(value).ToString("P0")}";
    }
}
