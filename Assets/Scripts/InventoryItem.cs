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

    public float SellingPrice => baseCost + (baseCost * markup / 100f);
    public float Profit => SellingPrice - baseCost;

    public InventoryItem(int id, string name, int qty, float cost, float markupPercentage,  float demandCurveSlope, float demandCurveIntercept)
    {
        idNumber = id;
        itemName = name;
        quantity = qty;
        baseCost = cost;
        markup = markupPercentage;
        this.demandCurveSlope = demandCurveSlope;
        this.demandCurveIntercept = demandCurveIntercept;
    }

    public int getSupply()
    {
        return quantity;
    }
}
