using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InventoryMenu inventoryMenu;
    public SceneManagerInstance sceneController;
    public PauseManager pauseManager;
    public BudgetMenu budgetMenu;
    public SuppliersMenu suppliersMenu;
    public PlayerManager playerManager;

    public void Start()
    {
    }

    public void endDay()
    {
        //Sell Items
        float moneyMade = inventoryMenu.sellItems();

        //TODO: Split money between savings and spending

        //For now: Send all money to one account
        playerManager.endDayStats(moneyMade);

    }

    public void nextDay()
    {

    }
}