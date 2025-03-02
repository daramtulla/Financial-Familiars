using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public JSONDatabaseOperations db;

    private void Awake()
    {
        LoadVolume();
    }

    public void LoadVolume()
    {
        float value = db.currentPlayer.volume;

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
        db.currentPlayer.volume = value;
        ApplyVolume(value);
    }
}
