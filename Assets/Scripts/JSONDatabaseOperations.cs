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

    [SerializeField] Boolean RegenerateOnLoad;

    void Awake()
    {
        currentPlayer = new Player();
        filePath = Application.persistentDataPath + "/JSONDatabase.json";
        Debug.Log(filePath);

        //Check to see if JSON db is created
        if (!File.Exists(filePath) || RegenerateOnLoad)
        {
            generateDatabase();
            SaveData();
        }

        LoadData();
    }

    public void generateDatabase()
    {
        if(File.Exists(Application.persistentDataPath + "/JSONDatabase.json"))
        {
            File.Delete(Application.persistentDataPath + "/JSONDatabase.json");
        }
        //Merchandise
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

        currentPlayer.merch.Add(new Merchandise(13, "Minor Rune of Healing", 0, 15, 0, .20f, 5, 1));
        currentPlayer.merch.Add(new Merchandise(14, "Major Rune of Damage", 0, 40, 0, .20f, 5, 2));
        currentPlayer.merch.Add(new Merchandise(15, "Omega Rune of Protection", 0, 95, 0, .20f, 5, 3));

        currentPlayer.merch.Add(new Merchandise(16, "Ironwood Shield", 0, 110, 0, .15f, 6, 1));
        currentPlayer.merch.Add(new Merchandise(17, "Darksteel Shield", 0, 280, 0, .15f, 6, 2));
        currentPlayer.merch.Add(new Merchandise(18, "Dragon Scale Shield", 0, 390, 0, .15f, 6, 3));

        //Suppliers
        currentPlayer.suppliers.Add(new Supplier(1, "Arcane Emporium", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(2, "Mystic Merchants", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(3, "Wand & Whimsy", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(4, "Phoenix Feather Trading", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(5, "Twilight Trinkets", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(6, "Spellbound Supplies", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(7, "Sigil & Sorcery", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(8, "Dwarven Magics", 0, 0, 0, 0));
        currentPlayer.suppliers.Add(new Supplier(9, "Witch's Circle", 0, 0, 0, 0));

        //Loans
        currentPlayer.loans.Add(new Loans("Loan 1", 0f, 0f));
        currentPlayer.loans.Add(new Loans("Loan 2", 0f, 0f));
        currentPlayer.loans.Add(new Loans("Loan 3", 0f, 0f));

        //TODO: Tweak daily wages
        currentPlayer.unemployedEmployees.Add(new Employee(0, "Fizzwick Flash", "Marketer", 80, "Fast-talking, dramatic, obsessed with catchy slogans.", "Increases demand for all items.", "Self-Employed for 10 years, made a steady living for themselves, in the Merchant's Guild", "Employees/Fizzwick.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(1, "Briza Coppercrank", "Technician", 90, "Meticulous, resourceful, loves tinkering with everything.", "Reduces the cost of all upgrades.", "Former airship mechanic, certified in arcane engineering, once rebuilt a golem using only scrap parts.", "Employees/Briza.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(2, "Grumlek Stonesnout", "Supplier", 85, "Gruff, well-connected, always knows a guy who knows a guy.", "Reduces the cost of buying items by negotiating better deals.", "Former caravan trader, has contacts in every market.", "Employees/Grumlek.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(3, "Dorin Dullard", "Idea Maker", 50, "Enthusiastic, always brainstorming, never executing.", "Spends time talking about ideas.", "Has 'Revolutionary' business insights, but has never held a real job", "Employees/Dorin.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(4, "Velora Swiftwhisper", "Rent Negotiator", 95, "Charming, silver-tongued, can talk anyone into a better deal.", "Reduces the cost of rent.", "Former noble house emissary, expert in contracts, once convinced an ogre to pay her for toll fees.", "Employees/Velora.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(5, "Lazlo Snooze", "Productivity Expert", 40, "Always tired and drinking coffee, grumpy", "Slows down all work", "Started writing a book on efficiency, but never finished reading it", "Employees/Lazlo.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(6, "Quillbert Ledgersnout", "Taxman", 100, "Nerdy, precise, loves finding loopholes.", "Reduces the cost of taxes.", "Former royal accountant, memorized entire tax laws, once deducted a wizard's beard as a business expense.", "Employees/Quillbert.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(7, "Bertie Blunder", "Logistics Coordinator", 60, "Eager, but niave and clueless", "Has a chance to lose shipments or send them to the wrong locations", "Formal postal worker, was fired for misdelivering royal mail.", "Employees/Bertie.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(8, "Velma Redtape", "Administrator", 55, "Strict, loves paperwork, enforces pointless rules.", "Increases price of upgrades due to extra paperwork", "Worked in the royal archives, once rejected a request becaues the ink was the wrong color", "Employees/Velma.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(9, "Marlo Wickert", "Business Contact", 95, "Well-dressed, well-mannered, and always knows the right people.", "Adds more items to the shipping cauldron.", "Former trade broker, once arranged a deal between rival witch covens.", "Employees/Marlo.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(10, "Clodwick Stumble", "Security Expert", 45, "Paranoid, clumsy, sets off his own traps.", "Occasionally locks employees out of the building.", "Former castle guard, once arrested himself on accident", "Employees/Clodwick.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(11, "Mina Gossip", "Public Relations", 50, "Knows everyone's business and shares it freely.", "Has a chance to lower demand by spreading embarrasing company secrets.", "Former town crier, fired for sharing scandalous rumors.", "Employees/Mina.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(12, "Evalis Keeneye", "Appraiser", 90, "Sharp-eyed, analytical, and detail-oriented.", "Reveals the optimal prices for items to maximize profits.", "Former auction house expert and trained in magical artifact valuation.", "Employees/Evalis.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(13, "Todd Tinkerfall", "R&D Specialist", 70, "Overly ambitious, but most experiments explode.", "Has a chance to destroy an upgrade, rather than purchasing it.", "Once designed a 'self-cleaning' rug that destroyed itself.", "Employees/Todd.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(14, "Talia Greenbloom", "Loan Assistant", 85, "Friendly, organized, and always ready to help.", "Lowers interest rates on loans.", "Former banker, has saved people 1,000,000 gold over his life.", "Employees/Talia.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(15, "Gilda Grumble", "HR Manager", 65, "Loves mediating, even when there's no conflict.", "Employees spend more time in meetings, lowering productivity", "Former peacekeeper for a goblin clan, retired due to stress.", "Employees/Gilda.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(16, "Zara Flaskbrew", "Alchemist", 110, "Curious, experimental, and always smells like chemicals.", "Greatly increases demand for potions.", "Self-employed for 100 years, master of alchemical mixtures.", "Employees/Zara.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(17, "Jaxson Goldleaf", "Designer", 100, "Charming, stylish, perfectionist.", "Greatly increases demand for accessories.", "Former fashion designer for high society, known for turning simple items into coveted pieces.", "Employees/Jaxson.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(18, "Borin Brightchant", "Motivational Speaker", 50, "Loud, overly enthusiastic, uses a lot of buzzwords", "Attempts to boost morale in mandatory meetings.", "Claims to have inspired famous heroes, but nobody has commented on this", "Employees/Borin.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(19, "Grunkar Ironfist", "Blacksmith", 120, "Strong, no-nonsense, reclusive.", "Greatly increases demand for weapons.", "A seasoned blacksmith with a reputation for crafting legendary weapons.", "Employees/Grunkar.png"));
        currentPlayer.unemployedEmployees.Add(new Employee(20, "Selene Starwhisper", "Grand Enchanter", 150, "Mysterious, graceful, always floating.", "Greatly increases demand for special items.", "A powerful enchanter who once created an invisibility cloak.", "Employees/Selene.png"));

        //TODO: Add upgrades
        //TODO: Add functionality
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(0, "Everburning Candles", 200, "Reduces utilities cost."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(1, "Luxury Branding", 500, "Slightly Increases demand for all items."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(2, "Premium Potions", 300, "Increases Demand for Potions."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(3, "Premium Accessories", 600, "Increases Demand for Accessories."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(4, "Premium Weapons", 600, "Increases Demand for Weapons."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(5, "Premium Special Items", 1000, "Increases demand for Special items."));

        //TODO: Add storage functionality
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(6, "Bigger Boxes", 400, "Increases max storage capacity."));
        //TODO: Add restocking
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(7, "Magic Hand I", 750, "Automatically restocks potions."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(8, "Magic Hand II", 1250, "Automatically restocks accessories."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(9, "Magic Hand III", 1750, "Automatically restocks weapons."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(10, "Magic Hand IV", 2250, "Automatically restocks Special items."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(11, "Matching Bling", 450, "Customers may buy two accessories."));
        currentPlayer.unpurchasedUpgrades.Add(new Upgrade(12, "Wealthy Patrons", 2000, "Increases Demand for expensive items."));

        //Player Stats
        currentPlayer.moveSpeedModifier = 1;
        currentPlayer.currentMoney = 1000f;
        currentPlayer.volume = .5f;
        currentPlayer.ResetDay();
        currentPlayer.dailySales = 0;
        currentPlayer.newPlayer = new IntegerField(1);
        currentPlayer.active = new int[18];
        currentPlayer.totalSales = 0;
        currentPlayer.purchases = 0;
        currentPlayer.cycleNum = 0;
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
        //Hotkeys for saving and loading. For testing
        if (debug && Input.GetKey(KeyCode.K))
        {
            SaveData();
        }

        if (debug && Input.GetKey(KeyCode.L))
        {
            LoadData();
        }

        //For testing. Gives Player full inventory and money
        if (debug && Input.GetKey(KeyCode.V))
        {
            currentPlayer.currentMoney = 10000;

            for (int i = 0; i < 18; i++)
            {
                currentPlayer.merch[i].quantity = 10;
                currentPlayer.active[i] = 1;
            }
        }
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        currentPlayer.upgrades.Add(upgrade);
    }
    public void addEmployee(Employee employee)
    {
        currentPlayer.employees.Add(employee);
    }
}

[System.Serializable]
public class Player
{
    public float moveSpeedModifier;
    public float currentMoney;
    public float volume;
    //0 = setup, 1 = selling, 2 = close
    public int cycleNum;
    public int currentLoanAmount;
    //Day is private as increasing day needs to generate new supplier stock and reset daily sales
    private int dayCount;
    public float dailySales;
    public float purchases;
    public float totalSales;

    //To determine what displays should be active
    public int[] active;

    //For determing if the character was just created
    public System.Object newPlayer;

    //Sub objects of player
    public List<Loans> loans = new List<Loans>();
    public List<Merchandise> merch = new List<Merchandise>();
    public List<Supplier> suppliers = new List<Supplier>();
    public List<Employee> unemployedEmployees = new List<Employee>();
    public List<Employee> employees = new List<Employee>();
    public List<Upgrade> unpurchasedUpgrades = new List<Upgrade>();
    public List<Upgrade> upgrades = new List<Upgrade>();

    //Merchandise Method Helpers
    public void ChangeQuantity(int id, int change)
    {
        if (id < 1 || id > 19)
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
            // UPGRADE ID 6, ADD MORE STORAGE (maybe 10 to 20 each)
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
        if (id < 1 || id > 19)
        {
            Debug.Log("Invalid ItemId");
            return;
        }
        else
        {
            //limiting markup limits
            if (newMarkup > .5f || newMarkup < -.5f)
            {
                Debug.Log("Invalid Markup");
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

        merch.Add(new Merchandise(13, "Minor Rune of Healing", 0, 15, 0, .20f, 5, 1));
        merch.Add(new Merchandise(14, "Major Rune of Damage", 0, 40, 0, .20f, 5, 2));
        merch.Add(new Merchandise(15, "Omega Rune of Protection", 0, 95, 0, .20f, 5, 3));

        merch.Add(new Merchandise(16, "Ironwood Shield", 0, 110, 0, .15f, 6, 1));
        merch.Add(new Merchandise(17, "Darksteel Shield", 0, 280, 0, .15f, 6, 2));
        merch.Add(new Merchandise(18, "Dragon Scale Shield", 0, 390, 0, .15f, 6, 3));
    }

    //Day Method Helpers

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

    //Loan Method Helpers
    // HIRE 13: LOWER INTEREST RATE ON LOANS
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

[System.Serializable]
public class Upgrade
{
    public int id;
    public string Name;
    public int Cost;
    public string Description;

    public Upgrade(int this_id, string name, int cost, string description)
    {
        id = this_id;
        Name = name;
        Cost = cost;
        Description = description;
    }
}

