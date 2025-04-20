using UnityEngine;
using UnityEngine.UI;
public class MoneyCount : MonoBehaviour
{

    float money;
    public Text text;
    public JSONDatabaseOperations db;
    public GameObject storeLight;
    public GameObject lowMoneySpotlight;

    void Start()
    {
        money = db.currentPlayer.currentMoney;
        text.text = money.ToString("N2");
    }

    void Update()
    {
        money = db.currentPlayer.currentMoney;
        if(money >= 0)
        {
            storeLight.SetActive(true);
            lowMoneySpotlight.SetActive(false);
        }
        else
        {
            storeLight.SetActive(false);
            lowMoneySpotlight.SetActive(true);
        }
        text.text = money.ToString("N2");
    }
    public void endDay()
    {
        //Range is set up like this due to it being minInclusive and maxExclusive, and we need to test that it's possible to see a trend upwards
        int itemsSold = 10;
        int price = 0;
        int upkeepCost = 0;
        int change = (itemsSold * price) - upkeepCost;
        money += change;
        Debug.Log("Items Sold: " + itemsSold + ", at price " + price + ", Upkeep Costs: " + upkeepCost + ". Net change: " + change);
        db.currentPlayer.currentMoney = money;
        text.text = string.Format("{0:F2}", money);
    }
}
