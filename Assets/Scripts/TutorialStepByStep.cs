using UnityEngine;
using System.Collections;

public class TutorialPlaybyPlay : MonoBehaviour
{
    public GameObject SpotChest;
    public GameObject SpotCauldron;
    public GameObject SpotLectern;


    void Start()
    {
        StartCoroutine(PlayTutorial());
    }

    IEnumerator PlayTutorial()
    {
        Debug.Log("Start Tutorial!");

        // Step 1: Go to chest
        SpotChest.SetActive(true);
        SpotCauldron.SetActive(false);
        SpotLectern.SetActive(false);


        Debug.Log("Tutorial complete!");
        yield return null;
    }
}
