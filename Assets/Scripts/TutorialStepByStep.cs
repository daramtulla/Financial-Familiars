using UnityEngine;
using System.Collections;

public class TutorialPlaybyPlay : MonoBehaviour
{
    public GameObject SpotChest;
    public GameObject SpotCauldron;
    public GameObject SpotLectern;

    private int chestUses = 0;
    private bool cauldronUsed = false;
    private bool lecternUsed = false;


    void Start()
    {
        StartCoroutine(PlayTutorial());
    }

    public void NotifyTutorialObjectUsed(string id)
    {
        Debug.Log("Tutorial object used: " + id);

        switch (id)
        {
            case "InventoryChest":
                chestUses++;
                break;
            case "cauldron":
                cauldronUsed = true;
                break;
            case "lectern":
                lecternUsed = true;
                break;
        }
    }

    IEnumerator PlayTutorial()
    {
        Debug.Log("Start Tutorial!");

        // Step 1: Go to chest
        SpotChest.SetActive(true);
        SpotCauldron.SetActive(false);
        SpotLectern.SetActive(false);
        yield return new WaitUntil(() => chestUses >= 2);

        //Step 2: Go to cauldron
        SpotChest.SetActive(false);
        SpotCauldron.SetActive(true);


        Debug.Log("Tutorial complete!");
        yield return null;
    }
}
