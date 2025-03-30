using UnityEngine;

public class SoundManager : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
