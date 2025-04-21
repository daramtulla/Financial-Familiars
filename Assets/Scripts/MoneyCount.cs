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
        text.text = money.ToString("N2");
    }

    void Update()
    {
        money = db.currentPlayer.currentMoney;
        text.text = money.ToString("N2");
    }
}
