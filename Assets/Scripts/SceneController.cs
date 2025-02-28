using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerInstance : MonoBehaviour
{
    /*
        IMPORTANT: Add scenes into build settings
    */
    public static SceneManagerInstance instance;

    //public Collider trigger;

    void Awake()
    {
        //Only ever should be one instance. Destroys prior instance if already made
        if (instance != null)
        {
            Destroy(gameObject);
        }

        //Create new instance
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //Example OnClick Scene Change. Copy, rename and replace scene name to use 
    public void ExampleOnClickSceneChange()
    {
        SceneManager.LoadScene("MovementTestScene");
        return;
    }

    public void SceneChangeStore()
    {
        SceneManager.LoadScene("Store");
        return;
    }

    //TODO
    /*
    void Update()
    {
        ExampleTriggerSceneChange(trigger);
    }

    //Example OnTrigger Scene Change. Copy, rename and replace scene name to use 
    public void ExampleTriggerSceneChange(Collider col)
    {
        if (col.))
        {
            Debug.Log("Activated");
            SceneManager.LoadScene("TitleScreen");
        }
    }
    */
}
