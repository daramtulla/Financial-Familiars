using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BudgetMenu budgetMenu;
    public SuppliersMenu suppliersMenu;
    public PlayerManager playerManager;
    public JSONDatabaseOperations db;

    //End of Day Visuals
    public GameObject endScreen;
    public Text endDayTitle;
    public Text moneyMade;
    public Text loansPaid;
    public Text wagesPaid;
    public Text upgradeUpkeep;
    public Text netProfit;

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
        float moneyMadeAmount = db.currentPlayer.dailySales;

        //Display End of Day
        endScreen.SetActive(true);
        endDayTitle.text = "Day " + db.currentPlayer.GetDay() + " Results";

        //Money Made
        formatText(moneyMade, moneyMadeAmount);

        //TODO: Add loan logic
        //For now: say it takes $100 to pay off every day
        float mandatoryLoansAmount = -100.0f;
        formatText(loansPaid, mandatoryLoansAmount);

        //TODO: Add hiring mechanic
        //For now: set Wages paid to 0 for 0 employees
        float wagesPaidAmount = 0.0f;
        formatText(wagesPaid, wagesPaidAmount);

        //TODO: Add Upgrade Mechanics
        //For now: set Upgrades to 0 for no upgrades
        float upgradeUpkeepAmount = 0.0f;
        formatText(upgradeUpkeep, upgradeUpkeepAmount);


        float netProfitAmount = moneyMadeAmount - -mandatoryLoansAmount - -wagesPaidAmount - -upgradeUpkeepAmount;
        formatText(netProfit, netProfitAmount);

        //TODO: Split money between savings and spending
        //For now: Send all money to one account
        playerManager.updatePlayerStats(netProfitAmount);
    }
    public void formatText(Text textObject, float amount)
    {
        if (amount > 0)
        {
            textObject.text = "$" + amount.ToString();
            textObject.color = Color.green;
        }
        else if (amount < 0)
        {
            textObject.text = "-$" + (-amount).ToString();
            textObject.color = Color.red;
        }
        else
        {
            textObject.text = "$" + amount.ToString();
            textObject.color = Color.gray;
        }
    }
}