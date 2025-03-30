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
    public SoundManager soundManager;
    [SerializeField] JSONDatabaseOperations db;

    [SerializeField] Boolean debug;

    [SerializeField] RandomGenNum rnd;

    public int dayTimeTotal;

    //Item id first, then sale time
    private List<KeyValuePair<int, int>> timeline;

    //Item id, then customer time
    private List<KeyValuePair<int, int>> entrance;

    private float dayTime;

    [SerializeField] Boolean timerActive;

    private int day;

    private float pastDayTime;

    [SerializeField] CustomerMovement cm;

    // HIRE 0, 15, 16, 18, 19: Affects demand/prices
    // UPGRADE 1, 2, 3, 4, 5, 11, 12: Affects dmeand/prices
    public void StartSelling()
    {
        dayTime = 0;
        pastDayTime = 0;
        timerActive = true;
        timeline = GetItemSaleTimeLine();
        entrance = GenerateCustomerEntrance();
    }

    //Perform customer sales during day phase
    public IEnumerator CustomerSales()
    {
        //Can't remove while foreach is iterating so make a deep copy
        List<KeyValuePair<int, int>> copy1 = timeline.Select(pair => new KeyValuePair<int, int>(pair.Key, pair.Value)).ToList();
        List<KeyValuePair<int, int>> copy2 = entrance.Select(pair => new KeyValuePair<int, int>(pair.Key, pair.Value)).ToList();

        while (dayTime < dayTimeTotal)
        {
            dayTime += Time.deltaTime;

            if (dayTime >= pastDayTime + 1)
            {
                pastDayTime = dayTime;

                if (debug) { Debug.Log("Checking sale time at " + dayTime); }

                //Check to see what sales occured at this time
                SaleTimeCheck(copy1);
                CustomerTimeCheck(copy2);
            }

            yield return null;
        }

        //After timer is up
        db.currentPlayer.cycleNum = 2;
        //db.SaveData();
        //Debug.Log($"db.currentPlayer.cycleNum: {db.currentPlayer.cycleNum}");
        timerActive = false;
        soundManager.soundAudioSource.PlayOneShot(soundManager.storeClosing, 0.3f);
    }

    public void CustomerTimeCheck(List<KeyValuePair<int, int>> copy)
    {

        foreach (KeyValuePair<int, int> pair in entrance)
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
                    cm.CreateCustomer(pair.Key);
                    if (debug) { Debug.Log("Attempting to sell merch " + pair.Value); }
                    KeyValuePair<int, int> toRemove = new(pair.Key, pair.Value);
                    Debug.Log("Remove Successful? " + copy.Remove(toRemove));
                }
            }
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

    public List<KeyValuePair<int, int>> GenerateCustomerEntrance()
    {
        List<KeyValuePair<int, int>> entrance = new();

        //Generate sale time for item
        for (int j = 0; j < (dayTimeTotal - 10) / 3; j++)
        {
            KeyValuePair<int, int> customerTime = new(rnd.GetRandomMerchId(), rnd.GetRandomCustomerEntrance(dayTimeTotal));

            if (debug)
            {
                Debug.Log("Pair added: " + customerTime.Key + ", " + customerTime.Value);
            }

            entrance.Add(customerTime);
        }

        return entrance;
    }


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

        //For Testing. Auto ends sale period
        if (debug && Input.GetKeyDown(KeyCode.X))
        {
            dayTime = dayTimeTotal;
        }

        //Debug.Log($"TimerActive: {timerActive}");
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
            soundManager.soundAudioSource.PlayOneShot(soundManager.itemSold, 1.25f);
        }
    }

    //HIRES AND UPGRADES
    //TODO: CHECK THIS SELLING EQUATION
    //UPGRADE 11: possibly sell two accessories at once
    public void SellItem(int id)
    {
        float baseCost = db.currentPlayer.merch[id - 1].baseCost;
        float markup = db.currentPlayer.merch[id - 1].markupPercentage;
        float customerMod = db.currentPlayer.merch[id - 1].customerMod;
        float degrees = markup / customerMod;

        //From Zach M's other selling function
        switch (db.currentPlayer.merch[id - 1].group)
        {
            //Potions
            case 1:
                if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 2))
                {
                    degrees *= 0.9f;
                }
                if (db.currentPlayer.employees.Any(employee => employee.id == 15))
                {
                    degrees *= 0.85f;
                }
                break;
            //accessories
            case 2:
                if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 3))
                {
                    degrees *= 0.9f;
                }
                if (db.currentPlayer.employees.Any(employee => employee.id == 16))
                {
                    degrees *= 0.85f;
                }
                break;
            case 3:
                if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 4))
                {
                    degrees *= 0.9f;
                }
                if (db.currentPlayer.employees.Any(employee => employee.id == 18))
                {
                    degrees *= 0.85f;
                }
                break;
            case 4:
                if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 5))
                {
                    degrees *= 0.9f;
                }
                if (db.currentPlayer.employees.Any(employee => employee.id == 19))
                {
                    degrees *= 0.85f;
                }
                break;
            case 5:
                //todo
                break;
            case 6:
                //todo
                break;
            default:
                Debug.Log("Item does not belong to a group");
                break;
        }
        //general upgrades
        if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 1))
        {
            degrees *= 0.95f;
        }
        if (db.currentPlayer.employees.Any(employee => employee.id == 0))
        {
            degrees *= 0.95f;
        }
        //TODO: ADD UPGRADE 11, but this might require a rework of this system

        if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == 12 && db.currentPlayer.merch[id - 1].baseCost >= 450))
        {
            degrees *= 0.9f;
        }

        //Todo Test for correctness
        float sale = baseCost * (1 + markup);

        if (debug) { Debug.Log(sale); }

        //TODO Upgrades that sell multiple items at a time
        db.currentPlayer.ChangeQuantity(id, -1);
        db.currentPlayer.currentMoney += sale;
        db.currentPlayer.dailySales += sale;

        //Upgrade ID 7, 8, 9, 10: Auto restock items

        int upgradeNeeded = -1;
        switch (db.currentPlayer.merch[id - 1].group)
        {
            //potions
            case 1:
                upgradeNeeded = 7;
                break;
            //accessory
            case 2:
                upgradeNeeded = 8;
                break;
            //weapons
            case 3:
                upgradeNeeded = 9;
                break;
            //special items
            case 4:
                upgradeNeeded = 10;
                break;
            //runes
            case 5:
                //TODO
                break;
            //shields
            case 6:
                //TODO
                break;
            default:
                Debug.Log("Unknown item group: " + db.currentPlayer.merch[id - 1].group);
                break;
        }
        if (db.currentPlayer.upgrades.Any(upgrade => upgrade.id == upgradeNeeded))
        {
            //auto restocks if possible
            //we have already moved the purchased item, so see if there's another one
            if (db.currentPlayer.merch[id].quantity > 0)
            {
                //Auto restock, yay!
            }
            else
            {
                db.currentPlayer.active[id - 1] = 0;
            }
        }
        else
        {
            db.currentPlayer.active[id - 1] = 0;

        }
    }
}

