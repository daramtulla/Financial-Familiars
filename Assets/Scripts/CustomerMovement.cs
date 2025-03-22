using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CustomerMovement : MonoBehaviour
{
    //General pathing nodes
    [SerializeField] GameObject startNode;
    [SerializeField] GameObject node1;
    [SerializeField] GameObject node2;
    [SerializeField] GameObject node3;
    [SerializeField] GameObject node4a;
    [SerializeField] GameObject node4b;
    [SerializeField] GameObject node5a;
    [SerializeField] GameObject node5b;
    [SerializeField] GameObject node5c;
    [SerializeField] GameObject node5d;
    [SerializeField] GameObject node5e;
    [SerializeField] GameObject node5f;

    [SerializeField] GameObject node6a;
    [SerializeField] GameObject node6b;
    [SerializeField] GameObject node6c;
    [SerializeField] GameObject node6d;
    [SerializeField] GameObject node6e;
    [SerializeField] GameObject node6f;

    [SerializeField] GameObject node7a;
    [SerializeField] GameObject node7b;
    [SerializeField] GameObject node7c;
    [SerializeField] GameObject node7d;
    [SerializeField] GameObject node7e;
    [SerializeField] GameObject node7f;

    [SerializeField] GameObject nodeReg1;
    [SerializeField] GameObject nodeReg2;

    [SerializeField] GameObject node8;
    [SerializeField] GameObject node9;
    [SerializeField] GameObject endNode;

    //Merch nodes
    [SerializeField] GameObject nodeT3Potion;
    [SerializeField] GameObject nodeT3Special;
    [SerializeField] GameObject nodeT3Accessory;
    [SerializeField] GameObject nodeT3Weapon;
    [SerializeField] GameObject nodeT3Shield;
    [SerializeField] GameObject nodeT3Rune;

    [SerializeField] GameObject nodeT2Potion;
    [SerializeField] GameObject nodeT2Special;
    [SerializeField] GameObject nodeT2Accessory;
    [SerializeField] GameObject nodeT2Weapon;
    [SerializeField] GameObject nodeT2Shield;
    [SerializeField] GameObject nodeT2Rune;

    [SerializeField] GameObject nodeT1Potion;
    [SerializeField] GameObject nodeT1Special;
    [SerializeField] GameObject nodeT1Accessory;
    [SerializeField] GameObject nodeT1Weapon;
    [SerializeField] GameObject nodeT1Shield;
    [SerializeField] GameObject nodeT1Rune;

    [SerializeField] int speed;

    //Non node refs
    [SerializeField] GameObject customerPrefab;

    private Dictionary<Customer, GameObject> linkTable = new Dictionary<Customer, GameObject>();

    [SerializeField] Boolean debug;

    void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.Z))
        {
            CreateCustomer(7);
        }

        foreach (KeyValuePair<Customer, GameObject> cust in linkTable)
        {
            switch (cust.Key.nodeNum)
            {
                case 0:
                    GoToNode1(cust.Key);
                    break;
                case 1:
                    GoToNode2(cust.Key);
                    break;
                case 2:
                    GoToNode3(cust.Key);
                    break;
                case 3:
                    GoToNode4(cust.Key);
                    break;
                case 4:
                    GoToNode5(cust.Key);
                    break;
                case 5:
                    GoToNode6(cust.Key);
                    break;
                case 6:
                    GoToNode7(cust.Key);
                    break;
                case 7:
                    GoToNode8(cust.Key);
                    break;
                case 8:
                    GoToNode9(cust.Key);
                    break;
                case 9:
                    GoToNodeEnd(cust.Key);
                    break;
                default:
                    Debug.Log("Invalid node num");
                    break;
            }
        }
    }

    public void CreateCustomer(int merchId)
    {
        GameObject customerObj = Instantiate(customerPrefab, startNode.transform.position, startNode.transform.rotation);
        customerObj.SetActive(true);
        Customer customerClass = new Customer(merchId);
        linkTable.Add(customerClass, customerObj);
    }

    public void GoToNode1(Customer cust)
    {

        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node1.transform.position, speed * Time.deltaTime);
    }

    public void GoToNode2(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }

    public void GoToNode3(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }

    public void GoToNode4(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }
    public void GoToNode5(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }
    public void GoToNode6(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }
    public void GoToNode7(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }
    public void GoToNode8(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }
    public void GoToNode9(Customer cust)
    {
        linkTable[cust].transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, speed * Time.deltaTime);
    }

    public void GoToNodeEnd(Customer cust)
    {

    }
}

public class Customer
{
    public int itemToBuy;
    public int speed;

    public int nodeNum;

    public Customer(int merchId)
    {
        this.itemToBuy = merchId;

        //TODO dynamically set
        this.speed = 1;

        this.nodeNum = 0;
    }
}
