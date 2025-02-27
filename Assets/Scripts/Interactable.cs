using UnityEngine;

public class Interactable : MonoBehaviour, Interact
{
    [SerializeField] GameObject menuToOpenOrClose;
    [SerializeField] SuppliersMenu suppliersMenu;
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
            else
            {
                //Open the menu
                menuToOpenOrClose.SetActive(true);
            }
        }

    }


}
