using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Linq;
public class InventoryMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inventoryPanel;
    [SerializeField] InteractionManager interactionManager;

    //cmon scrollbar!
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;

    //For selling items
    private CustomerManager customerManager;

    public JSONDatabaseOperations db;

    [SerializeField] TMP_InputField textInputField;

    void Start()
    {
        inventoryPanel.SetActive(false);
        //AddTestItems();
        UpdateInventoryUI();

    }

    // Update is called once per frame
    void Update()
    {
        //press I to open inventory
        if (Input.GetKeyDown(KeyCode.I) && !textInputField.isFocused)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    public void CloseMenu()
    {
        Debug.Log("Test Inventory Menu Close");
        //The if statement prevents you from being frozen when you press I to open the inventory menu
        //and then press the close button.
        //Also prevents "switchInteractState()" from being called twice when F is pressed
        //(once in the Update() function of InteractionManager.cs and the other time here).
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(InventoryMenu): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        inventoryPanel.SetActive(false);
    }

    public void UpdateInventoryUI()
    {
        // Clear existing UI items
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }

        List<Merchandise> inventory = db.currentPlayer.merch;

        // Populate the inventory UI
        foreach (Merchandise item in inventory)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContent);

            float sellingPrice = item.baseCost * item.markupPercentage;

            // Find TextMeshPro components directly in the prefab
            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.name;
            texts[1].text = item.quantity.ToString();
            texts[2].text = "$" + item.baseCost.ToString("N2");
            texts[3].text = item.markupPercentage.ToString() + "%";
            texts[4].text = item.group.ToString();
            texts[5].text = item.tier.ToString();
        }
    }

    public void ReloadInventory()
    {
        Debug.Log("Reloading Inventory...");

        UpdateInventoryUI();
        Canvas.ForceUpdateCanvases();
        inventoryPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        Canvas.ForceUpdateCanvases();

        Debug.Log("Inventory Reloaded and UI Updated.");
    }

    //TODO: MAKE SURE THIS IS WORKING RIGHT
    //BUT IT HAS NO REFERENCE TO THE INVENTORY ITSELF, IT DOESN"T REMOVE

    //WE NEED THIS TO WORK
    public void SellItems()
    {
        foreach (Merchandise merch in db.currentPlayer.merch)
        {
            float degrees = merch.markupPercentage / merch.customerMod;

            switch (merch.group)
            {
                //Potions
                case 1:
                    if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 2))
                    {
                        degrees *= 0.9f;
                    }
                    if (db.currentPlayer.employees.Any(employee => employee.id == 8))
                    {
                        degrees *= 0.85f;
                    }
                    break;
                //accessories
                case 2:
                    if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 3))
                    {
                        degrees *= 0.9f;
                    }
                    if (db.currentPlayer.employees.Any(employee => employee.id == 9))
                    {
                        degrees *= 0.85f;
                    }
                    break;
                case 3:
                    if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 4))
                    {
                        degrees *= 0.9f;
                    }
                    if (db.currentPlayer.employees.Any(employee => employee.id == 10))
                    {
                        degrees *= 0.85f;
                    }
                    break;
                case 4:
                    if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 5))
                    {
                        degrees *= 0.9f;
                    }
                    if (db.currentPlayer.employees.Any(employee => employee.id == 11))
                    {
                        degrees *= 0.85f;
                    }
                    break;
                default:
                    Debug.Log("Item does not belong to a group");
                    break;
            }
            //general upgrades
            if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 1))
            {
                degrees *= 0.95f;
            }
            if (db.currentPlayer.employees.Any(employee => employee.id == 0))
            {
                degrees *= 0.95f;
            }
            //TODO: ADD UPGRADE 11, but this might require a rework of this system

            if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 12 && merch.baseCost >= 450))
            {
                degrees *= 0.9f;
            }
            //100 is a place holder value for now
            float sales = merch.baseCost * merch.markupPercentage * (100 - (100 * merch.customerMod * degrees));

            db.currentPlayer.currentMoney += sales;
            db.currentPlayer.dailySales += sales;
        }
    }
}
