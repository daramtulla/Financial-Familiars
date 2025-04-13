using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Data.Common;

public class Borrowing : MonoBehaviour
{
    public SoundManager soundManager;

    public GameObject BorrowingCardPrefab;
    public Transform loanCardContainer;
    public GameObject loanLimitMessage;

    [SerializeField] GameObject borrowmenu;

    [SerializeField] JSONDatabaseOperations db;
    [SerializeField] InteractionManager im;

    public int maxLoans = 3;

    //tutorial specific
    [SerializeField] bool tutorialMode = false;
    [SerializeField] GameObject tutorialBlockA;
    [SerializeField] GameObject tutorialBlockB;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            //tutorial specific
            if (tutorialMode)
            {
                if (GetCurrentLoanCount() > 0)
                {
                    tutorialBlockA.SetActive(false);
                    tutorialBlockB.SetActive(true);

                }
            }

            loanCardContainer.parent.parent.gameObject.SetActive(true);
            if (loanLimitMessage != null) loanLimitMessage.SetActive(false);
        }

        foreach (Loan loan in db.currentPlayer.availableLoans)
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
            soundManager.ButtonClickSound();
            db.SaveData();
        });
    }

    int GetCurrentLoanCount()
    {
        return db.currentPlayer.loans.Count;
    }
}
