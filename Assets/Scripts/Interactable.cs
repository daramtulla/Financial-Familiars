using UnityEngine;

public class Interactable : MonoBehaviour, Interact
{
    [SerializeField] GameObject menuToOpenOrClose;
    [SerializeField] SuppliersMenu suppliersMenu;
    [SerializeField] BudgetMenu budgetMenu;
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
            else
            {
                //Open the menu
                menuToOpenOrClose.SetActive(true);
            }
        }

    }


}
