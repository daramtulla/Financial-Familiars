using UnityEngine;
using UnityEngine.UI;
public class MoneyCount : MonoBehaviour
{

    float money;
    public Text text;
    public JSONDatabaseOperations db;

    void Start()
    {
        money = db.currentPlayer.currentMoney;
        text.text = money.ToString();
    }

    void Update()
    {
        money = db.currentPlayer.currentMoney;
        text.text = money.ToString();
    }
    public void endDay()
    {
        //Range is set up like this due to it being minInclusive and maxExclusive, and we need to test that it's possible to see a trend upwards
        int itemsSold = 10;
        int price = Random.Range(0, 10);
        int upkeepCost = Random.Range(0, 40);
        int change = (itemsSold * price) - upkeepCost;
        money += change;
        Debug.Log("Items Sold: " + itemsSold + ", at price " + price + ", Upkeep Costs: " + upkeepCost + ". Net change: " + change);
        db.currentPlayer.currentMoney = money;
        text.text = money.ToString();
    }
}
