using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text moneyCount;

    public JSONDatabaseOperations db;

    int day;
    public Text dayCount;
    void Start()
    {
        float money = db.currentPlayer.currentMoney;
        moneyCount.text = "$" + money.ToString("0.##");
        if (money > 0)
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }
        else if (money < 0)
        {
            moneyCount.text = "-$" + (-money).ToString();
            moneyCount.color = Color.red;
        }
        else
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }

        day = db.currentPlayer.GetDay();
        dayCount.text = "Day " + day.ToString();

    }
    public void updatePlayerStats(float moneyMade)
    {
        //Increment day
        day += 1;
        db.currentPlayer.IncrDay();
        dayCount.text = "Day " + day.ToString();

        //Money Logic
        float money = moneyMade + db.currentPlayer.currentMoney;
        db.currentPlayer.currentMoney = money;

        moneyCount.text = "$" + money.ToString("0.##");
        if (money > 0)
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }
        else if (money < 0)
        {
            moneyCount.text = "-$" + (-money).ToString();
            moneyCount.color = Color.red;
        }
        else
        {
            moneyCount.color = new Color(1.0f, 0.5f, 0);
        }
    }

    public void ResetData()
    {
        //Intiale starting money is 1000 so player can purchase goods
        db.currentPlayer.currentMoney = 1000;
        float money = db.currentPlayer.currentMoney;
        moneyCount.text = Math.Round(money, 2).ToString();

        moneyCount.color = new Color(1.0f, 0.5f, 0);
        db.currentPlayer.ResetDay();
        dayCount.text = "Day 1";
        day = 1;

        //Clear inventory
        db.currentPlayer.ResetInventory();
    }
}
