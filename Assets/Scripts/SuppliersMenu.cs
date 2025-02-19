using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
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
        supplierItems = CSVLoader.LoadItemsFromCSV("InventoryItems.csv");
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

        foreach (InventoryItem item in supplierItems)
        {
            GameObject newItem = Instantiate(supplierItemPrefab, suppliersContent);

            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = "Store A";
            texts[1].text = item.itemName;
            texts[2].text = "$" + (item.baseCost).ToString("0.00");

            Button buy10Button = newItem.transform.Find("Buy10Button").GetComponent<Button>();
            Button buy100Button = newItem.transform.Find("Buy100Button").GetComponent<Button>();

            buy10Button.onClick.AddListener(() => BuyItem(item, 10));
            buy100Button.onClick.AddListener(() => BuyItem(item, 100));
        }
    }

    private void BuyItem(InventoryItem item, int quantity)
    {
        Debug.Log($"Bought {quantity} of: {item.itemName}");
    }


}
