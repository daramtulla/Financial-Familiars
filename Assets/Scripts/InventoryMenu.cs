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
        
    void Start()
    {
        inventoryPanel.SetActive(false);
        AddTestItems();
        UpdateInventoryUI();
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
        inventory.Add(new InventoryItem(1, "Clear Crystal Orb", 10, 20.00f, 50));
        inventory.Add(new InventoryItem(2, "Walnut Wand", 100, 10.00f, 25));
        inventory.Add(new InventoryItem(3, "Vial of Acid", 1, 25.00f, 100));
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


}
