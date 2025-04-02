using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour, InteractMenu
{
    [SerializeField] GameObject tutorialPopup;
    [SerializeField] string tutorialKey;
    [SerializeField] Button closeButton;

    [SerializeField] GameObject menuToOpenOrClose;
    [SerializeField] SuppliersMenu suppliersMenu;
    [SerializeField] BudgetMenu budgetMenu;
    [SerializeField] InventoryMenu inventoryMenu;
    [SerializeField] GameManager gameManager;
    [SerializeField] ShowMenu showMenu;
    [SerializeField] LoansMenu loansMenu;
    [SerializeField] Borrowing borrowMenu;
    [SerializeField] EmployeeManager employeeManager;

    public Text interactableNameText;
    public string interactableNameTextString;

    [SerializeField] JSONDatabaseOperations db;
    [SerializeField] InteractionManager im;


    public void InteractMenu()
    {
        Debug.Log("Interacting with " + menuToOpenOrClose.name);

        //Check if the tutorial has been shown
        /*
         * if (PlayerPrefs.GetInt(tutorialKey, 0) == 0)
        {
            ShowTutorial();
        }
        else
        {
            ShowWindow();
        }
         */
        if (!db.currentPlayer.completedTutorials.Contains(tutorialKey))
        {
            ShowTutorial();
        }
        else
        {
            ShowWindow();
        }

        if (!string.IsNullOrEmpty(tutorialKey))
        {
            FindObjectOfType<TutorialPlaybyPlay>()?.NotifyTutorialObjectUsed(tutorialKey);
        }
        
    }


    void ShowWindow()
    {
        //If the menu is open
        if (menuToOpenOrClose.activeInHierarchy)
        {
            if (menuToOpenOrClose.name == "Suppliers Panel")
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
            else if (menuToOpenOrClose.name == "LoansMenu")
            {
                Debug.Log("Close Loans Menu");
                loansMenu.CloseThis();
            }
            else if (menuToOpenOrClose.name == "Borrowing Lectern")
            {
                Debug.Log("Close Borrowing Menu");
                borrowMenu.CloseMenu();
            }
            else if (menuToOpenOrClose.name == "HiringUI")
            {
                //todo
                employeeManager.ToggleMenu();
            }
            else if (menuToOpenOrClose.name == "UpgradeUI")
            {
                //todo
                menuToOpenOrClose.SetActive(true);
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

                if (db.currentPlayer.cycleNum == 0)
                {
                    gameManager.StartSellingPhase();
                    im.SwitchInteractState();
                }
                else if (db.currentPlayer.cycleNum == 2)
                {
                    gameManager.RestartCycle();
                }
            }
            else if (menuToOpenOrClose.name == "LoansMenu")
            {
                Debug.Log("Open Loans Menu");
                loansMenu.OpenThis();
            }
            else if (menuToOpenOrClose.name == "Borrowing Lectern")
            {
                Debug.Log("Close Borrowing Menu");
                borrowMenu.ToggleMenu();
            }
            else if (menuToOpenOrClose.name == "HiringUI")
            {
                //todo
                employeeManager.ToggleMenu();
            }
            else if (menuToOpenOrClose.name == "UpgradeUI")
            {
                //todo
                menuToOpenOrClose.SetActive(true);
            }
            else
            {
                //Open the menu
                menuToOpenOrClose.SetActive(true);
            }
        }
    }
    void ShowTutorial()
    {
        tutorialPopup.SetActive(true);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseTutorial);
    }
    void CloseTutorial()
    {
        db.currentPlayer.completedTutorials.Add(tutorialKey);
        db.SaveData();
        /*
         * PlayerPrefs.SetInt(tutorialKey, 1);
         * PlayerPrefs.Save();
         */

        InteractionManager im = (InteractionManager)FindFirstObjectByType(typeof(InteractionManager));
        im.SwitchInteractState();

        tutorialPopup.SetActive(false);
    }
}
