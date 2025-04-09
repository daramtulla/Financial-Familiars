using UnityEngine;
using System.Collections;

public class TutorialPlaybyPlay : MonoBehaviour
{
    //lights
    public GameObject SpotChest;
    public GameObject SpotCauldron;
    public GameObject SpotLectern;
    public GameObject SpotEnd;
    public GameObject SpotHire;
    public GameObject SpotBoard;
    public GameObject SpotLoan;
    public GameObject tableLights;

    //keys
    private int chestUses = 0;
    private int cauldronUses = 0;
    private int lecternUses = 0;
    private int endUses = 0;
    private int hireUses = 0;
    private int boardUses = 0;
    private int loanUses = 0;

    //warnings
    public GameObject Warning1;
    public GameObject Warning2;
    public GameObject Warning4;
    public GameObject Warning5;
    public GameObject stopStocking;
    public GameObject stopShopping;
    public GameObject stopTutorial;



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
            case "EndDayTable":
                endUses++;
                break;
            case "HiringDesk":
                hireUses++;
                break;
            case "UpgradeTutorial":
                boardUses++;
                break;
            case "LoanDesk":
                loanUses++;
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
        SpotEnd.SetActive(false);
        SpotBoard.SetActive(false);
        SpotEnd.SetActive(false);
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
        yield return new WaitForSeconds(20f);

        //Step 7: Start the day
        tableLights.SetActive(false);
        stopStocking.SetActive(true);
        SpotEnd.SetActive(true);
        yield return new WaitUntil(() => endUses >= 2);
        endUses = 0;

        //Step 8: the day goes
        SpotEnd.SetActive(false);
        yield return new WaitForSeconds(42f);
        stopShopping.SetActive(true);

        //Step 9: look at the hires
        SpotHire.SetActive(true);
        yield return new WaitUntil(() => hireUses >= 2);
        hireUses = 0;

        //Step 10: look at the upgrades
        SpotHire.SetActive(false);
        SpotBoard.SetActive(true);
        yield return new WaitUntil(() => boardUses >= 2);
        boardUses = 0;

        //Step 11: Close phase
        SpotBoard.SetActive(false);
        SpotEnd.SetActive(true);
        yield return new WaitUntil(() => endUses >= 1);
        endUses = 0;

        //Step 12: check on loans
        SpotEnd.SetActive(false);
        SpotLoan.SetActive(true);
        yield return new WaitUntil(() => loanUses >= 2);
        loanUses = 0;

        //Step 13: DIY mode kid
        SpotLoan.SetActive(false);
        yield return new WaitForSeconds(15f);
        stopTutorial.SetActive(true);





        Debug.Log("Tutorial complete!");
        yield return null;
    }
}
