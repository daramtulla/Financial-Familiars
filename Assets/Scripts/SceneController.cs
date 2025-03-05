using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioManager am;
    /*
        IMPORTANT: Add scenes into build settings
    */
    public JSONDatabaseOperations db;
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        db.SaveData();
    }

    //Example OnClick Scene Change. Copy, rename and replace scene name to use 
    public void ExampleOnClickSceneChange()
    {
        SceneManager.LoadScene("MovementTestScene");
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
}
