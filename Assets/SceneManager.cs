using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

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
}
