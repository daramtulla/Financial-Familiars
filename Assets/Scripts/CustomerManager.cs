using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Data;
using UnityEngine.InputSystem.Controls;
using System.Collections;
using NUnit.Framework;
using System.Linq;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public SoundManager soundManager;
    [SerializeField] JSONDatabaseOperations db;

    [SerializeField] Boolean debug;

    [SerializeField] RandomGenNum rnd;
    [SerializeField] private GameManager gameManager;

    public int dayTimeTotal;

    //Item id first, then sale time
    private List<KeyValuePair<int, int>> timeline = new List<KeyValuePair<int, int>>();

    //Item id, then customer time
    private List<KeyValuePair<int, int>> entrance = new List<KeyValuePair<int, int>>();

    private float dayTime;

    [SerializeField] Boolean timerActive;

    private int day;

    private float pastDayTime;

    //0 if no customer is heading towards, 1 if is heading towards, 2 if customer has reached
    public int[] customerReached = new int[18];

    [SerializeField] CustomerMovement cm;

    public Text dayTimeNumberText;

    [SerializeField] CustomerInteractHandler[] customerInteractHandlers;

    [SerializeField] InteractableDisplay interact;

    // HIRE 0, 15, 16, 18, 19: Affects demand/prices
    // UPGRADE 1, 2, 3, 4, 5, 11, 12: Affects demand/prices

    private void Start()
    {
        customerInteractHandlers = FindObjectsByType<CustomerInteractHandler>(FindObjectsSortMode.None);
    }

    public void StartSelling()
    {
        dayTime = 0;
        pastDayTime = 0;
        timerActive = true;

        //Zero out customerReached array
        for (int i = 0; i < customerReached.Length; i++)
        {
            customerReached[i] = 0;
        }

        //Now calls generate customer entrance
        GetItemSaleTimeLine();
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

            if (dayTime >= pastDayTime + .01)
            {
                pastDayTime = dayTime;

                if (debug) { Debug.Log("Checking sale time at " + dayTime); }

                //Check to see what sales occured at this time
                SaleTimeCheck(copy1);
                CustomerTimeCheck(copy2);

                dayTimeNumberText.text = ((int)(dayTimeTotal - dayTime)).ToString();
            }

            yield return null;
        }

        //After timer is up
        db.currentPlayer.cycleNum = 2;

        //Debug.Log($"db.currentPlayer.cycleNum: {db.currentPlayer.cycleNum}");
        timerActive = false;
        soundManager.soundAudioSource.PlayOneShot(soundManager.storeClosing, 0.3f);

        //sign animation
        gameManager.ShowClosedSign();

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
                    customerReached[pair.Key - 1] = 1;
                }
            }
        }
    }

    public void SaleTimeCheck(List<KeyValuePair<int, int>> copy)
    {
        List<KeyValuePair<int, int>> toRemoveList = new();

        foreach (KeyValuePair<int, int> pair in timeline)
        {
            if (pair.Value <= dayTime)
            {
                if (customerReached[pair.Key - 1] == 2)
                {
                    customerReached[pair.Key - 1] = 0;
                    AttemptSale(pair.Key);
                    if (debug) { Debug.Log("Attempting to sell merch " + pair.Value); }
                    toRemoveList.Add(pair);
                }
            }
        }

        foreach (var removePair in toRemoveList)
        {
            timeline.Remove(removePair);
        }
    }

    //Generates a number of sales based on the markup of the item and when those sales occur
    public void GetItemSaleTimeLine()
    {
        List<Merchandise> merch = db.currentPlayer.merch;

        bool onWay;

        for (int i = 0; i < 18; i++)
        {
            float markupDeviation = merch[i].markupPercentage * merch[i].customerMod;
            int saleLimit = (int)Math.Round(markupDeviation);

            if (saleLimit > 3) { saleLimit = 3; }

            int sales = rnd.GetDailyItemSaleNumber(0, 4 - saleLimit);

            onWay = false;

            //Generate sale time for item
            for (int j = 0; j < sales; j++)
            {
                KeyValuePair<int, int> saleTime = new(i + 1, rnd.GetRandomSaleTime(dayTimeTotal));

                foreach (KeyValuePair<int, int> pair in timeline)
                {
                    if (saleTime.Value == pair.Value)
                    {
                        onWay = true;
                    }
                }

                if (!onWay)
                {
                    GenerateCustomerEntrance(saleTime.Key, saleTime.Value);

                    if (debug)
                    {
                        Debug.Log("Pair added: " + saleTime.Key + ", " + saleTime.Value);
                    }

                    timeline.Add(saleTime);
                }
            }
        }
    }

    public void GenerateCustomerEntrance(int merchId, int entranceTime)
    {
        //KeyValuePair<int, int> customerTime = new(rnd.GetRandomMerchId(), rnd.GetRandomCustomerEntrance(dayTimeTotal));
        KeyValuePair<int, int> customerTime = new(merchId, entranceTime);

        if (debug)
        {
            Debug.Log("Pair added: " + customerTime.Key + ", " + customerTime.Value);
        }

        entrance.Add(customerTime);
    }


    public void LateUpdate()
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
        Debug.Log($"SEARCH AttemptSale: {cm.saleChanceCheck}");
        if (db.currentPlayer.active[id - 1] == 1 && rnd.GetSaleChance() > 3)
        {
            //Loop through all the CustomerInteractHandler scripts and check which one has matching item IDs
            foreach (CustomerInteractHandler customerInteractHandler in customerInteractHandlers)
            {
                if (customerInteractHandler.tableMerchID == id)
                {
                    customerInteractHandler.activateCustomerInteractAnimation = true;
                }
            }

            SellItem(id);
            db.currentPlayer.playedSfx[id - 1] = 0;
            soundManager.soundAudioSource.PlayOneShot(soundManager.itemSold, 1.25f);
        }
    }

    //HIRES AND UPGRADES
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
                if (db.CheckUpgrade(2))
                {
                    degrees *= 0.9f;
                }
                if (db.CheckEmployee(16))
                {
                    if (db.CheckEmployee(15))
                    {
                        degrees *= 0.92f;
                    }
                    else
                    {
                        degrees *= 0.85f;
                    }
                }
                break;
            //accessories
            case 2:
                if (db.CheckUpgrade(3))
                {
                    degrees *= 0.9f;
                }
                if (db.CheckEmployee(17))
                {
                    if (db.CheckEmployee(15))
                    {
                        degrees *= 0.92f;
                    }
                    else
                    {
                        degrees *= 0.85f;
                    }
                }
                break;
            case 3:
                if (db.CheckUpgrade(4))
                {
                    degrees *= 0.9f;
                }
                if (db.CheckEmployee(19))
                {
                    if (db.CheckEmployee(15))
                    {
                        degrees *= 0.92f;
                    }
                    else
                    {
                        degrees *= 0.85f;
                    }
                }
                break;
            case 4:
                if (db.CheckUpgrade(5))
                {
                    degrees *= 0.9f;
                }
                if (db.CheckEmployee(20))
                {
                    if (db.CheckEmployee(15))
                    {
                        degrees *= 0.92f;
                    }
                    else
                    {
                        degrees *= 0.85f;
                    }
                }
                break;
            case 5:
                if (db.CheckUpgrade(15))
                {
                    degrees *= 0.9f;
                }
                break;
            case 6:
                if (db.CheckUpgrade(16))
                {
                    degrees *= 0.9f;
                }
                break;
            default:
                Debug.Log("Item does not belong to a group");
                break;
        }
        //general upgrades
        if (db.CheckUpgrade(0))
        {
            degrees *= 0.97f;
        }
        if (db.CheckEmployee(0))
        {
            if (db.CheckEmployee(15))
            {
                degrees *= 0.98f;
            }
            else
            {
                degrees *= 0.95f;
            }
        }
        if (db.CheckEmployee(11))
        {
            if (db.CheckEmployee(15))
            {
                degrees *= 1.04f;
            }
            else
            {
                degrees *= 1.08f;
            }
        }

        if (db.CheckEmployee(14) && db.currentPlayer.merch[id - 1].baseCost >= 450)
        {
            if (db.CheckEmployee(15))
            {
                degrees *= 0.95f;
            }
            else
            {
                degrees *= 0.9f;
            }
        }

        float sale = baseCost * (1 + markup);
        if (db.CheckEmployee(12))
        {
            if (db.CheckEmployee(15))
            {
                degrees *= 1.01f;
            }
            else
            {
                degrees *= 1.03f;
            }
        }

        if (debug) { Debug.Log(sale); }

        db.currentPlayer.ChangeQuantity(id, -1);
        db.currentPlayer.currentMoney += sale;
        db.currentPlayer.dailySales += sale;
        //chance to buy two items
        // If:
        //1. This is an accessory
        //2. We have the available item
        //3. We have the employee
        //4. 5% chance to buy it
        if (db.currentPlayer.merch[id - 1].group == 2 &&
            db.currentPlayer.merch[id].quantity > 0 &&
            db.CheckEmployee(13) &&
            new System.Random().Next(1, 100) >= 95)
        {
            db.currentPlayer.ChangeQuantity(id, -1);
            db.currentPlayer.currentMoney += sale;
            db.currentPlayer.dailySales += sale;
        }

        //Upgrade ID 7 - 12: Auto restock items
        int upgradeNeeded;
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
                upgradeNeeded = 11;
                break;
            //shields
            case 6:
                upgradeNeeded = 12;
                break;
            default:
                upgradeNeeded = -1;
                Debug.Log("Unknown item group: " + db.currentPlayer.merch[id - 1].group);
                break;
        }
        if (db.CheckUpgrade(upgradeNeeded))
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

