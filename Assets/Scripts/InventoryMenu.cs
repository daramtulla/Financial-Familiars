using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class InventoryMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inventoryPanel;

    //cmon scrollbar!
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;

    //all the stock
    private List<InventoryItem> inventory = new List<InventoryItem>();

    //For selling items
    private CustomerManager customerManager;
        
    void Start()
    {
        inventoryPanel.SetActive(false);
        AddTestItems();
        UpdateInventoryUI();
        customerManager = new CustomerManager(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        //press I to open inventory
        if(Input.GetKeyDown(KeyCode.I)){
            ToggleMenu();
        }
    }

    public void ToggleMenu(){
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    public void CloseMenu(){
        inventoryPanel.SetActive(false);

    }

    private void AddTestItems()
    {
        inventory.Add(new InventoryItem(1, "Clear Crystal Orb", 10, 20.00f, 50, 3.0f, -5.0f));
        inventory.Add(new InventoryItem(2, "Walnut Wand", 100, 10.00f, 10, 2.0f, 5.0f));
        inventory.Add(new InventoryItem(3, "Vial of Acid", 1, 25.00f, 100, 1.2f, -2.0f));
        inventory.Add(new InventoryItem(4, "Duplicating Rings", 1000, 10.00f, 0, 0.5f, 100.0f));
    }

    private void UpdateInventoryUI()
    {
        // Clear existing UI items
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }

        // Populate the inventory UI
        foreach (InventoryItem item in inventory)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContent);
            
            // Find TextMeshPro components directly in the prefab
            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.idNumber.ToString();
            texts[1].text = item.itemName;
            texts[2].text = item.quantity.ToString();
            texts[3].text = "$" + item.baseCost.ToString("0.00");
            texts[4].text = item.markup.ToString("0") + "%";
            texts[5].text = "$" + item.SellingPrice.ToString("0.00");
            texts[6].text = "$" + item.Profit.ToString("0.00");
        }
    }

    //test function
    public void simulateSales()
    {
        //don't need to see the money made, but good for debugging
        customerManager.findInformation();
        float moneyMade = customerManager.sellItems();

        UpdateInventoryUI();
    }
}
