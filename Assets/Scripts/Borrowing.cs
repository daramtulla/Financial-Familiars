using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Borrowing : MonoBehaviour
{
    public GameObject BorrowingCardPrefab;
    public Transform loanCardContainer; 

    [System.Serializable]
    public class LoanOffer
    {
        public int Amount;
        public float Interest;
        public string Lender;
    }

    public List<LoanOffer> availableLoans = new List<LoanOffer>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadTestLoans();

        PopulateLoanCards();
    }


    void LoadTestLoans()
    {
        availableLoans.Clear();
        availableLoans.Add(new LoanOffer { Amount = 100, Interest = 20f, Lender = "Madam Glim" });
        availableLoans.Add(new LoanOffer { Amount = 300, Interest = 50f, Lender = "Burnscale" });
        availableLoans.Add(new LoanOffer { Amount = 200, Interest = 10f, Lender = "Enchanted Mirror Bank" });
        availableLoans.Add(new LoanOffer { Amount = 150, Interest = 0f, Lender = "Tharvok the Lich" });
        availableLoans.Add(new LoanOffer { Amount = 250, Interest = 30f, Lender = "Fae Court" });
    }

    void PopulateLoanCards()
    {
        foreach (Transform child in loanCardContainer)
        {
            Destroy(child.gameObject); // Clear old cards
        }

        foreach (LoanOffer loan in availableLoans)
        {
            GameObject card = Instantiate(BorrowingCardPrefab, loanCardContainer);
            SetCardInfo(card, loan);
        }
    }

    void SetCardInfo(GameObject card, LoanOffer loan)
    {
        Text[] texts = card.GetComponentsInChildren<Text>();

        foreach (Text t in texts)
        {
            if (t.name == "Amount") t.text = $"Amount: {loan.Amount}g";
            else if (t.name == "Interest") t.text = $"Interest: {loan.Interest}%";
            else if (t.name == "Lender") t.text = $"Lender: {loan.Lender}";
        }
    }
}
