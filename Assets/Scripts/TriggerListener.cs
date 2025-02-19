using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerListener : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Activated");
            SceneManager.LoadScene("TitleScreen");
        }
    }
}