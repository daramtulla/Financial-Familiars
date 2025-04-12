
using UnityEngine;
using UnityEngine.SceneManagement;

//[CreateAssetMenu(fileName = "VolumeData", menuName = "ScriptableObjects/VolumeData", order = 1)]
public class SceneController : MonoBehaviour
{
    public AudioManager am;
    public float volume;
    public float volumeSfx;


    /*
        IMPORTANT: Add scenes into build settings
    */
    public JSONDatabaseOperations db;

    public bool debug;
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        db.currentPlayer.musicVolume = volume;
        db.currentPlayer.sfxVolume = volumeSfx;

        db.SaveData();
    }

    public void OnSceneUnload(Scene scene, LoadSceneMode mode)
    {
        volume = db.currentPlayer.musicVolume;
        volumeSfx = db.currentPlayer.sfxVolume;
    }

    //Example OnClick Scene Change. Copy, rename and replace scene name to use 
    public void OnClickSceneChangeTutorial()
    {
        db.SaveData();
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickSceneChangeStore()
    {
        db.SaveData();
        SceneManager.LoadScene("Store");
    }

    public void OnClickSceneChangeTitle()
    {
        Time.timeScale = 1.0f;
        db.SaveData();
        SceneManager.LoadScene("TitleScreen");
    }

    void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.Tilde))
        {
            OnClickSceneChangeTutorial();
        }

        if (debug && Input.GetKeyDown(KeyCode.Tab))
        {
            OnClickSceneChangeStore();
        }
    }
}
