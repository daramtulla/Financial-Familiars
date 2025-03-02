using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    /*
        IMPORTANT: Add scenes into build settings
    */
    public JSONDatabaseOperations db;
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        db.LoadData();
    }

    //Example OnClick Scene Change. Copy, rename and replace scene name to use 
    public void ExampleOnClickSceneChange()
    {
        SceneManager.LoadScene("MovementTestScene");
    }

    public void OnClickSceneChangeStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void OnClickSceneChangeTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
