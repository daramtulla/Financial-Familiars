using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text moneyCount;

    public JSONDatabaseOperations db;

    private int day;
    public Text dayCount;
    public SceneController sceneController;
    void Start()
    {
        float money = db.currentPlayer.currentMoney;
        moneyCount.text = "$" + money.ToString("N2");
        if (money > 0)
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }
        else if (money < 0)
        {
            moneyCount.text = "-$" + (-money).ToString("N2");
            moneyCount.color = Color.red;
        }
        else
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }

        day = db.currentPlayer.GetDay();
        dayCount.text = "Day " + day.ToString();

    }
    public void UpdatePlayerStats(float moneyMade)
    {
        day = db.currentPlayer.GetDay();
        dayCount.text = "Day " + day.ToString();

        //Money Logic
        float money = db.currentPlayer.currentMoney;

        moneyCount.text = "$" + money.ToString("N2");
        if (money >= 0)
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }
        else if (money < 0)
        {
            moneyCount.text = "-$" + (-money).ToString("N2");
            moneyCount.color = Color.red;
        }
        else
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }
    }

    
    public void ResetData()
    {
        moneyCount.color = new Color(1.0f, 0.5f, 0);
        db.currentPlayer.ResetDay();
        dayCount.text = "Day 1";
        day = 1;

        //Intialize starting money is 10000 to match starting money
        db.currentPlayer.currentMoney = 10000;
        moneyCount.text = Math.Round(db.currentPlayer.currentMoney, 2).ToString();
        //db.currentPlayer.dayCount = day;
        moneyCount.text = db.currentPlayer.currentMoney.ToString("N2");

        //Clear inventory
        db.currentPlayer.ResetInventory();
        //Recreate Database
        db.generateDatabase();

        //Clear items on tables
        for (int i = 0; i < db.currentPlayer.active.Length; i++)
        {
            db.currentPlayer.active[i] = 0;
        }
        db.SaveData();
        sceneController.OnClickSceneChangeTitle();
    }
    
}
