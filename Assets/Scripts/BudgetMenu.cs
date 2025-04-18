using UnityEngine;



using TMPro;
using System.Data.Common;


public class BudgetMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject budgetPanel;
    [SerializeField] InteractionManager interactionManager;

    [SerializeField] TMP_InputField textInputField;

    //spreadsheet variables
    //earned income
    private float sales = 0;
    public TextMeshProUGUI salesText;
    private float subscriptions = 0;
    public TextMeshProUGUI subsText;
    private float adRevenue = 0;
    public TextMeshProUGUI adrevText;
    private float serviceFees = 0;
    public TextMeshProUGUI servfeeText;
    //fixed costs
    private float rent = 0;
    public TextMeshProUGUI rentText;
    private float insurance = 0;
    public TextMeshProUGUI insuranceText;
    private float utilities = 0;
    public TextMeshProUGUI utilitiesText;
    private float taxes = 0;
    public TextMeshProUGUI taxesText;
    //variable costs
    private float inventory = 0;
    public TextMeshProUGUI inventoryText;
    private float wages = 0;
    public TextMeshProUGUI wagesText;
    private float marketing = 0;
    public TextMeshProUGUI marketingText;
    private float repairsMaint = 0;
    public TextMeshProUGUI repairsMaintText;
    private float otherVC = 0;
    public TextMeshProUGUI otherVCText;
    //Loan Payments
    private float startupLoan = 0;
    public TextMeshProUGUI startupLoanText;
    private float loanA = 0;
    public TextMeshProUGUI loanAText;
    private float loanB = 0;
    public TextMeshProUGUI loanBText;
    private float loanC = 0;
    public TextMeshProUGUI loanCText;
    //totals
    private float incomeTtl = 0;
    public TextMeshProUGUI incmTtlText;
    private float expenseTtl = 0;
    public TextMeshProUGUI expenseTtlText;
    private float netProfit = 0;
    public TextMeshProUGUI netProfitText;
    private float savings = 0;
    public TextMeshProUGUI savingsText;
    private float cashOnHand = 0;
    public TextMeshProUGUI cohText;

    [SerializeField] JSONDatabaseOperations db;

    void Awake()
    {
        sales = db.currentPlayer.totalSales;

        inventory = db.currentPlayer.purchases;


        netProfit = sales - inventory;
    }

    void Start()
    {
        budgetPanel.SetActive(false);
        UpdateBudgetUI();

    }

    public void CloseMenu()
    {
        Debug.Log("Test Budget Menu Close");
        //The if statement prevents you from being frozen when you press B to open the budget menu
        //and then press the close button.
        //Also prevents "switchInteractState()" from being called twice when F is pressed
        //(once in the Update() function of InteractionManager.cs and the other time here).
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(BudgetMenu): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        budgetPanel.SetActive(false);
    }

    private void UpdateBudgetUI()
    {
        //latest values please
        salesText.text = "$" + sales.ToString("0.00");
        subsText.text = "$" + subscriptions.ToString("0.00");
        adrevText.text = "$" + adRevenue.ToString("0.00");
        servfeeText.text = "$" + serviceFees.ToString("0.00");
        rentText.text = "$" + rent.ToString("0.00");
        insuranceText.text = "$" + insurance.ToString("0.00");
        utilitiesText.text = "$" + utilities.ToString("0.00");
        taxesText.text = "$" + taxes.ToString("0.00");
        inventoryText.text = "$" + inventory.ToString("0.00");
        wagesText.text = "$" + wages.ToString("0.00");
        marketingText.text = "$" + marketing.ToString("0.00");
        repairsMaintText.text = "$" + repairsMaint.ToString("0.00");
        otherVCText.text = "$" + otherVC.ToString("0.00");
        startupLoanText.text = "$" + startupLoan.ToString("0.00");
        loanAText.text = "$" + loanA.ToString("0.00");
        loanBText.text = "$" + loanB.ToString("0.00");
        loanCText.text = "$" + loanC.ToString("0.00");

        //calculate totals
        incomeTtl = sales + subscriptions + adRevenue + serviceFees;
        expenseTtl = rent + insurance + utilities + taxes + inventory + wages + marketing + repairsMaint + otherVC + startupLoan + loanA + loanB + loanC;
        netProfit = incomeTtl - expenseTtl;
        cashOnHand = savings + netProfit;

        //update totals
        incmTtlText.text = "$" + incomeTtl.ToString("0.00");
        expenseTtlText.text = "$" + expenseTtl.ToString("0.00");
        netProfitText.text = "$" + netProfit.ToString("0.00");
        savingsText.text = "$" + savings.ToString("0.00");
        cohText.text = "$" + cashOnHand.ToString("0.00");
    }
}
