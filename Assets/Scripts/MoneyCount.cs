using UnityEngine;
using UnityEngine.UI;
public class MoneyCount : MonoBehaviour {

    int money;
    public Text text;

    void Start()
    {
        money = PlayerPrefs.GetInt("amount");
        text.text = money.ToString();
    }
    public void endDay()
    {
        //Rnage is set up like this due to it being minInclusive and maxExclusive, and we need to test that it's possible to see a trend upwards
        money += Random.Range(-19, 21);
        Debug.Log("PRESSED");
        PlayerPrefs.SetInt("amount", money);
        PlayerPrefs.Save();
        text.text = money.ToString();
    }
}
