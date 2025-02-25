using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    float money;
    public Text moneyCount;

    int day;
    public Text dayCount;
    void Start()
    {
        money = PlayerPrefs.GetFloat("amount");
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

        day = PlayerPrefs.GetInt("day");
        dayCount.text = "Day " + day.ToString();

    }
    public void updatePlayerStats(float moneyMade)
    {
        //Increment day
        day += 1;
        PlayerPrefs.SetInt("day", day);
        dayCount.text = "Day " + day.ToString();

        //Money Logic
        money += moneyMade;
        PlayerPrefs.SetFloat("amount", money);
        PlayerPrefs.Save();

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
        PlayerPrefs.SetInt("day", 1);
        PlayerPrefs.SetFloat("amount", 0);
        PlayerPrefs.Save();
    }

    public int getDay()
    {
        return day;
    }
}
