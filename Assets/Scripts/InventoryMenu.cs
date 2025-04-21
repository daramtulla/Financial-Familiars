using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Linq;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
public class InventoryMenu : MonoBehaviour
{
    public SoundManager soundManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inventoryPanel;
    [SerializeField] InteractionManager interactionManager;

    //cmon scrollbar!
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;

    public JSONDatabaseOperations db;

    [SerializeField] TMP_InputField textInputField;

    float[] dBoxOptions = { 10f, 25f, 50f };

    [SerializeField] int[] lastDBoxValue = new int[18];

    void Start()
    {
        inventoryPanel.SetActive(false);
        //AddTestItems();
        UpdateInventoryUI();

    }

    void MarkupChange(int index, int merchID)
    {
        if (lastDBoxValue[merchID - 1] != index)
        {
            db.currentPlayer.merch[merchID - 1].markupPercentage = dBoxOptions[index];
            soundManager.ButtonClickSound();
        }
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

            TMP_Dropdown dBox = newItem.GetComponentInChildren<TMP_Dropdown>();

            if (dBox == null)
            {
                Debug.Log("Could not find dropdown");
            }

            //Needed up here also to prevent button click sound from replaying on menu open
            if (item.markupPercentage == 10f)
            {
                dBox.value = 0;
                lastDBoxValue[item.id - 1] = 0;
            }
            else if (item.markupPercentage == 25f)
            {
                dBox.value = 1;
                lastDBoxValue[item.id - 1] = 1;
            }
            else if (item.markupPercentage == 50f)
            {
                dBox.value = 2;
                lastDBoxValue[item.id - 1] = 2;
            }

            dBox.onValueChanged.AddListener((int index) => MarkupChange(index, item.id));

            float sellingPrice = item.baseCost * item.markupPercentage;

            // Find TextMeshPro components directly in the prefab
            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.name;
            texts[1].text = item.quantity.ToString();
            texts[2].text = "$" + item.baseCost.ToString("N2");
            texts[3].text = item.markupPercentage.ToString() + "%";
            texts[4].text = item.group.ToString();
            texts[5].text = item.tier.ToString();

            if (item.markupPercentage == 10f)
            {
                dBox.value = 0;
                lastDBoxValue[item.id - 1] = 0;
            }
            else if (item.markupPercentage == 25f)
            {
                dBox.value = 1;
                lastDBoxValue[item.id - 1] = 1;
            }
            else if (item.markupPercentage == 50f)
            {
                dBox.value = 2;
                lastDBoxValue[item.id - 1] = 2;
            }
        }
    }

    //Not being used currently. May be needed in future
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
