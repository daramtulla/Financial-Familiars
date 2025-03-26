using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Borrowing : MonoBehaviour
{
    public GameObject BorrowingCardPrefab;
    public Transform loanCardContainer; 
    public enum InterestType { Flat, Compounding }

    [System.Serializable]
    public class LoanOffer
    {
        public int Amount;
        public float Interest;
        public string Lender;
        public InterestType interestType;
        public bool borrowed = false;
    }

    public List<LoanOffer> availableLoans = new List<LoanOffer>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadTestLoans();

        PopulateLoanCards();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }


    void LoadTestLoans()
    {
        availableLoans.Clear();
        availableLoans.Add(new LoanOffer { Amount = 25000, Interest = 20f, Lender = "Candlelight Credit", interestType = InterestType.Flat });
        availableLoans.Add(new LoanOffer { Amount = 10000, Interest = 50f, Lender = "Dragon Investments", interestType = InterestType.Flat });
        availableLoans.Add(new LoanOffer { Amount = 50000, Interest = 5f, Lender = "Bank of Enchancia", interestType = InterestType.Compounding });
        availableLoans.Add(new LoanOffer { Amount = 35000, Interest = 15f, Lender = "Turtle Tank inc.", interestType = InterestType.Flat });
        availableLoans.Add(new LoanOffer { Amount = 15000, Interest = 3f, Lender = "Fae Court Credit Union", interestType = InterestType.Compounding });
    }

    void PopulateLoanCards()
    {
        foreach (Transform child in loanCardContainer)
        {
            Destroy(child.gameObject); // Clear old cards
        }

        foreach (LoanOffer loan in availableLoans)
        {
            if (!loan.borrowed){
                GameObject card = Instantiate(BorrowingCardPrefab, loanCardContainer);
                SetCardInfo(card, loan);
            }
            
        }
    }

    void SetCardInfo(GameObject card, LoanOffer loan)
    {
        TextMeshProUGUI[] texts = card.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI t in texts)
        {
            if (t.name == "Amount") t.text = $"${loan.Amount}";
            else if (t.name == "Interest") t.text = $"{loan.Interest}%";
            else if (t.name == "Lender") t.text = $"{loan.Lender}";
            else if (t.name == "InterestType") t.text = $"{loan.interestType.ToString()}";
        }

        Button borrowButton = card.transform.Find("BorrowButton").GetComponent<Button>();
        borrowButton.onClick.AddListener(() =>
        {
            loan.borrowed = true;
            PopulateLoanCards();
        });
    }
}
