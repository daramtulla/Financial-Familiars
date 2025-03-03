using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    float money;
    public Text moneyCount;

    public JSONDatabaseOperations db;

    int day;
    public Text dayCount;
    void Start()
    {
        money = db.currentPlayer.currentMoney;
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

        day = db.currentPlayer.dayCount;
        dayCount.text = "Day " + day.ToString();

    }
    public void updatePlayerStats(float moneyMade)
    {
        //Increment day
        day += 1;
        db.currentPlayer.dayCount = day;
        dayCount.text = "Day " + day.ToString();

        //Money Logic
        money += moneyMade;
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

    public void resetData()
    {
        moneyCount.text = "$0000";
        moneyCount.color = new Color(1.0f, 0.5f, 0);
        dayCount.text = "Day 1";
        money = 0;
        day = 1;
        db.currentPlayer.currentMoney = money;
        day = db.currentPlayer.dayCount;
    }
}
