using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Data.Common;

public class Borrowing : MonoBehaviour
{
    public GameObject BorrowingCardPrefab;
    public Transform loanCardContainer;
    public GameObject loanLimitMessage;

    [SerializeField] GameObject borrowmenu;

    [SerializeField] JSONDatabaseOperations db;
    [SerializeField] InteractionManager im;

    public int maxLoans = 3;

    public List<Loan> availableLoans = new List<Loan>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadTestLoans();

        PopulateLoanCards();
        loanLimitMessage.SetActive(false);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);

        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(SuppliersMenu): GetInteractState() is true");
            im.SwitchInteractState();
        }
    }

    public void ToggleMenu()
    {
        borrowmenu.SetActive(!borrowmenu.activeSelf);
        if (borrowmenu.activeSelf)
        {
            PopulateLoanCards();
        }
    }


    void LoadTestLoans()
    {
        availableLoans.Clear();
        availableLoans.Add(new Loan(0, 25000f, .2f, "Candlelight Credit", false, JSONDatabaseOperations.InterestType.Flat));
        availableLoans.Add(new Loan(1, 10000f, .5f, "Dragon Investments", false, JSONDatabaseOperations.InterestType.Flat));
        availableLoans.Add(new Loan(2, 50000f, .05f, "Bank of Enchancia", false, JSONDatabaseOperations.InterestType.Flat));
        availableLoans.Add(new Loan(3, 35000f, .15f, "Turtle Tank inc.", false, JSONDatabaseOperations.InterestType.Flat));
        availableLoans.Add(new Loan(4, 15000f, .03f, "Fae Court Credit Union", false, JSONDatabaseOperations.InterestType.Compound));
    }

    void PopulateLoanCards()
    {
        Debug.Log("Current loan count: " + GetCurrentLoanCount());
        foreach (Transform child in loanCardContainer)
        {
            Destroy(child.gameObject); // Clear old cards
        }

        if (GetCurrentLoanCount() >= maxLoans)
        {
            loanCardContainer.parent.parent.gameObject.SetActive(false);
            if (loanLimitMessage != null) loanLimitMessage.SetActive(true);
            return;
        }
        else
        {
            loanCardContainer.parent.parent.gameObject.SetActive(true);
            if (loanLimitMessage != null) loanLimitMessage.SetActive(false);
        }

        foreach (Loan loan in availableLoans)
        {
            if (!loan.borrowed)
            {
                GameObject card = Instantiate(BorrowingCardPrefab, loanCardContainer);
                SetCardInfo(card, loan);
            }

        }
    }

    void SetCardInfo(GameObject card, Loan loan)
    {
        TextMeshProUGUI[] texts = card.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI t in texts)
        {
            if (t.name == "Amount") t.text = $"${loan.amount}";
            else if (t.name == "Interest") t.text = $"{loan.interest}%";
            else if (t.name == "Lender") t.text = $"{loan.lender}";
            else if (t.name == "InterestType") t.text = $"{loan.type.ToString()}";
        }

        Button borrowButton = card.transform.Find("BorrowButton").GetComponent<Button>();
        borrowButton.onClick.AddListener(() =>
        {
            loan.borrowed = true;
            db.currentPlayer.AddLoan(loan);
            db.currentPlayer.currentMoney += loan.amount;
            db.currentPlayer.dailySales += loan.amount;
            PopulateLoanCards();
        });
    }

    int GetCurrentLoanCount()
    {
        int count = 0;
        foreach (var loan in availableLoans)
        {
            if (loan.borrowed) count++;
        }
        return count;
    }
}
