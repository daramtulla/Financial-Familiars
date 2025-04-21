using UnityEngine;
using System.Collections;
using System.Data.Common;

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

    //blocks
    public GameObject lecternBlock;
    public GameObject loanBlock;
    public GameObject cauldronBlock;
    public GameObject endBlock;
    public GameObject hireBlock;
    public GameObject upgradeBlock;

    //button bools
    bool shoppingPhase = false;

    [SerializeField] bool debug;

    void Start()
    {
        StartCoroutine(PlayTutorial());
    }

    void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.Q))
        {
            lecternBlock.SetActive(false);
            loanBlock.SetActive(false);
            cauldronBlock.SetActive(false);
            endBlock.SetActive(false);
            hireBlock.SetActive(false);
            upgradeBlock.SetActive(false);
            NotifyTutorialObjectUsed("UpgradeTutorial");
        }
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
        //set blocks
        lecternBlock.SetActive(true);
        loanBlock.SetActive(true);
        cauldronBlock.SetActive(true);
        endBlock.SetActive(true);
        hireBlock.SetActive(true);
        upgradeBlock.SetActive(true);


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
        cauldronBlock.SetActive(false);

        SpotChest.SetActive(false);
        SpotCauldron.SetActive(true);
        yield return new WaitUntil(() => cauldronUses >= 2);
        cauldronUses = 0;

        //Step 3: Go to Lectern
        lecternBlock.SetActive(false);

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
        endBlock.SetActive(false);

        tableLights.SetActive(false);
        stopStocking.SetActive(true);
        SpotEnd.SetActive(true);

        Debug.Log("waiting for shopping start");
        yield return new WaitUntil(() => endUses >= 2 && shoppingPhase);
        endUses = 0;
        Debug.Log("shopping started");


        //Step 8: the day goes
        toggleShop();
        SpotEnd.SetActive(false);
        yield return new WaitForSeconds(42f);
        stopShopping.SetActive(true);

        //Step 9: look at the hires
        hireBlock.SetActive(false);

        SpotHire.SetActive(true);
        yield return new WaitUntil(() => hireUses >= 2);
        hireUses = 0;

        //Step 10: look at the upgrades
        upgradeBlock.SetActive(false);

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
        loanBlock.SetActive(false);

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

    public void toggleShop()
    {
        shoppingPhase = !shoppingPhase;
    }
}
