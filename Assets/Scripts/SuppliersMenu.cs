using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography;
using System;
using System.Linq;
using static UnityEngine.ParticleSystem;

public class SuppliersMenu : MonoBehaviour
{
    public SoundManager soundManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject suppliersPanel;
    public Transform suppliersContent;
    public GameObject supplierItemPrefab;

    public JSONDatabaseOperations db;

    private int day;

    private RandomGenNum rnd;
    [SerializeField] InteractionManager interactionManager;

    [SerializeField] TMP_InputField textInputField;

    void Awake()
    {
        rnd = new RandomGenNum();

        day = db.currentPlayer.GetDay();
    }

    void Start()
    {
        suppliersPanel.SetActive(false);
    }

    void Update()
    {
        //press P to open purchasing of goods

        if (Input.GetKeyDown(KeyCode.P) && !textInputField.isFocused)
        {
            ToggleMenu();
        }

        //Generate new stock on day increase
        if (db.currentPlayer.GetDay() == day)
        {
            GenerateStock();
            day++;
        }
    }

    public void ToggleMenu()
    {
        suppliersPanel.SetActive(!suppliersPanel.activeSelf);
        if (suppliersPanel.activeSelf)
        {
            UpdateSuppliersUI();
        }
    }

    public void CloseMenu()
    {
        Debug.Log("Test Suppliers Menu Close");
        //The if statement prevents you from being frozen when you press P to open the suppliers menu
        //and then press the close button.
        //Also prevents "switchInteractState()" from being called twice when F is pressed
        //(once in the Update() function of InteractionManager.cs and the other time here).
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(SuppliersMenu): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        suppliersPanel.SetActive(false);
    }

    public void GenerateStock()
    {
        if (db.currentPlayer.suppliers.Count < 9)
        {
            Debug.LogWarning("Not enough suppliers to generate stock!");
            return;
        }

        // HIRE ID 8: Increases number of suppliers
        int numSuppliers = 7;
        if (db.CheckEmployee(9))
        {
            if (db.CheckEmployee(15))
            {
                numSuppliers = 8;
            }
            else
            {
                numSuppliers = 10;
            }
        }
        for (int i = 1; i < numSuppliers; i++)
        {
            int id1 = rnd.GetRandomMerchId();

            int id2 = rnd.GetRandomMerchId();

            while (id1 == id2)
            {
                id2 = rnd.GetRandomMerchId();
            }

            db.currentPlayer.suppliers[i - 1].stock1 = id1;
            db.currentPlayer.suppliers[i - 1].stock2 = id2;

            db.currentPlayer.suppliers[i - 1].cost1 = generateCost(id1, i);
            db.currentPlayer.suppliers[i - 1].cost2 = generateCost(id2, i);
        }
    }

    public float generateCost(int merchId, int storeID)
    {
        float bCost = db.currentPlayer.merch[merchId - 1].baseCost;

        //Randomly generate sale price modifer
        bCost *= (float)(1 - ((float)rnd.SalePriceModifier() / 10));

        if ((storeID == 1 && (merchId == 1 || merchId == 2 || merchId == 3))
        || (storeID == 2 && (merchId == 4 || merchId == 4 || merchId == 6))
        || (storeID == 3 && (merchId == 7 || merchId == 8 || merchId == 8))
        || (storeID == 4 && (merchId == 10 || merchId == 11 || merchId == 12))
        || (storeID == 8 && (merchId == 16 || merchId == 17 || merchId == 18))
        || (storeID == 9 && (merchId == 13 || merchId == 14 || merchId == 15))
        || (storeID == 5 && (merchId == 1 || merchId == 4 || merchId == 7 || merchId == 10))
        || (storeID == 6 && (merchId == 2 || merchId == 5 || merchId == 8 || merchId == 11))
        || (storeID == 7 && (merchId == 3 || merchId == 6 || merchId == 9 || merchId == 12)))
        {
            bCost *= .8f;
        }
        if (db.CheckEmployee(2))
        {
            if (db.CheckEmployee(15))
            {
                bCost *= .92f;
            }
            else
            {
                bCost *= .85f;
            }
        }
        return (float)Math.Round(bCost, 2);
    }

    public void UpdateSuppliersUI()
    {
        //Destroys prior entries in suppliers list
        foreach (Transform child in suppliersContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Supplier supplier in db.currentPlayer.suppliers)
        {
            if (supplier.stock1 == 0 || supplier.stock2 == 0)
            {
                continue;
            }
            GameObject newItem = Instantiate(supplierItemPrefab, suppliersContent);

            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = supplier.name;
            texts[1].text = db.currentPlayer.merch[supplier.stock1 - 1].name;
            texts[2].text = "$" + string.Format("{0:F2}", supplier.cost1);
            texts[3].text = db.currentPlayer.merch[supplier.stock2 - 1].name;
            texts[4].text = "$" + string.Format("{0:F2}", supplier.cost2);

            Button buy1Button1 = newItem.transform.Find("Buy1Button1").GetComponent<Button>();
            Button buy10Button1 = newItem.transform.Find("Buy10Button1").GetComponent<Button>();

            buy1Button1.onClick.AddListener(() => BuyItem(supplier.cost1, 1, supplier.stock1));
            buy10Button1.onClick.AddListener(() => BuyItem(supplier.cost1, 10, supplier.stock1));

            Button buy1Button2 = newItem.transform.Find("Buy1Button2").GetComponent<Button>();
            Button buy10Button2 = newItem.transform.Find("Buy10Button2").GetComponent<Button>();

            buy1Button2.onClick.AddListener(() => BuyItem(supplier.cost2, 1, supplier.stock2));
            buy10Button2.onClick.AddListener(() => BuyItem(supplier.cost2, 10, supplier.stock2));
        }
    }

    private void BuyItem(float cost, int bought, int id)
    {
        float total = cost * bought;

        if (db.CheckEmployee(2))
        {
            if (db.CheckEmployee(15))
            {
                total *= .92f;
            }
            else
            {
                total *= .85f;
            }
        }

        if (db.currentPlayer.currentMoney > total)
        {
            //HIRE ID 7: Possibility of losing items
            if (db.CheckEmployee(7) && new System.Random().Next(1, 100) >= 95)
            {
                // TODO: ADD FEEDBACK FOR THE ITEMS BEING LOST
                db.currentPlayer.currentMoney -= total;
            }
            else
            {
                db.currentPlayer.currentMoney -= total;
                db.currentPlayer.merch[id - 1].quantity += bought;
                db.currentPlayer.purchases += total;
            }

            if (bought == 1)
            {
                soundManager.soundAudioSource.PlayOneShot(soundManager.oneItemPurchase, 1.0f);
            }
            else
            {
                soundManager.soundAudioSource.PlayOneShot(soundManager.tenItemPurchase, 1.0f);
            }
        }
        else
        {
            soundManager.soundAudioSource.PlayOneShot(soundManager.itemPurchaseError, 0.2f);
        }
        db.SaveData();
    }
}
