using UnityEngine;
using System.Collections;

public class TutorialPlaybyPlay : MonoBehaviour
{
    //lights
    public GameObject SpotChest;
    public GameObject SpotCauldron;
    public GameObject SpotLectern;
    public GameObject tableLights;

    //keys
    private int chestUses = 0;
    private int cauldronUses = 0;
    private int lecternUses = 0;

    //warnings
    public GameObject Warning1;
    public GameObject Warning2;
    public GameObject Warning4;
    public GameObject Warning5;


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
            case "ShippingCauldron":
                cauldronUses++;
                break;
            case "BorrowingTutorial":
                lecternUses++;
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
        Warning4.SetActive(false);
        Warning5.SetActive(false);
        tableLights.SetActive(false);
        yield return new WaitUntil(() => chestUses >= 2);
        chestUses = 0;

        //Step 2: Go to cauldron
        SpotChest.SetActive(false);
        SpotCauldron.SetActive(true);
        yield return new WaitUntil(() => cauldronUses >= 2);
        cauldronUses = 0;

        //Step 3: Go to Lectern
        SpotCauldron.SetActive(false);
        SpotLectern.SetActive(true);
        yield return new WaitUntil(() => lecternUses >= 2);
        lecternUses = 0;
        Warning2.SetActive(false);
        Warning4.SetActive(true);

        //Step 4: Go to cauldron
        SpotLectern.SetActive(false);
        SpotCauldron.SetActive(true);
        yield return new WaitUntil(() => cauldronUses >= 1);
        cauldronUses = 0;

        //Step 5: Go to chest
        Warning1.SetActive(false);
        Warning5.SetActive(true);
        SpotCauldron.SetActive(false);
        SpotChest.SetActive(true);
        yield return new WaitUntil(() => chestUses >= 1);
        chestUses = 0;

        //Step 6: Go stock some shelves
        SpotChest.SetActive(false);
        tableLights.SetActive(true);




        Debug.Log("Tutorial complete!");
        yield return null;
    }
}
