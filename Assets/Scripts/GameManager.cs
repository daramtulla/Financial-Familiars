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
    public Text loansTaken;
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

    //signs
    public GameObject OpenSign;
    [SerializeField] private Animator openSignAnimator;
    public GameObject ClosedSign;
    [SerializeField] private Animator closedSignAnimator;
    public GameObject RenewSign;
    [SerializeField] private Animator renewSignAnimator;

    public GameObject smallOpenSign;
    public GameObject smallClosedSign;

    public GameObject dayTimer;

    public void StartSellingPhase()
    {
        if (db.currentPlayer.cycleNum != 0)
        {
            Debug.Log("Incorrect cycle order");
        }

        db.currentPlayer.cycleNum = 1;
        soundManager.soundAudioSource.PlayOneShot(soundManager.storeSelling, 0.3f);
        cm.StartSelling();

        //open signs
        OpenSign.gameObject.SetActive(true);
        openSignAnimator.gameObject.SetActive(true);
        openSignAnimator.Play("openGlow");
        closedSignAnimator.gameObject.SetActive(false);
        ClosedSign.gameObject.SetActive(false);
        renewSignAnimator.gameObject.SetActive(false);
        RenewSign.gameObject.SetActive(false);

        smallClosedSign.gameObject.SetActive(false);
        smallOpenSign.gameObject.SetActive(true);

        dayTimer.SetActive(true);
    }

    //Close Phase starts automatically when selling phase timer ends

    public void RestartCycle()
    {
        if (db.currentPlayer.cycleNum != 2)
        {
            Debug.Log("Incorrect cycle order");
        }

        //open signs
        openSignAnimator.gameObject.SetActive(false);
        OpenSign.gameObject.SetActive(false);
        RenewSign.gameObject.SetActive(true);
        renewSignAnimator.gameObject.SetActive(true);
        renewSignAnimator.Play("openGlow");


        EndDay();
        db.currentPlayer.dailySales = 0;
        db.currentPlayer.dailyLoanAmount = 0;
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

    public float CalculateEndDayScreenResults(bool endDayButtonPressed)
    {
        Time.timeScale = 0.0f;

        float moneyMadeAmount = db.currentPlayer.dailySales - db.currentPlayer.dailyLoanAmount;

        //Display End of Day
        endScreen.SetActive(true);
        endDayTitle.text = "Day " + db.currentPlayer.GetDay() + " Results";

        float mandatoryLoansAmount = -db.currentPlayer.totalLoansPaid;


        float wagesPaidAmount = 0.0f;
        foreach (var employee in db.currentPlayer.employees)
        {
            wagesPaidAmount -= employee.salary;
        }
        if (db.CheckEmployee(10))
        {
            if (db.CheckEmployee(15))
            {
                wagesPaidAmount -= db.currentPlayer.employees.Count * 20;
            }
            else
            {
                wagesPaidAmount -= db.currentPlayer.employees.Count * 30;
            }
        }

        float loansTakenAmount = db.currentPlayer.dailyLoanAmount;

        float utilitiesCostAmount = -50.0f;

        if (db.CheckUpgrade(0))
        {
            utilitiesCostAmount *= 0.9f;
        }

        if (db.CheckEmployee(4))
        {
            if (db.CheckEmployee(15))
            {
                utilitiesCostAmount *= 0.95f;
            }
            else
            {
                utilitiesCostAmount *= 0.9f;
            }
        }
        if (db.CheckUpgrade(6))
        {
            moneyMadeAmount += 400;
        }

        //Removed loan payments from profit calc
        float netProfitBeforeTaxAmount = moneyMadeAmount - -wagesPaidAmount - -utilitiesCostAmount;
        FormatText(netProfitBeforeTax, netProfitBeforeTaxAmount);

        //Apply Tax
        float taxAmount = ApplyTax(netProfitBeforeTaxAmount - db.currentPlayer.dailyLoanAmount);
        FormatText(taxText, -taxAmount);
        Debug.Log($"Money Made: {moneyMadeAmount}");
        Debug.Log($"Tax: {taxAmount}");

        //Money Made
        FormatText(moneyMade, moneyMadeAmount);
        FormatText(loansPaid, mandatoryLoansAmount);
        FormatText(wagesPaid, wagesPaidAmount);
        FormatText(loansTaken, loansTakenAmount);
        FormatText(utilitiesCost, utilitiesCostAmount);

        Debug.Log($"wagesPaidAmount: {wagesPaidAmount}");

        float netProfitAmount = netProfitBeforeTaxAmount - taxAmount;
        FormatText(netProfit, netProfitAmount);

        if (endDayButtonPressed == true)
        {
            Debug.Log($"DBCURRENTMONEY {db.currentPlayer.currentMoney} - {wagesPaidAmount} - {utilitiesCostAmount} - {taxAmount}");
            //The Debug.Log statement above shows the wagesPaidAmount and utilitiesCostAmount as negative numbers so we add them instead of subtract them.

            //Subtract the wages, utilities cost, and tax from the current money
            db.currentPlayer.currentMoney = db.currentPlayer.currentMoney + wagesPaidAmount + utilitiesCostAmount - taxAmount;
        }
        return netProfitAmount;
    }

    public void EndDay()
    {
        float netProfitAmount = CalculateEndDayScreenResults(true);

        db.currentPlayer.IncrDay();

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
        if (moneyMadeAmount > 5000f)
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

        //Hire 6: reduces the amount of taxes
        if (db.CheckEmployee(6))
        {
            if (db.CheckEmployee(15))
            {
                taxAmount *= 0.98f;
            }
            else
            {
                taxAmount *= 0.96f;
            }
        }
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

    public void ShowClosedSign()
    {
        ClosedSign.SetActive(true);
        closedSignAnimator.gameObject.SetActive(true);
        closedSignAnimator.Play("openGlow");

        smallOpenSign.gameObject.SetActive(false);
        smallClosedSign.gameObject.SetActive(true);

        dayTimer.SetActive(false);
    }
}