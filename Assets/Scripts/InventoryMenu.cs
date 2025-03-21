using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Linq;
using System;
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
}
