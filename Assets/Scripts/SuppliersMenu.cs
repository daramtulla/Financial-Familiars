using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
public class SuppliersMenu: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject suppliersPanel;
    public Transform suppliersContent;
    public GameObject supplierItemPrefab;

    private List<InventoryItem> supplierItems = new List<InventoryItem>();

    void Start()
    {
        suppliersPanel.SetActive(false);
        LoadSuppliersFromCSV();
        UpdateSuppliersUI();
    }

    void Update()
    {
        //press P to open purchasing of goods
        if(Input.GetKeyDown(KeyCode.P)){
            ToggleMenu();
        }
    }

    private void LoadSuppliersFromCSV()
    {
        List<InventoryItem> allItems = CSVLoader.LoadItemsFromCSV("InventoryItems.csv");
        supplierItems = Filter(allItems);

    }

    public void ToggleMenu(){
        suppliersPanel.SetActive(!suppliersPanel.activeSelf);
        if (suppliersPanel.activeSelf)
        {
            UpdateSuppliersUI();
        }
    }

    public void CloseMenu(){
        suppliersPanel.SetActive(false);

    }

    private void UpdateSuppliersUI()
    {
        foreach (Transform child in suppliersContent)
        {
            Destroy(child.gameObject);
        }

        List<InventoryItem> filteredItems = Filter(supplierItems);

        foreach (InventoryItem item in supplierItems)
        {
            GameObject newItem = Instantiate(supplierItemPrefab, suppliersContent);

            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.storeName;
            texts[1].text = item.itemName;
            texts[2].text = "$" + (item.SellingPrice).ToString("0.00");

            Button buy10Button = newItem.transform.Find("Buy10Button").GetComponent<Button>();
            Button buy100Button = newItem.transform.Find("Buy100Button").GetComponent<Button>();

            buy10Button.onClick.AddListener(() => BuyItem(item, 10));
            buy100Button.onClick.AddListener(() => BuyItem(item, 100));
        }
    }

    private void BuyItem(InventoryItem item, int quantity)
    {
        item.increaseQuantity(quantity);
        Debug.Log($"Bought {quantity} of: {item.itemName}");

        SaveInventoryToCSV();

    }

    private void SaveInventoryToCSV()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "InventoryItems.csv");
        List<string> lines = new List<string>();

        lines.Add("ID,Name,Quantity,BaseCost,Markup");

        foreach (InventoryItem item in supplierItems)
        {
            lines.Add($"{item.idNumber},{item.itemName},{item.quantity},{item.baseCost},{item.markup}");
        }
        File.WriteAllLines(filePath, lines);

        Debug.Log($"Inventory saved to CSV: {filePath}");
    }

    //Who is selling what for what?
    private List<InventoryItem> Filter(List<InventoryItem> items)
    {
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        System.Random rand = new System.Random();

        
        //exactly 3 stores
        string[] stores = { "Store A", "Store B", "Store C" };

        foreach (string store in stores)
        {

            //1-3 items per store
            int loopCount = rand.Next(1, 4);
            for (int i = 0; i < loopCount; i++){
                int randomIndex = rand.Next(0, items.Count);
                InventoryItem selectedItem = items[randomIndex];

                //label store
                selectedItem.changeStore(store);

                //Store-Specific Markup
                if (store == "Store A")
                {
                    selectedItem.changeMarkup(rand.Next(0, 16));
                }
                else if (store == "Store B")
                {
                    //like rolling 1d4
                    int randomChance = rand.Next(1, 5);
                    if (randomChance == 1)
                    {
                        selectedItem.changeMarkup(-10);
                    } else if (randomChance == 2)
                    {
                        selectedItem.changeMarkup(5);
                    } else if (randomChance == 3)
                    {
                        selectedItem.changeMarkup(20);
                    } else{
                        selectedItem.changeMarkup(60);
                    }
                }
                else if (store == "Store C")
                {
                    selectedItem.changeMarkup(rand.Next(0, 100));
                }

                //put on list
                filteredItems.Add(selectedItem);
            }

        }

        Debug.Log($"Filtered {items.Count} items down to {filteredItems.Count} items.");
        return filteredItems;
    }

}
