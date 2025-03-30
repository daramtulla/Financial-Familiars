using TMPro;
using UnityEngine;

public class LoansManager : MonoBehaviour
{
    [SerializeField] GameObject loansmenu;

    [SerializeField] TMP_InputField textInputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //loansmenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !textInputField.isFocused)
        {
            //ToggleMenu();
        }
    }

    /*
    public void ToggleMenu()
    {
        loansmenu.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }
    */
}
