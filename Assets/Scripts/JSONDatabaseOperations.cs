using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class JSONDatabaseOperations : MonoBehaviour
{
    private string filePath;
    [SerializeField] Boolean debug;
    public Player currentPlayer;

    void Awake()
    {
        currentPlayer = new Player();
        filePath = Application.persistentDataPath + "/JSONDatabase.json";

        //TEST REMOVE WHEN DONE
        //currentPlayer.merch.Add(new Merchandise(1, "hi", 1, 1.1, 1.1, 1.1, 1.1));
    }

    private void SaveData()
    {
        string JSONString = JsonUtility.ToJson(currentPlayer);
        System.IO.File.WriteAllText(filePath, JSONString);

        if (debug)
        {
            Debug.Log("Data Saved");
        }
    }

    private void LoadData()
    {
        string JSONString = System.IO.File.ReadAllText(filePath);
        currentPlayer = JsonUtility.FromJson<Player>(JSONString);

        if (debug)
        {
            Debug.Log("Data Loaded");
        }
    }


    void Update()
    {
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

    public List<Merchandise> merch = new List<Merchandise>();

    //TODO: Do we want to save this to the player or save game?
    public int currentLoanAmount;

    //Implement skills TODO
}

[System.Serializable]
public class Merchandise
{
    //Example values. TODO create all items in the game
    public int id;
    public string name;
    public int quantity;
    public float cost;
    public float markupPercentage;
    public float slope;
    public float intercept;

    /*
        public Merchandise(int id, string name, int quantity, float cost, float markupPercentage, float slope, float intercept)
        {
            this.id = id;

        }
    */
}