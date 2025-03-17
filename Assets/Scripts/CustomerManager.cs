using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Data;
using UnityEngine.InputSystem.Controls;
using System.Collections;
using NUnit.Framework;
using System.Linq;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] JSONDatabaseOperations db;

    [SerializeField] Boolean debug;

    [SerializeField] RandomGenNum rnd;

    [SerializeField] int dayTimeTotal;

    //Item id first, then sale time
    private List<KeyValuePair<int, int>> timeline;

    private float dayTime;

    [SerializeField] Boolean timerActive;

    private int day;

    private float pastDayTime;

    public void StartDay()
    {
        dayTime = 0;
        timerActive = true;
        timeline = GetItemSaleTimeLine();
    }

    //Perform customer sales during day phase
    public IEnumerator CustomerSales()
    {
        //Can't remove while foreach is iterating so make a deep copy
        List<KeyValuePair<int, int>> copy = timeline.Select(pair => new KeyValuePair<int, int>(pair.Key, pair.Value)).ToList();

        while (dayTime < dayTimeTotal)
        {
            dayTime += Time.deltaTime;

            if (dayTime >= pastDayTime + 1)
            {
                pastDayTime = dayTime;

                if (debug) { Debug.Log("Checking sale time at " + dayTime); }

                //Check to see what sales occured at this time
                SaleTimeCheck(copy);
            }

            yield return null;
        }
    }

    public void SaleTimeCheck(List<KeyValuePair<int, int>> copy)
    {
        foreach (KeyValuePair<int, int> pair in timeline)
        {
            Boolean contains = false;
            if (pair.Value <= dayTime)
            {
                foreach (KeyValuePair<int, int> copyPair in copy)
                {
                    //Debug.Log("copy key" + copyPair.Key + ", copy value" + copyPair.Value);
                    if (copyPair.Value.Equals(pair.Value) && copyPair.Key.Equals(pair.Key))
                    {
                        contains = true;
                    }
                }

                if (contains)
                {
                    AttemptSale(pair.Key);
                    if (debug) { Debug.Log("Attempting to sell merch " + pair.Value); }
                    KeyValuePair<int, int> toRemove = new(pair.Key, pair.Value);
                    Debug.Log("Remove Successful? " + copy.Remove(toRemove));
                    contains = false;
                }
            }
        }
    }

    //Generates a number of sales based on the markup of the item and when those sales occur
    public List<KeyValuePair<int, int>> GetItemSaleTimeLine()
    {
        List<Merchandise> merch = db.currentPlayer.merch;
        List<KeyValuePair<int, int>> timeline = new();

        for (int i = 0; i < 18; i++)
        {
            float markupDeviation = merch[i].markupPercentage * merch[i].customerMod;
            int saleLimit = (int)Math.Round(markupDeviation);

            if (saleLimit > 3) { saleLimit = 3; }

            int sales = rnd.GetDailyItemSaleNumber(0, 4 - saleLimit);

            //Generate sale time for item
            for (int j = 0; j < sales; j++)
            {
                KeyValuePair<int, int> saleTime = new(i + 1, rnd.GetRandomSaleTime(dayTimeTotal));

                if (debug)
                {
                    Debug.Log("Pair added: " + saleTime.Key + ", " + saleTime.Value);
                }

                timeline.Add(saleTime);
            }

        }

        return timeline;
    }

    //For Testing
    public void Update()
    {
        //For testing
        if (debug && Input.GetKeyDown(KeyCode.J))
        {
            for (int i = 0; i < 18; i++)
            {
                AttemptSale(i + 1);
            }
        }

        //For testing
        if (debug && Input.GetKeyDown(KeyCode.H))
        {
            StartDay();
        }

        if (timerActive)
        {
            StartCoroutine(CustomerSales());
            timerActive = false;
        }
    }

    public void Awake()
    {
        day = db.currentPlayer.GetDay();
        rnd = new();
        timerActive = false;
        pastDayTime = -1;
    }


    //Item must be displayed to be sold
    public void AttemptSale(int id)
    {
        if (db.currentPlayer.active[id - 1] == 1)
        {
            SellItem(id);
        }
    }

    public void SellItem(int id)
    {
        float baseCost = db.currentPlayer.merch[id - 1].baseCost;
        float markup = db.currentPlayer.merch[id - 1].markupPercentage;

        //TODO Upgrades that sell multiple items at a time
        db.currentPlayer.ChangeQuantity(id, -1);
        db.currentPlayer.currentMoney += baseCost * (1 + markup);
        db.currentPlayer.dailySales += baseCost * (1 + markup);

        db.currentPlayer.active[id - 1] = 0;
    }





}
