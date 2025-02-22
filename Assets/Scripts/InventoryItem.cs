using UnityEngine;

//This is the class for the stuff in the inventory

//This is important for class: https://discussions.unity.com/t/can-someone-explain-what-system-serializable-does-im-confused/791567
[System.Serializable]
public class InventoryItem
{
    public int idNumber;
    public string itemName;
    public int quantity;
    public float baseCost;
    public float markup;

    public float demandCurveSlope;
    public float demandCurveIntercept;

    public float SellingPrice;
    public float Profit;

    public string storeName = "unknown";

    public InventoryItem(int id, string name, int qty, float cost, float markupPercentage,  float demandCurveSlope, float demandCurveIntercept)
    {
        idNumber = id;
        itemName = name;
        quantity = qty;
        baseCost = cost;
        markup = markupPercentage;
        this.demandCurveSlope = demandCurveSlope;
        this.demandCurveIntercept = demandCurveIntercept;
        SellingPrice = cost + (cost * markupPercentage / 100f);
        Profit = (cost * markupPercentage / 100f);
    }

    public int getSupply()
    {
        return quantity;

    }

    public void increaseQuantity(int added){
        quantity = quantity + added;
    }

    public void changeStore(string store){
        storeName = store;
    }

    public void changeMarkup(int percent){
        markup = percent;
        SellingPrice = baseCost * (1 + percent / 100f);

        Debug.Log($"{itemName} now has a {markup}% markup, selling for ${SellingPrice.ToString("0.00")}");
    }
}
