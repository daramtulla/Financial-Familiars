using UnityEngine;
using UnityEngine.UI;
public class MoneyCount : MonoBehaviour
{

    float money;
    public Text text;
    public JSONDatabaseOperations db;
    public GameObject storeLight;
    public GameObject lowMoneySpotlight;

    public GameObject noMoneyPanel;
    public GameObject gameOverPanel;

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
            text.color = new Color(1.0f, 0.5f, 0.0f);
            storeLight.SetActive(true);
            lowMoneySpotlight.SetActive(false);
        }
        else
        {
            if (!db.currentPlayer.completedTutorials.Contains("0Money")) {
                noMoneyPanel.SetActive(true);
                db.currentPlayer.completedTutorials.Add("0Money");
            }
            if (money < -5000.0f)
            {
                Time.timeScale = 0.0f;
                gameOverPanel.SetActive(true);
            }
            text.color = Color.red;
            storeLight.SetActive(false);
            lowMoneySpotlight.SetActive(true);
        }
        text.text = money.ToString("N2");
    }
}
