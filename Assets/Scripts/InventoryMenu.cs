using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
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
        //AddTestItems();
        LoadInventoryFromCSV();
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

    private void LoadInventoryFromCSV()
    {
        List<InventoryItem> allItems = CSVLoader.LoadItemsFromCSV("PlayerInventory.csv");
        inventory = allItems.FindAll(item => item.quantity > 0);

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

    public void UpdateInventoryUI()
    {
        // Clear existing UI items
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }

        //Only stuff in stock
        List<InventoryItem> inStockItems = inventory.FindAll(item => item.quantity > 0);

        // Populate the inventory UI
        foreach (InventoryItem item in inStockItems)
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
    public void BuyItem(InventoryItem item, int quantity)
    {
        //Check if item exists
        InventoryItem existingItem = inventory.Find(i => i.idNumber == item.idNumber);

        //if so
        if (existingItem != null)
        {
            Debug.Log($"Just bought existing item {item.itemName}");
            existingItem.quantity += quantity;
        }
        else
        {
            Debug.Log($"Just bought new item {item.itemName}");
            InventoryItem newItem = new InventoryItem(item.idNumber, item.itemName, quantity, item.baseCost, item.markup, item.demandCurveSlope, item.demandCurveIntercept);
            inventory.Add(newItem);
        }
        Debug.Log($"Bought {quantity} of: {item.itemName}");
        SaveInventoryToCSV();
        UpdateInventoryUI();
    }
    public void SaveInventoryToCSV()
    {
        //find the player inventory
        string filePath = Path.Combine(Application.streamingAssetsPath, "PlayerInventory.csv");
        List<string> lines = new List<string>();
        //add header
        lines.Add("ID,Name,Quantity,BaseCost,Markup,Slope,Intercept");

        //get player inventory, add it to CSV
        foreach (InventoryItem item in inventory)
        {
            Debug.Log($"Just added {item.idNumber},{item.itemName},{item.quantity},{item.baseCost},{item.markup},{item.demandCurveSlope},{item.demandCurveIntercept}");
            lines.Add($"{item.idNumber},{item.itemName},{item.quantity},{item.baseCost},{item.markup},{item.demandCurveSlope},{item.demandCurveIntercept}");
        }
        //Clear inventory csv
        File.WriteAllText(filePath, string.Empty);
        //rewrite it
        File.WriteAllLines(filePath, lines);

        Debug.Log($"Inventory saved to CSV: {filePath}");
    }

    public void ReloadInventory()
    {
        Debug.Log("Reloading Inventory...");
        inventory.Clear();

        inventory = CSVLoader.LoadItemsFromCSV("PlayerInventory.csv");

        UpdateInventoryUI();

        Canvas.ForceUpdateCanvases();
        inventoryPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        Canvas.ForceUpdateCanvases();

        Debug.Log("Inventory Reloaded and UI Updated.");
    }
    int findDemand(float demandSlope, float demandIntercept, float sellingPrice)
    {
        //basic demand equation for now
        int numberSold = Mathf.FloorToInt((demandSlope * sellingPrice) + demandIntercept);
        return Mathf.Max(0, numberSold);
    }

    public float sellItems()
    {
        float moneyMade = 0;
        foreach (InventoryItem item in inventory)
        {
            int numberSold = Mathf.Min(findDemand(item.demandCurveSlope, item.demandCurveIntercept, item.SellingPrice), item.quantity);
            item.quantity -= numberSold;
            moneyMade += numberSold * item.SellingPrice;
            Debug.Log($"Just sold {numberSold} of {item.itemName} at cost ${item.SellingPrice}");
        }
        return moneyMade;
    }
}
