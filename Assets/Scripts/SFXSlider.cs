using UnityEngine;
using UnityEngine.UI;

//This script is a copied and modified version of AudioSlider
public class SFXSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public Text valueText;
    public JSONDatabaseOperations db;
    public SoundManager sm;

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
        float value = db.currentPlayer.sfxVolume;
        volumeSlider.value = value;
        UpdateVolumeText(value);
    }

    public void OnChangeSlider(float value)
    {
        UpdateVolumeText(value);
        db.currentPlayer.sfxVolume = value;
        sm.SaveVolume(value); // Save and apply volume globally
    }

    private void UpdateVolumeText(float value)
    {
        valueText.text = $"{(value).ToString("P0")}";
        db.SaveData();
    }
}
