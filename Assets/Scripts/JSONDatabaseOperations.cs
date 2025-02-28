using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering;

public class JSONDatabaseOperations : MonoBehaviour
{
    private string filePath;
    [SerializeField] Boolean debug;
    public Player currentPlayer;

    public static JSONDatabaseOperations Instance; // Singleton to ensure one instance exists

    void Awake()
    {
        // Singleton to share across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentPlayer = new Player();
        filePath = Application.persistentDataPath + "/JSONDatabase.json";

        //Preintailize items in list to 0 inventory and markup. Change on load.
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
        currentPlayer.merch.Add(new Merchandise(11, "Wand Core Cluster", 0, 200, 0, .10f, 4, 2));
        currentPlayer.merch.Add(new Merchandise(12, "Pulsating Dragon Heart", 0, 200, 0, .10f, 4, 3));

        LoadData();
    }

    public void SaveData()
    {
        string JSONString = JsonUtility.ToJson(currentPlayer);
        System.IO.File.WriteAllText(filePath, JSONString);

        if (debug)
        {
            Debug.Log("Data Saved");
        }
    }

    public void LoadData()
    {
        string JSONString = System.IO.File.ReadAllText(filePath);
        currentPlayer = JsonUtility.FromJson<Player>(JSONString);

        if (debug)
        {
            Debug.Log("Data Loaded");
        }
    }

    public float LoadMainMenuData()
    {
        LoadData();
        return currentPlayer.volume;
    }

    public void SaveMainMenuData(float newVolume)
    {
        currentPlayer.volume = newVolume;
        SaveData();
        return;
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
}

[System.Serializable]
public class Player
{
    public float moveSpeedModifier;
    public int currentMoney;
    public float volume;

    public List<Merchandise> merch = new List<Merchandise>();
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