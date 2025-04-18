using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.Data.Common;

//Moved Loans class to db for saving and loading

public class LoansMenu : MonoBehaviour
{
    public SoundManager soundManager;

    [SerializeField] Button close;
    [SerializeField] Button up;
    [SerializeField] Button down;
    [SerializeField] Button newest;
    [SerializeField] Button oldest;
    [SerializeField] Button paynow;
    [SerializeField] GameObject loansmenu;
    [SerializeField] GameObject totalloans;
    [SerializeField] GameObject totalinterest;
    [SerializeField] GameObject currenttype;
    [SerializeField] GameObject currentamount;
    [SerializeField] GameObject currentinterest;
    [SerializeField] GameObject payInput;
    [SerializeField] int maxLoans;
    [SerializeField] InteractionManager interactionManager;
    [SerializeField] JSONDatabaseOperations db;

    //[SerializeField] RandomGenNum rnd = new RandomGenNum();

    //[SerializeField] int highOffer = 10001;

    //[SerializeField] int lowOffer = 1000;

    //[SerializeField] int maxLoans = 3;

    private int currentindex = 0;

    [SerializeField] bool debug;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //For randomly generated loans
        //rnd = new RandomGenNum();
    }

    void Start()
    {
        //GenerateOffers();
        close.onClick.AddListener(CloseThis);
        up.onClick.AddListener(Increaser);
        down.onClick.AddListener(Decreaser);
        newest.onClick.AddListener(SetYoungest);
        oldest.onClick.AddListener(SetOldest);
        paynow.onClick.AddListener(PayLoan);
    }

    void PayLoan()
    {
        List<Loan> loans = db.currentPlayer.loans;
        float payAmt;
        if (float.TryParse(payInput.GetComponent<TMP_InputField>().text, out payAmt))
        {
            Debug.Log(payAmt);

            if (db.currentPlayer.currentMoney >= payAmt && payAmt > 0 && payAmt <= loans[currentindex].amount)
            {
                loans[currentindex].amount -= payAmt;
                db.currentPlayer.currentMoney -= payAmt;
                db.currentPlayer.totalLoansPaid += payAmt;
            }

            if (loans[currentindex].amount == 0)
            {
                loans.RemoveAt(currentindex);
                if (currentindex >= loans.Count)
                {
                    currentindex = loans.Count - 1;
                }
                if (currentindex < 0) currentindex = 0;
            }

            LoansDisplay();

        }
        soundManager.ButtonClickSound();
    }

    public void CloseThis()
    {
        Debug.Log("Test Loan Menu Close");

        loansmenu.SetActive(false);
        //The if statement prevents you from being frozen when you press F to interact
        //and then press the close button.
        //Also prevents "switchInteractState()" from being called twice when F is pressed
        //(once in the Update() function of InteractionManager.cs and the other time here).
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(LoansMenu): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        soundManager.ButtonClickSound();
    }
    public void OpenThis()
    {
        loansmenu.SetActive(true);
        LoansDisplay();
    }
    void Increaser()
    {
        if (db.currentPlayer.loans.Count == 0)
        {
            return;
        }

        currentindex += 1;
        if (currentindex >= db.currentPlayer.loans.Count)
        {
            currentindex = 0;
        }

        LoansDisplay();
        soundManager.ButtonClickSound();
    }
    void Decreaser()
    {
        if (db.currentPlayer.loans.Count == 0)
        {
            return;
        }

        currentindex -= 1;
        if (currentindex < 0)
        {
            currentindex = db.currentPlayer.loans.Count - 1;
        }

        LoansDisplay();
        soundManager.ButtonClickSound();
    }
    void SetOldest()
    {
        currentindex = 0;

        LoansDisplay();
        soundManager.ButtonClickSound();
    }
    void SetYoungest()
    {
        currentindex = db.currentPlayer.loans.Count - 1;

        LoansDisplay();
        soundManager.ButtonClickSound();
    }

    /*
    //Code for randomly generating loan offers. Currently using preset loan amounts and interest
    void GenerateOffers()
    {
        //Offering five loans
        for (int i = 0; i < 5; i++)
        {
            String name = "Offer" + i;
            float amount = OfferAmount();
            float interest = OfferInterest(amount);
            //theloans.Add(new Loans(name, amount, interest));
        }
    }

    public float OfferInterest(float offer)
    {
        float baseInterest = (float)Math.Round((offer / highOffer / 10) + .05, 2);

        float interestMod = (float)rnd.GetLoanInterestModifier() / 100;

        return (float)Math.Round(baseInterest * (1 + interestMod), 2);
    }

    public float OfferAmount()
    {
        //Rounds to nearest hundred
        return (float)Math.Round((float)rnd.GetRandomLoanAmount(lowOffer, highOffer) / 100 * 100);
    }
    */

    void LoansDisplay()
    {
        List<Loan> loans = db.currentPlayer.loans;

        currentindex = Mathf.Clamp(currentindex, 0, db.currentPlayer.loans.Count - 1);

        if (debug) { Debug.Log(currentindex); }

        totalloans.GetComponent<TMP_Text>().text = db.currentPlayer.GetTotalLoansOwed(loans).ToString();
        totalinterest.GetComponent<TMP_Text>().text = db.currentPlayer.GetDailyInterest(loans).ToString();

        if (loans.Count == 0)
        {
            currentamount.GetComponent<TMP_Text>().text = "0";
        }
        else
        {
            currentamount.GetComponent<TMP_Text>().text = Math.Round(db.currentPlayer.loans[currentindex].amount, 2).ToString();
        }

        if (loans.Count == 0)
        {
            currentinterest.GetComponent<TMP_Text>().text = "0";
        }
        else
        {
            currentinterest.GetComponent<TMP_Text>().text = Math.Round(db.currentPlayer.loans[currentindex].interest, 2).ToString();
        }

        if (loans.Count == 0)
        {
            currenttype.GetComponent<TMP_Text>().text = "N/A";
        }
        else
        {
            currenttype.GetComponent<TMP_Text>().text = loans[currentindex].lender;
        }
    }
}


