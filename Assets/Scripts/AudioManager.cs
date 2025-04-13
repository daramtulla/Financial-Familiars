using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public JSONDatabaseOperations db;
    public AudioManager am;

    private void Start()
    {
        LoadVolume();
    }

    public void LoadVolume()
    {
        float value = db.currentPlayer.musicVolume;

        ApplyVolume(value);
    }

    public void ApplyVolume(float value)
    {
        if (value <= 0)
        {
            mixer.SetFloat("Volume", -144);
        }
        else
        {
            mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        }
    }

    public void SaveVolume(float value)
    {
        db.currentPlayer.musicVolume = value;
        ApplyVolume(value);
    }
}
