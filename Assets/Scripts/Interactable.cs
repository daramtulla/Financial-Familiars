using UnityEngine;

public class Interactable : MonoBehaviour, Interact
{
    [SerializeField] GameObject menuToOpenOrClose;
    public void Interact()
    {
        Debug.Log("Interacting");

        //Insert functionality here

        //If the menu is open
        if(menuToOpenOrClose.activeInHierarchy)
        {
            //Close the menu
            menuToOpenOrClose.SetActive(false);
        }
        //Else the menu is closed
        else
        {
            //Open the menu
            menuToOpenOrClose.SetActive(true);
        }

    }


}
