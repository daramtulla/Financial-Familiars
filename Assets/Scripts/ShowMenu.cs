using UnityEngine;
using UnityEngine.UI;
public class ShowMenu : MonoBehaviour
{
    public GameObject element;
    [SerializeField] InteractionManager interactionManager;

    public void ShowElement(bool unPause)
    {
        Debug.Log("Test End Day Menu Close");
        //The if statement prevents you from being frozen when you press the end day button to end the day
        //and then press the close button.
        //Also prevents "switchInteractState()" from being called twice when F is pressed
        //(once in the Update() function of InteractionManager.cs and the other time here).
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(ShowMenu (End Day Menu)): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        if (unPause)
            Time.timeScale = 1.0f;
        element.SetActive(!element.activeSelf);
    }
}
