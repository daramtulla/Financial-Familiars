using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InventoryMenu inventoryMenu;
    public SceneManagerInstance sceneController;
    public PauseManager pauseManager;
    public BudgetMenu budgetMenu;
    public SuppliersMenu suppliersMenu;
    public PlayerManager playerManager;

    //End of Day Visuals
    public GameObject endScreen;
    public Text endDayTitle;
    public Text moneyMade;
    public Text loansPaid;
    public Text wagesPaid;
    public Text upgradeUpkeep;
    public Text netProfit;

    public void Start()
    {
    }

    public void endDay()
    {
        //Sell Items
        float moneyMadeAmount = inventoryMenu.sellItems();

        //Display End of Day
        endScreen.SetActive(true);
        endDayTitle.text = "Day " + playerManager.getDay().ToString() + " Results";
        //Money Made
        if(moneyMadeAmount > 0)
        {
            moneyMade.text = "$" + moneyMadeAmount.ToString();
            moneyMade.color = Color.green;
        }
        else if (moneyMadeAmount < 0)
        {
            moneyMade.text = "-$" + (-moneyMadeAmount).ToString();
            moneyMade.color = Color.red;
        }
        else
        {
            moneyMade.text = "$" + moneyMadeAmount.ToString();
            moneyMade.color = Color.gray;
        }
        //TODO: Add loan logic
        //For now: say it takes $100 to pay off every day
        loansPaid.color = Color.red;
        loansPaid.text = "-$100";

        //TODO: Add hiring mechanic
        //For now: set Wages paid to 0 for 0 employees
        wagesPaid.color = Color.gray;
        wagesPaid.text = "$0";

        //TODO: Add Upgrade Mechanics
        //For now: set Upgrades to 0 for no upgrades
        upgradeUpkeep.color = Color.gray;
        upgradeUpkeep.text = "$0";

        //TODO: Add Loan, Hiring, and Upgrades to current calculation
        float netProfitAmount = moneyMadeAmount - 100;
        if (netProfitAmount > 0)
        {
            netProfit.text = "$" + netProfitAmount.ToString();
            netProfit.color = Color.green;
        }
        else if (netProfitAmount < 0)
        {
            netProfit.text = "-$" + (-netProfitAmount).ToString();
            netProfit.color = Color.red;
        }
        else
        {
            netProfit.text = "$" + netProfitAmount.ToString();
            netProfit.color = Color.gray;
        }

        //TODO: Split money between savings and spending
        //For now: Send all money to one account
        playerManager.updatePlayerStats(netProfitAmount);
    }

    public void nextDay()
    {

    }
}