using System;
using System.Collections.Generic;
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

            currentPlayer.unemployedEmployees.Add(new Employee(0, "Alex Carter", "Marketer", 100, "Analytic, dedicated", "Increases demand for all potions", "Marketing degree and 5+ years experience", "Employees/Marketer_Alex.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(1, "Riley Shaw", "Technician", 200, "Detail-oriented, problem solver", "Upgrades cost less", "Engineering Background and specialized in automation", "Employees/Technician_Riley.png"));

            currentPlayer.moveSpeedModifier = 1;
            currentPlayer.currentMoney = 1000f;
            currentPlayer.volume = .5f;
            currentPlayer.dayCount = 1;
            currentPlayer.dailySales = 0;
            currentPlayer.newPlayer = new IntegerField(1);

            SaveData();
        }

        LoadData();
    }

    public void SaveData()
    {
        filePath = Application.persistentDataPath + "/JSONDatabase.json";
        string JSONString = JsonUtility.ToJson(currentPlayer, true);
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
        //For debugging
        //When quiting from pause menu, call this function TODO
        if (debug && Input.GetKey(KeyCode.K))
        {
            SaveData();
        }

        //When click start on the title screen, call this function TODO
        if (debug && Input.GetKey(KeyCode.L))
        {
            LoadData();
        }
    }

    public void UpdateDailySales(float sales)
    {
        currentPlayer.dailySales = sales;
    }
}

[System.Serializable]
public class Player
{
    public float moveSpeedModifier;
    public float currentMoney;
    public float volume;

    public int dayCount;
    public float dailySales;

    public System.Object newPlayer;

    public List<Merchandise> merch = new List<Merchandise>();
    public List<Supplier> suppliers = new List<Supplier>();
    public List<Employee> unemployedEmployees = new List<Employee>();

    public int currentLoanAmount;
    public void changeQuantity(int id, int change)
    {
        if (id < 1 || id > 13)
        {
            Debug.Log("Invalid ItemId");
            return;
        }
        else
        {
            if (merch[id].quantity + change > -1)
            {
                merch[id].quantity += change;
            }
            else
            {
                Debug.Log("Not enough stock");
                return;
            }
        }

        return;
    }

    public void changeMarkup(int id, float newMarkup)
    {
        if (id < 1 || id > 13)
        {
            Debug.Log("Invalid ItemId");
            return;
        }
        else
        {
            //limiting markup limits
            if ((newMarkup > .5f || newMarkup < -.5f))
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
}

[System.Serializable]
public class Merchandise
{
    //Example values. TODO create all items in the game
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
[System.Serializable]
public class Employee
{
    public int id;
    public string name;
    public string position;
    public int salary;
    public string personality;
    public string benefits;
    public string qualifications;
    public string imageSource;


    public Employee(int id, string name, string position, int salary, string personality, string benefits, string qualifications, string imageSource)
    {
        this.id = id;
        this.name = name;
        this.position = position;
        this.salary = salary;
        this.personality = personality;
        this.benefits = benefits;
        this.qualifications = qualifications;
        this.imageSource = imageSource;
    }
}
