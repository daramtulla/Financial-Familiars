using UnityEngine;
using UnityEngine.UI;
public class ShowMenu : MonoBehaviour
{
    public GameObject element;
    
    public void ShowElement()
    {
        element.SetActive(!element.activeSelf);
    }
}
