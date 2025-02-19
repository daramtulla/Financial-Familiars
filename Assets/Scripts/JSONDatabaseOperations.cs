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
        if (debug && Input.GetKey(KeyCode.K))
        {
            SaveData();
        }

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

public class Merchandise
{
    //Example values. TODO create all items in the game
    public decimal value;
    public string name;
    public string description;
}