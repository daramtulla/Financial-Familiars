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


            //TODO: Tweak daily wages
            currentPlayer.unemployedEmployees.Add(new Employee(0, "Fizzwick Flash", "Marketer", 80, "Fast-talking, dramatic, obsessed with catchy slogans.", "Increases demand for all items.", "Self-Employed for 10 years, made a steady living for themselves, in the Merchant’s Guild", "Employees/Fizzwick.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(1, "Briza Coppercrank", "Technician", 90, "Meticulous, resourceful, loves tinkering with everything.", "Reduces the cost of all upgrades.", "Former airship mechanic, certified in arcane engineering, once rebuilt a golem using only scrap parts.", "Employees/Briza.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(2, "Grumlek Stonesnout", "Supplier", 85, "Gruff, well-connected, always knows a guy who knows a guy.", "Reduces the cost of buying items by negotiating better deals.", "Former caravan trader, has contacts in every market.", "Employees/Grumlek.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(3, "Velora Swiftwhisper", "Rent Negotiator", 95, "Charming, silver-tongued, can talk anyone into a better deal.", "Reduces the cost of rent.", "Former noble house emissary, expert in contracts, once convinced an ogre to pay her for toll fees.", "Employees/Velora.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(4, "Quillbert Ledgersnout", "Taxman", 100, "Nerdy, precise, loves finding loopholes.", "Reduces the cost of taxes.", "Former royal accountant, memorized entire tax laws, once deducted a wizard’s beard as a business expense.", "Employees/Quillbert.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(5, "Marlo Wickert", "Business Contact", 95, "Well-dressed, well-mannered, and always knows the right people.", "Adds more items to the shipping cauldron.", "Former trade broker, once arranged a deal between rival witch covens.", "Employees/Marlo.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(6, "Evalis Keeneye", "Appraiser", 90, "Sharp-eyed, analytical, and detail-oriented.", "Reveals the optimal prices for items to maximize profits.", "Former auction house expert and trained in magical artifact valuation.", "Employees/Evalis.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(7, "Talia Greenbloom", "Loan Assistant", 85, "Friendly, organized, and always ready to help.", "Lowers interest rates on loans.", "Former banker, has saved people 1,000,000 gold over his life.", "Employees/Talia.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(8, "Zara Flaskbrew", "Alchemist", 110, "Curious, experimental, and always smells like chemicals.", "Greatly increases demand for potions.", "Self-employed for 100 years, master of alchemical mixtures.", "Employees/Zara.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(9, "Jaxson Goldleaf", "Designer", 100, "Charming, stylish, perfectionist.", "Greatly increases demand for accessories.", "Former fashion designer for high society, known for turning simple items into coveted pieces.", "Employees/Jaxson.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(10, "Grunkar Ironfist", "Blacksmith", 120, "Strong, no-nonsense, reclusive.", "Greatly increases demand for weapons.", "A seasoned blacksmith with a reputation for crafting legendary weapons.", "Employees/Grunkar.png"));
            currentPlayer.unemployedEmployees.Add(new Employee(11, "Selene Starwhisper", "Grand Enchanter", 150, "Mysterious, graceful, always floating.", "Greatly increases demand for special items.", "A powerful enchanter who once created an invisibility cloak.", "Employees/Selene.png"));


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
