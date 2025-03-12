using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class JSONDatabaseOperations : MonoBehaviour
{
    private string filePath;
    [SerializeField] Boolean debug;
    public Player currentPlayer;

    void Awake()
    {
        currentPlayer = new Player();
        filePath = Application.persistentDataPath + "/JSONDatabase.json";
        Debug.Log(filePath);

        //Check to see if JSON db is created
        if (!File.Exists(filePath))
        {
            currentPlayer.merch.Add(new Merchandise(1, "Crimson Restoration Potion", 0, 10, 0, .2f, 1, 1));
            currentPlayer.merch.Add(new Merchandise(2, "Bubbling Polymorph Flask", 0, 25, 0, .2f, 1, 2));
            currentPlayer.merch.Add(new Merchandise(3, "Draught of Living Death", 0, 65, 0, .2f, 1, 3));
            currentPlayer.merch.Add(new Merchandise(4, "Emerald Ring of Minor Protection", 0, 30, 0, .15f, 2, 1));
            currentPlayer.merch.Add(new Merchandise(5, "Necklace of Fire Resistance", 0, 70, 0, .15f, 2, 2));
            currentPlayer.merch.Add(new Merchandise(6, "Pocket Dimension Bag", 0, 220, 0, .15f, 2, 3));
            currentPlayer.merch.Add(new Merchandise(7, "Never-Dull Gold Dagger", 0, 150, 0, .10f, 3, 1));
            currentPlayer.merch.Add(new Merchandise(8, "Lich King Grimoire", 0, 320, 0, .10f, 3, 2));
            currentPlayer.merch.Add(new Merchandise(9, "Featherlight Warhammer", 0, 700, 0, .10f, 3, 3));
            currentPlayer.merch.Add(new Merchandise(10, "All-Seeing Crystal Ball", 0, 200, 0, .10f, 4, 1));
            currentPlayer.merch.Add(new Merchandise(11, "Wand Core Cluster", 0, 450, 0, .10f, 4, 2));
            currentPlayer.merch.Add(new Merchandise(12, "Pulsating Dragon Heart", 0, 1000, 0, .10f, 4, 3));

            currentPlayer.suppliers.Add(new Supplier(1, "Arcane Emporium", 0, 0, 0, 0));
            currentPlayer.suppliers.Add(new Supplier(2, "Mystic Merchants", 0, 0, 0, 0));
            currentPlayer.suppliers.Add(new Supplier(3, "Wand & Whimsy", 0, 0, 0, 0));
            currentPlayer.suppliers.Add(new Supplier(4, "Phoenix Feather Trading", 0, 0, 0, 0));
            currentPlayer.suppliers.Add(new Supplier(5, "Twilight Trinkets", 0, 0, 0, 0));
            currentPlayer.suppliers.Add(new Supplier(6, "Spellbound Supplies", 0, 0, 0, 0));
            currentPlayer.suppliers.Add(new Supplier(7, "Sigil & Sorcery", 0, 0, 0, 0));

            currentPlayer.loans.Add(new Loans("Loan 1", 0f, 0f));
            currentPlayer.loans.Add(new Loans("Loan 2", 0f, 0f));
            currentPlayer.loans.Add(new Loans("Loan 3", 0f, 0f));

            currentPlayer.moveSpeedModifier = 1;
            currentPlayer.currentMoney = 1000f;
            currentPlayer.volume = .5f;
            currentPlayer.ResetDay();
            currentPlayer.dailySales = 0;
            currentPlayer.newPlayer = new IntegerField(1);
            currentPlayer.active = new int[12];
            currentPlayer.totalSales = 0;
            currentPlayer.purchases = 0;

            SaveData();
        }

        LoadData();
    }

    public void SaveData()
    {
        filePath = Application.persistentDataPath + "/JSONDatabase.json";
        string JSONString = JsonUtility.ToJson(currentPlayer);
        System.IO.File.WriteAllText(filePath, JSONString);

        if (debug)
        {
            Debug.Log("Data Saved");
        }
    }

    public void LoadData()
    {
        filePath = Application.persistentDataPath + "/JSONDatabase.json";
        string JSONString = System.IO.File.ReadAllText(filePath);
        currentPlayer = JsonUtility.FromJson<Player>(JSONString);

        if (debug)
        {
            Debug.Log("Data Loaded");
        }
    }

    void Update()
    {
        //Hotkeys for saving and loading
        if (Input.GetKey(KeyCode.K))
        {
            SaveData();
        }

        if (Input.GetKey(KeyCode.L))
        {
            LoadData();
        }

        //For testing
        if (debug && Input.GetKey(KeyCode.V))
        {
            currentPlayer.currentMoney = 10000;

            for (int i = 0; i < 12; i++)
            {
                currentPlayer.merch[i].quantity = 10;
            }
        }
    }
}

[System.Serializable]
public class Player
{
    public float moveSpeedModifier;
    public float currentMoney;
    public float volume;

    //Day is private as increasing day needs to generate new supplier stock and reset daily sales
    [SerializeField] int dayCount;
    public float dailySales;
    public float purchases;

    public int[] active;

    public System.Object newPlayer;

    public List<Loans> loans = new List<Loans>();

    public List<Merchandise> merch = new List<Merchandise>();
    public List<Supplier> suppliers = new List<Supplier>();
    public float totalSales;
    public void ChangeQuantity(int id, int change)
    {
        if (id < 1 || id > 13)
        {
            Debug.Log("Invalid ItemId");
            return;
        }
        else
        {
            if (merch[id - 1].quantity + change > -1)
            {
                merch[id - 1].quantity += change;
            }
            else
            {
                Debug.Log("Not enough stock");
                return;
            }
        }

        return;
    }

    public void ChangeMarkup(int id, float newMarkup)
    {
        //TODO Add feedback for player
        if (id < 1 || id > 13)
        {
            Debug.Log("Invalid ItemId");
            return;
        }
        else
        {
            //limiting markup limits
            if (newMarkup > .5f || newMarkup < -.5f)
            {
                Debug.Log("Invalid ItemId");
                return;
            }
            else
            {
                merch[id].markupPercentage = newMarkup;
            }
        }
    }
    public void ResetInventory()
    {
        merch.Clear();

        merch.Add(new Merchandise(1, "Crimson Restoration Potion", 0, 10, 0, .2f, 1, 1));
        merch.Add(new Merchandise(2, "Bubbling Polymorph Flask", 0, 25, 0, .2f, 1, 2));
        merch.Add(new Merchandise(3, "Draught of Living Death", 0, 65, 0, .2f, 1, 3));
        merch.Add(new Merchandise(4, "Emerald Ring of Minor Protection", 0, 30, 0, .15f, 2, 1));
        merch.Add(new Merchandise(5, "Necklace of Fire Resistance", 0, 70, 0, .15f, 2, 2));
        merch.Add(new Merchandise(6, "Pocket Dimension Bag", 0, 220, 0, .15f, 2, 3));
        merch.Add(new Merchandise(7, "Never-Dull Gold Dagger", 0, 150, 0, .10f, 3, 1));
        merch.Add(new Merchandise(8, "Lich King Grimoire", 0, 320, 0, .10f, 3, 2));
        merch.Add(new Merchandise(9, "Featherlight Warhammer", 0, 700, 0, .10f, 3, 3));
        merch.Add(new Merchandise(10, "All-Seeing Crystal Ball", 0, 200, 0, .10f, 4, 1));
        merch.Add(new Merchandise(11, "Wand Core Cluster", 0, 450, 0, .10f, 4, 2));
        merch.Add(new Merchandise(12, "Pulsating Dragon Heart", 0, 1000, 0, .10f, 4, 3));
    }

    public void IncrDay()
    {
        dayCount++;
        //Daily sales already added to money total. Add to total sales counter
        totalSales += dailySales;
        dailySales = 0;

        //Add interest to loan
        AddDailyInterest(loans);
    }

    public int GetDay()
    {
        return dayCount;
    }

    public void ResetDay()
    {
        dayCount = 1;
    }

    public void AddDailyInterest(List<Loans> loans)
    {
        foreach (Loans l in loans)
        {
            l.amount += l.amount * l.interest;
        }
    }

    public void AddLoan(List<Loans> loans, float newAmount, float newInterest)
    {
        for (int i = 0; i < 3; i++)
        {
            if (loans[i].amount == 0f)
            {
                loans[i].amount = newAmount;
                loans[i].interest = newInterest;
                return;
            }
        }

        //TODO put user feedback to say max amount of loans has been reached
        Debug.Log("Already have 3 Loans");
    }

    public void PayLoan(int id, float pay)
    {
        loans[id].amount -= pay;
    }

    public int LoanCount()
    {
        int count = 0;

        foreach (Loans l in loans)
        {
            if (l.amount > 0)
            {
                count++;
            }
        }

        return count;
    }
}

[System.Serializable]
public class Merchandise
{
    public int id;
    public string name;
    public int quantity;
    public float baseCost;
    public float markupPercentage;
    public float customerMod;
    public int group;
    public int tier;


    public Merchandise(int id, string name, int quantity, float baseCost, float markupPercentage, float customerMod, int group, int tier)
    {
        this.id = id;
        this.name = name;
        this.quantity = quantity;
        this.baseCost = baseCost;
        this.markupPercentage = markupPercentage;
        this.customerMod = customerMod;
        this.group = group;
        this.tier = tier;
    }
}

[System.Serializable]
public class Supplier
{
    public int id;
    public string name;
    public int stock1;
    public float cost1;

    public int stock2;
    public float cost2;

    public Supplier(int id, string name, int stock1, int cost1, int stock2, int cost2)
    {
        this.id = id;
        this.name = name;
        this.stock1 = stock1;
        this.cost1 = cost1;
        this.stock2 = stock2;
        this.cost2 = cost2;
    }
}

//Max of three loans out at once
[System.Serializable]
public class Loans
{
    public string id;
    public float amount;
    public float interest;
    public Loans(string id, float amount, float interest)
    {
        this.id = id;
        this.amount = amount;
        this.interest = interest;
    }


}
