using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MusicSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource music;
    public Text valueText;

    public void Start()
    {
        mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
        valueText.text = $"{(PlayerPrefs.GetFloat("Volume", 1)).ToString("P0")}";
    }
    public void OnChangeSlider(float value)
    {
        valueText.text = $"{(value).ToString("P0")}";
        if (value  <= 0)
        {
            mixer.SetFloat("Volume", -144);
        }
        else
        {
            mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        }
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
}
