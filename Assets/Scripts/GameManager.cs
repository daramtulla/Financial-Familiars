using System;
using System.Data.Common;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;

    public BudgetMenu budgetMenu;
    public SuppliersMenu suppliersMenu;
    public PlayerManager playerManager;
    public InventoryMenu inventoryMenu;
    public JSONDatabaseOperations db;

    //End of Day Visuals
    public GameObject endScreen;
    public Text endDayTitle;
    public Text moneyMade;
    public Text loansPaid;
    public Text wagesPaid;
    public Text upgradeUpkeep;
    public Text netProfit;
    public Text utilitiesCost;
    public Text taxText;
    public Text netProfitBeforeTax;

    //Debug Text
    public TMP_Text dailySalesNumber;
    public Text moneyCount;

    //Pause Menu Logic
    public GameObject pauseMenu;

    //Day Phase Logic
    [SerializeField] CustomerManager cm;
    [SerializeField] Boolean debug;

    public void StartSellingPhase()
    {
        if (db.currentPlayer.cycleNum != 0)
        {
            Debug.Log("Incorrect cycle order");
        }

        db.currentPlayer.cycleNum = 1;
        soundManager.soundAudioSource.PlayOneShot(soundManager.storeSelling, 0.3f);
        cm.StartSelling();
    }

    //Close Phase starts automatically when selling phase timer ends

    public void RestartCycle()
    {
        if (db.currentPlayer.cycleNum != 2)
        {
            Debug.Log("Incorrect cyle order");
        }

        db.currentPlayer.IncrDay();
        EndDay();
        db.currentPlayer.dailySales = 0;
        //Debug.Log($"RestartCycle(): db.currentPlayer.dailySales: {db.currentPlayer.dailySales}");
        db.currentPlayer.cycleNum = 0;
        soundManager.soundAudioSource.PlayOneShot(soundManager.storeSetup, 1.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        //For testing
        if (debug && Input.GetKeyDown("N"))
        {
            StartSellingPhase();
        }

        //For testing
        if (debug && Input.GetKeyDown("M"))
        {
            StartSellingPhase();
        }
    }
    public void PauseGame()
    {
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        }
    }

    public void EndDay()
    {
        Time.timeScale = 0.0f;

        float moneyMadeAmount = db.currentPlayer.dailySales;

        //Display End of Day
        endScreen.SetActive(true);
        endDayTitle.text = "Day " + db.currentPlayer.GetDay() + " Results";

        //HIRE 13: Lowers interest rates on loans
        //TODO: Add loan logic
        //For now: say it takes $50 to pay off every day
        float mandatoryLoansAmount = -50.0f;


        float wagesPaidAmount = 0.0f;
        foreach (var employee in db.currentPlayer.employees)
        {
            wagesPaidAmount -= employee.salary;
        }


        //TODO: Discuss if we're keeping upgrade upkeeps
        //For now: set Upgrades to 0 for no upgrades
        float upgradeUpkeepAmount = 0.0f;

        float utilitiesCostAmount = -50.0f;

        if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 0))
        {
            utilitiesCostAmount *= 0.9f;
        }

        //TODO: Add rent? Or lump it all in utilities. for now, just lump it in with utilities
        if (db.currentPlayer.employees.Any(employee => employee.id == 4))
        {
            utilitiesCostAmount *= 0.9f;
        }

        float netProfitBeforeTaxAmount = moneyMadeAmount - -mandatoryLoansAmount - -wagesPaidAmount - -upgradeUpkeepAmount - -utilitiesCostAmount;
        FormatText(netProfitBeforeTax, netProfitBeforeTaxAmount);

        //Apply Tax
        float taxAmount = ApplyTax(netProfitBeforeTaxAmount);
        FormatText(taxText, -taxAmount);
        Debug.Log($"Money Made: {moneyMadeAmount}");
        Debug.Log($"Tax: {taxAmount}");

        //Money Made
        FormatText(moneyMade, moneyMadeAmount);
        FormatText(loansPaid, mandatoryLoansAmount);
        FormatText(wagesPaid, wagesPaidAmount);
        FormatText(upgradeUpkeep, upgradeUpkeepAmount);
        FormatText(utilitiesCost, utilitiesCostAmount);

        Debug.Log($"wagesPaidAmount: {wagesPaidAmount}");

        float netProfitAmount = netProfitBeforeTaxAmount - taxAmount;
        FormatText(netProfit, netProfitAmount);

        //TODO: Split money between savings and spending
        //For now: Send all money to one account
        playerManager.UpdatePlayerStats(netProfitAmount);
    }
    public void FormatText(Text textObject, float amount)
    {
        if (amount > 0)
        {
            textObject.text = "$" + amount.ToString("N2");
            textObject.color = Color.green;
        }
        else if (amount < 0)
        {
            textObject.text = "-$" + (-amount).ToString("N2");
            textObject.color = Color.red;
        }
        else
        {
            textObject.text = "$" + amount.ToString("N2");
            textObject.color = Color.gray;
        }
    }

    public float ApplyTax(float moneyMadeAmount)
    {
        float taxAmount = 0.0f;

        //My aim is to implement a progressive tax system (higher profit = higher tax)
        if(moneyMadeAmount > 5000f)
        {
            //only profit made above 5000 is taxed at the 30% tax rate
            taxAmount = ((moneyMadeAmount - 5000f) * 0.3f) + 700f;
        }
        else if (moneyMadeAmount > 2500f)
        {
            //only profit made above 2500 is taxed at the 20% tax rate
            taxAmount = ((moneyMadeAmount - 2500f) * 0.2f) + 200f;
        } 
        else if (moneyMadeAmount > 500f)
        {
            //only profit made above 500 is taxed at the 10% tax rate
            taxAmount = (moneyMadeAmount - 500f) * 0.1f;
        }
        //500 and below profit made means there is no tax
        return taxAmount;
    }

    public void Add100DailySales()
    {
        //Used to test the tax system
        db.currentPlayer.dailySales += 100;
        dailySalesNumber.text = db.currentPlayer.dailySales.ToString("N2");

        db.currentPlayer.currentMoney += 100;
        moneyCount.text = db.currentPlayer.currentMoney.ToString("N2");
    }
}