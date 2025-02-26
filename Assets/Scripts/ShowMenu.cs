using UnityEngine;
using UnityEngine.UI;
public class ShowMenu : MonoBehaviour
{
    public GameObject element;

    public void ShowElement(bool unPause)
    {
        if(unPause)
            Time.timeScale = 1.0f;
        element.SetActive(!element.activeSelf);
    }
}
