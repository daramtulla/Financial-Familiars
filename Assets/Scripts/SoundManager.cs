using UnityEngine;
using UnityEngine.Audio;

//I copied and modified AudioManager and put it into this script
public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public JSONDatabaseOperations db;

    public AudioSource soundAudioSource;

    public AudioClip oneItemPurchase;
    public AudioClip tenItemPurchase;
    public AudioClip itemPurchaseError;
    public AudioClip itemPlaced;
    public AudioClip itemSold;
    public AudioClip buttonClick;
    public AudioClip storeSelling;
    public AudioClip storeClosing;
    public AudioClip storeSetup;

    public void ButtonClickSound()
    {
        soundAudioSource.PlayOneShot(buttonClick, 1.0f);
    }

    private void Start()
    {
        LoadVolume();
    }

    public void LoadVolume()
    {
        float value = db.currentPlayer.sfxVolume;

        ApplyVolume(value);
    }

    public void ApplyVolume(float value)
    {
        if (value <= 0)
        {
            mixer.SetFloat("SFXVolume", -144);
        }
        else
        {
            mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        }
    }

    public void SaveVolume(float value)
    {
        db.currentPlayer.sfxVolume = value;
        ApplyVolume(value);
    }
}
