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

    [SerializeField] InteractionManager interactionManager;
    [SerializeField] JSONDatabaseOperations db;

    [SerializeField] RandomGenNum rnd;

    [SerializeField] int highOffer = 10001;

    [SerializeField] int lowOffer = 1000;

    [SerializeField] int maxLoans = 3;

    public string numDisp(float numba)
    {
        string preturnable;
        string returnable = "";
        bool dotfound = false;
        preturnable = MathF.Round(numba, 2, 0).ToString();

        if (preturnable.Length < 3)
        {
            return "$" + preturnable + ".00";
        }

        int digitcount = 1;
        for (int i = preturnable.Length - 1; i >= 0; i--)
        {
            if (preturnable[i] == '.')
            {
                if (returnable.Length == 1)
                {
                    returnable = returnable + "0";
                }
                digitcount = 0;
                dotfound = true;
            }

            returnable = preturnable[i] + returnable;
            if (digitcount == 3)
            {
                if (i != 0)
                {
                    returnable = "," + returnable;
                }

                if (!dotfound)
                {
                    returnable = returnable + ".00";
                    //not found, but we ain looking no more
                    dotfound = true;
                }
                digitcount = 1;
            }
            else
            {
                digitcount++;
            }
        }

        ///returnable[returnable.Length];
        return "$" + returnable;
    }

    private int currentindex = 0;

    Color ogcolor = new Color(53, 41, 42, 255);
    Color target = new Color(118, 89, 60, 255);
    Color current = new Color(53, 41, 42, 255);

    List<Loans> theloans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        theloans = db.currentPlayer.loans;
        rnd = new RandomGenNum();
    }

    void Start()
    {
        loansmenu.SetActive(false);

        GenerateOffers();
        close.onClick.AddListener(CloseThis);
        up.onClick.AddListener(Increaser);
        down.onClick.AddListener(Decreaser);
        newest.onClick.AddListener(setset2);
        oldest.onClick.AddListener(setset1);
        paynow.onClick.AddListener(ClearLoan);
        //highlighttext();

        ogcolor = new Color(53, 41, 42, 255);
        target = new Color(195, 141, 87, 255);
        current = new Color(53, 41, 42, 255);

    }

    // Update is called once per frame
    void Update()
    {

        LoansDisplay();
        if (theloans[currentindex].interest > 0)
        {
            highlighttext(currentinterest, currentamount);
        }
        else
        {
            highlighttext(currentamount, currentinterest);
        }

        //Debug.Log(moneyspent);
    }

    bool AttemptLoanPayment(float value)
    {
        if (db.currentPlayer.currentMoney >= value)
        {
            return true;
        }
        return false;
    }

    void ClearLoan()
    {
        float value = theloans[currentindex].amount;

        //TODO
        if (true)
        {
            //TODO play noise or display graphic showing you cant pay
            return;
        }
        else
        {
            theloans[currentindex].interest -= value;
            db.currentPlayer.currentMoney -= value;
        }
    }

    public void CloseThis()
    {
        Debug.Log("Test Loan Menu Close");
        //The if statement prevents you from being frozen when you press F to interact
        //and then press the close button.
        //Also prevents "switchInteractState()" from being called twice when F is pressed
        //(once in the Update() function of InteractionManager.cs and the other time here).
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(LoansMenu): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        loansmenu.SetActive(false);
    }
    public void OpenThis()
    {
        loansmenu.SetActive(true);
    }
    void Increaser()
    {
        currentindex += 1;
        if (currentindex >= maxLoans)
        {
            currentindex = 0;
        }
    }
    void Decreaser()
    {
        currentindex -= 1;
        if (currentindex < 0)
        {
            currentindex = maxLoans - 1;
        }
    }
    void setset1()
    {
        currentindex = 0;
    }
    void setset2()
    {
        currentindex = maxLoans - 1;
    }


    float cycler = 0;
    void highlighttext(GameObject tom, GameObject jerry)
    {
        cycler = Mathf.Sin((Time.time * 1.25f) * Mathf.PI);
        float r = ogcolor.r + (target.r - ogcolor.r) * cycler;
        float g = ogcolor.g + (target.g - ogcolor.g) * cycler;
        float b = ogcolor.b + (target.b - ogcolor.b) * cycler;
        current = new Color(r, g, b);

        tom.GetComponent<TMP_Text>().color = new Color(current.r / 255, current.g / 255, current.b / 255);
        //currentinterest.GetComponent<TMP_Text>().color = new Color(0,0,0,0);
        jerry.GetComponent<TMP_Text>().color = new Color(ogcolor.r / 255, ogcolor.g / 255, ogcolor.b / 255);
    }


    void GenerateOffers()
    {
        //Offering five loans
        for (int i = 0; i < 5; i++)
        {
            String name = "Offer" + i;
            float amount = OfferAmount();
            float interest = OfferInterest(amount);
            theloans.Add(new Loans(name, amount, interest));
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

    void LoansDisplay()
    {

        if (currentindex < 0)
        {
            currentindex = 0;
        }

        while (currentindex >= 3)
        {
            currentindex--;
            if (currentindex < 0)
            {
                currentindex = 0;
                break;
            }
        }

        totalloans.GetComponent<TMP_Text>().text = db.currentPlayer.LoanCount().ToString();
        totalinterest.GetComponent<TMP_Text>().text = "N/A"; //Does it make sense to add interest?
        currentamount.GetComponent<TMP_Text>().text = Math.Round(db.currentPlayer.loans[currentindex].interest, 2).ToString();
        currentinterest.GetComponent<TMP_Text>().text = Math.Round(db.currentPlayer.loans[currentindex].amount, 2).ToString();
        currenttype.GetComponent<TMP_Text>().text = currentindex.ToString();
    }
}

