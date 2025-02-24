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
        moneyCount.text = money.ToString("0.##");
        day = PlayerPrefs.GetInt("day");
        dayCount.text = "Day " + day.ToString();

    }
    public void endDayStats(float moneyMade)
    {
        //Increment day
        day += 1;
        PlayerPrefs.SetInt("day", day);
        dayCount.text = "Day " + day.ToString();

        //Money Logic
        money += moneyMade;
        PlayerPrefs.SetFloat("amount", money);
        PlayerPrefs.Save();
        moneyCount.text = money.ToString();
    }

    public void resetData()
    {
        moneyCount.text = "0000";
        dayCount.text = "Day 1";
        money = 0;
        day = 1;
        PlayerPrefs.SetInt("day", 1);
        PlayerPrefs.SetFloat("amount", 0);
        PlayerPrefs.Save();
    }
}
