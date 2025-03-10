using UnityEngine;

public class Interactable : MonoBehaviour, Interact
{
    [SerializeField] GameObject menuToOpenOrClose;
    [SerializeField] SuppliersMenu suppliersMenu;
    [SerializeField] BudgetMenu budgetMenu;
    [SerializeField] InventoryMenu inventoryMenu;
    [SerializeField] GameManager gameManager;
    [SerializeField] ShowMenu showMenu;
    [SerializeField] LoansMenu loansMenu;

    public void Interact()
    {
        Debug.Log("Interacting");

        //Insert functionality here

        //If the menu is open
        if(menuToOpenOrClose.activeInHierarchy)
        {
            if(menuToOpenOrClose.name == "Suppliers Panel")
            {
                Debug.Log("Closed Suppliers Menu");
                suppliersMenu.CloseMenu();
            }
            else if (menuToOpenOrClose.name == "Budget Panel")
            {
                Debug.Log("Close Budget Menu");
                budgetMenu.CloseMenu();
            }
            else if (menuToOpenOrClose.name == "Inventory Panel")
            {
                Debug.Log("Close Inventory Menu");
                inventoryMenu.CloseMenu();
            }
            else if (menuToOpenOrClose.name == "EndDayScreen")
            {
                Debug.Log("Close End Day Screen");
                showMenu.ShowElement(true); //I'm assuming true for ShowElement means "yes" to unpause
            }
            else if (menuToOpenOrClose.name == "Loans Menu")
            {
                Debug.Log("Close Loans Menu");
                loansMenu.CloseThis();
            }
            else
            {
                //Close the menu
                menuToOpenOrClose.SetActive(false);
            }
        }
        //Else the menu is closed
        else
        {
            if (menuToOpenOrClose.name == "Suppliers Panel")
            {
                Debug.Log("Open Suppliers Menu");
                suppliersMenu.ToggleMenu();
            }
            else if (menuToOpenOrClose.name == "Budget Panel")
            {
                Debug.Log("Open Budget Menu");
                budgetMenu.ToggleBudgetMenu();
            }
            else if (menuToOpenOrClose.name == "Inventory Panel")
            {
                Debug.Log("Open Inventory Panel");
                inventoryMenu.ToggleMenu();
            }
            else if (menuToOpenOrClose.name == "EndDayScreen")
            {
                Debug.Log("Open End Day Screen");
                gameManager.endDay();
            }
            else if (menuToOpenOrClose.name == "Loans Menu")
            {
                Debug.Log("Open Loans Menu");
                loansMenu.OpenThis();
            }
            else if (menuToOpenOrClose.name == "HiringUI")
            {
                menuToOpenOrClose.SetActive(true);
            }
            else if (menuToOpenOrClose.name == "UpgradeUI")
            {
                menuToOpenOrClose.SetActive(true);
            }
            else
            {
                //Open the menu
                menuToOpenOrClose.SetActive(true);
            }
        }
    }
}