using UnityEngine;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    public List<InventoryItem> InventoryItems;

    public void Initialize(List<InventoryItem> playerItems)
    {
        InventoryItems = playerItems;
    }
    void Start()
    {
        findInformation();
    }
    public CustomerManager(List<InventoryItem> inventoryItems)
    {
        InventoryItems = inventoryItems;
    }

    public void findInformation()
    {
        foreach(InventoryItem item in InventoryItems)
        {
            int demand = findDemand(item.demandCurveSlope, item.demandCurveIntercept, item.SellingPrice);
            int ItemsSold = demand * item.quantity;

            Debug.Log($"{item.itemName}: Base Price = ${item.baseCost}, Selling Price = ${item.SellingPrice}, Demand = {demand * 100:F1}%, Expected Sales = {ItemsSold}");
        }
    }

    int findDemand(float demandSlope, float demandIntercept, float sellingPrice)
    {
        //basic demand equation for now
        int numberSold = Mathf.FloorToInt((demandSlope * sellingPrice) + demandIntercept);
        return Mathf.Max(0, numberSold);
    }

    public float sellItems()
    {
        float moneyMade = 0;
        foreach (InventoryItem item in InventoryItems)
        {
            int numberSold = Mathf.Min(findDemand(item.demandCurveSlope, item.demandCurveIntercept, item.SellingPrice), item.quantity);
            item.quantity -= numberSold;
            moneyMade += numberSold * item.SellingPrice;
            Debug.Log($"Just sold {numberSold} of {item.itemName} at cost ${item.SellingPrice}");
        }
        return moneyMade;
    }
}
