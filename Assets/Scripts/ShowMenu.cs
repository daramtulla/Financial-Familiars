using UnityEngine;
//using System.Collections;
using UnityEngine.UI;
public class ShowMenu : MonoBehaviour
{
    /*
    public SoundManager soundManager;
    public static bool isPlayingSound = false;
    public static bool inCoroutine = false;
    */

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

        /* I wasted all my time trying to get the buttonClick sound to not stack and now the close button for settings doesn't work
        Debug.Log($"isPlayingSound: {isPlayingSound}");
        if (!isPlayingSound)
        //Prevents buttonClick sound to stack due to the ShowMenu script being on the SettingsButton gameobject twice
        {
            soundManager.soundAudioSource.PlayOneShot(soundManager.buttonClick, 1f);
            isPlayingSound = true;
            StartCoroutine(IsPlayingSoundRoutine());
        }
        else
        {
            if (!inCoroutine)
            {
                element.SetActive(!element.activeSelf);
            }
        }
        */
    }

    /*
    //This function is copied from InteractableDisplay.cs
    IEnumerator IsPlayingSoundRoutine()
    {
        inCoroutine = true;
        Debug.Log($"Test coroutine before");
        yield return new WaitForSecondsRealtime(0.0000000001f); //I used Realtime because the timescale is 0 when paused
        Debug.Log($"Test coroutine after");
        isPlayingSound = false;
        Debug.Log($"isPlayingSound (Coroutine): {isPlayingSound}");
        inCoroutine = false;
        element.SetActive(!element.activeSelf);
    }
    */
}
