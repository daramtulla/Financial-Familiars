using System.Data.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

    //Pause Menu Logic
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }
    public void pauseGame()
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

    public void endDay()
    {
        Time.timeScale = 0.0f;

        //Sell Items
        inventoryMenu.SellItems();
        float moneyMadeAmount = db.currentPlayer.dailySales;

        //Display End of Day
        endScreen.SetActive(true);
        endDayTitle.text = "Day " + db.currentPlayer.dayCount + " Results";

        

        //TODO: Add loan logic
        //For now: say it takes $50 to pay off every day
        float mandatoryLoansAmount = -50.0f;


        float wagesPaidAmount = 0.0f;
        foreach(var employee in db.currentPlayer.employees)
        {
            wagesPaidAmount -= employee.salary;
        }


        //TODO: Discuss if we're keeping upgrade upkeeps
        //For now: set Upgrades to 0 for no upgrades
        float upgradeUpkeepAmount = 0.0f;

        float utilitiesCostAmount = -50.0f;

        if(db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 0)) {
            utilitiesCostAmount *= 0.9f;
        }

        //TODO: Add multiple employees. For now, just use the ID
        //TODO: Add rent? Or lump it all in utilities. for now, just lump it in with utilities
        if (db.currentPlayer.employees.Any(employee => employee.id == 3))
        {
            utilitiesCostAmount *= 0.9f;
        }


        //Money Made
        formatText(moneyMade, moneyMadeAmount);
        formatText(loansPaid, mandatoryLoansAmount);
        formatText(wagesPaid, wagesPaidAmount);
        formatText(upgradeUpkeep, upgradeUpkeepAmount);
        formatText(utilitiesCost, utilitiesCostAmount); 


        float netProfitAmount = moneyMadeAmount - -mandatoryLoansAmount - -wagesPaidAmount - -upgradeUpkeepAmount - -utilitiesCostAmount;
        formatText(netProfit, netProfitAmount);

        //TODO: Split money between savings and spending
        //For now: Send all money to one account
        playerManager.updatePlayerStats(netProfitAmount);
    }
    public void formatText(Text textObject, float amount)
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
}