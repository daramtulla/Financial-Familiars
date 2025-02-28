using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton to ensure one instance exists
    public AudioMixer mixer;
    public JSONDatabaseOperations db;

    private void Awake()
    {
        // Singleton to share across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolume();
    }

    public void LoadVolume()
    {
        //float value = PlayerPrefs.GetFloat("Volume", 0.5f);
        float value = db.LoadMainMenuData();

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
        //PlayerPrefs.SetFloat("Volume", value);
        //PlayerPrefs.Save();

        db.SaveMainMenuData(value);
        ApplyVolume(value);
    }
}
