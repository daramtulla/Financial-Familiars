using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Polybrush;
using UnityEngine.Rendering;

public class CustomerMovement : MonoBehaviour
{
    //General pathing nodes
    public GameObject startNode;
    public GameObject node1;
    public GameObject node2;
    public GameObject node3;
    public GameObject node4a;
    public GameObject node4b;
    public GameObject node5a;
    public GameObject node5b;
    public GameObject node5c;
    public GameObject node5d;
    public GameObject node5e;
    public GameObject node5f;

    public GameObject node6a;
    public GameObject node6b;
    public GameObject node6c;
    public GameObject node6d;
    public GameObject node6e;
    public GameObject node6f;

    public GameObject node7a;
    public GameObject node7b;
    public GameObject node7c;
    public GameObject node7d;
    public GameObject node7e;
    public GameObject node7f;

    public GameObject nodeReg1;
    public GameObject nodeReg2;

    public GameObject node8;
    public GameObject node9;
    public GameObject endNode;

    //Merch nodes
    public GameObject nodeT3Potion;
    public GameObject nodeT3Special;
    public GameObject nodeT3Accessory;
    public GameObject nodeT3Weapon;
    public GameObject nodeT3Shield;
    public GameObject nodeT3Rune;

    public GameObject nodeT2Potion;
    public GameObject nodeT2Special;
    public GameObject nodeT2Accessory;
    public GameObject nodeT2Weapon;
    public GameObject nodeT2Shield;
    public GameObject nodeT2Rune;

    public GameObject nodeT1Potion;
    public GameObject nodeT1Special;
    public GameObject nodeT1Accessory;
    public GameObject nodeT1Weapon;
    public GameObject nodeT1Shield;
    public GameObject nodeT1Rune;

    public int speed;

    //Non node refs
    public GameObject customerPrefab;
    public GameObject customerPrefab1;
    public GameObject customerPrefab2;
    GameObject[] customerSet;

    public Dictionary<GameObject, Customer> linkTable = new Dictionary<GameObject, Customer>();

    public Boolean debug;

    [SerializeField] CustomerManager cm;

    [SerializeField] JSONDatabaseOperations jsonDbOps;

    private RandomGenNum rnd;
    public bool saleChanceCheck;

    public void Awake()
    {
        rnd = new();
    }
    private void Start()
    {
        customerSet = new GameObject[] { customerPrefab, customerPrefab1, customerPrefab2 };
    }
    void Update()
    {

        //Testing
        if (debug && Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 1; i < 19; i++)
            {
                CreateCustomer(i);
            }
        }

        foreach (KeyValuePair<GameObject, Customer> cust in linkTable)
        {
            switch (cust.Value.nodeNum)
            {
                case 1:
                    GoToNode1(cust.Key);
                    break;
                case 2:
                    GoToNode2(cust.Key);
                    break;
                case 3:
                    GoToNode3(cust.Key);
                    break;
                case 4:
                    GoToNode4(cust.Key);
                    break;
                case 5:
                    GoToNode5(cust.Key);
                    break;
                case 6:
                    GoToNode6(cust.Key);
                    break;
                case 7:
                    GotoItem(cust.Key);
                    break;
                case 8:
                    //Has already reached item.
                    cm.customerReached[cust.Value.itemToBuy - 1] = 2;

                    GoToNode7(cust.Key);
                    break;
                case 9:
                    GoToReg(cust.Key);
                    break;
                case 10:
                    GoToNode8(cust.Key);
                    break;
                case 11:
                    GoToNode9(cust.Key);
                    break;
                case 12:
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
        //GameObject customerObj = Instantiate(customerPrefab, startNode.transform.position, startNode.transform.rotation);
        GameObject customerObj = Instantiate(customerSet[UnityEngine.Random.Range(0, 3)], startNode.transform.position, startNode.transform.rotation);
        customerObj.SetActive(true);
        Customer customerClass = new Customer(merchId, this);
        linkTable.Add(customerObj, customerClass);
        if (debug) { Debug.Log("Obj Id: " + customerObj.GetInstanceID()); }
    }

    public void GoToNode1(GameObject cust)
    {
        cust.transform.position = Vector3.MoveTowards(cust.transform.position, node1.transform.position, speed * Time.deltaTime);
        cust.transform.LookAt(node1.transform.position);
    }

    public void GoToNode2(GameObject cust)
    {
        cust.transform.position = Vector3.MoveTowards(cust.transform.position, node2.transform.position, speed * Time.deltaTime);
        cust.transform.LookAt(node2.transform.position);
    }

    public void GoToNode3(GameObject cust)
    {
        cust.transform.position = Vector3.MoveTowards(cust.transform.position, node3.transform.position, speed * Time.deltaTime);
        cust.transform.LookAt(node3.transform.position);
    }

    public void GoToNode4(GameObject cust)
    {
        if (linkTable[cust].nextNode == node4a)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node4a.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node4a.transform.position);
        }
        else if (linkTable[cust].nextNode == node4b)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node4b.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node4b.transform.position);
        }
    }
    public void GoToNode5(GameObject cust)
    {
        if (linkTable[cust].nextNode == node5a)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node5a.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node5a.transform.position);
        }
        else if (linkTable[cust].nextNode == node5b)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node5b.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node5b.transform.position);
        }
        else if (linkTable[cust].nextNode == node5c)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node5c.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node5c.transform.position);
        }
        else if (linkTable[cust].nextNode == node5d)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node5d.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node5d.transform.position);
        }
        else if (linkTable[cust].nextNode == node5e)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node5e.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node5e.transform.position);
        }
        else if (linkTable[cust].nextNode == node5f)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node5f.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node5f.transform.position);
        }
    }
    public void GoToNode6(GameObject cust)
    {
        if (linkTable[cust].nextNode == node6a)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node6a.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node6a.transform.position);
        }
        else if (linkTable[cust].nextNode == node6b)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node6b.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node6b.transform.position);
        }
        else if (linkTable[cust].nextNode == node6c)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node6c.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node6c.transform.position);
        }
        else if (linkTable[cust].nextNode == node6d)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node6d.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node6d.transform.position);
        }
        else if (linkTable[cust].nextNode == node6e)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node6e.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node6e.transform.position);
        }
        else if (linkTable[cust].nextNode == node6f)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node6f.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(node6f.transform.position);
        }
    }

    public void GotoItem(GameObject cust)
    {
        if (linkTable[cust].nextNode == nodeT3Shield)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT3Shield.transform.position, speed * Time.deltaTime);

        }
        else if (linkTable[cust].nextNode == nodeT2Shield)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT2Shield.transform.position, speed * Time.deltaTime);

        }
        else if (linkTable[cust].nextNode == nodeT1Shield)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT1Shield.transform.position, speed * Time.deltaTime);

        }
        else if (linkTable[cust].nextNode == nodeT3Rune)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT3Rune.transform.position, speed * Time.deltaTime);

        }
        else if (linkTable[cust].nextNode == nodeT2Rune)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT2Rune.transform.position, speed * Time.deltaTime);

        }
        else if (linkTable[cust].nextNode == nodeT1Rune)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT1Rune.transform.position, speed * Time.deltaTime);
            ;
        }
        else if (linkTable[cust].nextNode == nodeT3Weapon)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT3Weapon.transform.position, speed * Time.deltaTime);

        }
        else if (linkTable[cust].nextNode == nodeT2Weapon)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT2Weapon.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT1Weapon)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT1Weapon.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT3Accessory)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT3Accessory.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT2Accessory)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT2Accessory.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT1Accessory)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT1Accessory.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT3Special)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT3Special.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT2Special)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT2Special.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT1Special)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT1Special.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT3Potion)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT3Potion.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT2Potion)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT2Potion.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == nodeT1Potion)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeT1Potion.transform.position, speed * Time.deltaTime);
        }

        cust.transform.LookAt(linkTable[cust].nextNode.transform.position);
        //cust.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Interact_trig");
    }
    public void GoToNode7(GameObject cust)
    {
        if (linkTable[cust].nextNode == node7a)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node7a.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == node7b)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node7b.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == node7c)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node7c.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == node7d)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node7d.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == node7e)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node7e.transform.position, speed * Time.deltaTime);
        }
        else if (linkTable[cust].nextNode == node7f)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, node7f.transform.position, speed * Time.deltaTime);
        }
        cust.transform.LookAt(linkTable[cust].nextNode.transform.position);
    }

    public void GoToReg(GameObject cust)
    {
        if (linkTable[cust].nextNode == nodeReg1)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeReg1.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(nodeReg2.transform.position);
        }
        else if (linkTable[cust].nextNode == nodeReg2)
        {
            cust.transform.position = Vector3.MoveTowards(cust.transform.position, nodeReg2.transform.position, speed * Time.deltaTime);
            cust.transform.LookAt(nodeReg2.transform.position);
        }
    }
    public void GoToNode8(GameObject cust)
    {
        cust.transform.position = Vector3.MoveTowards(cust.transform.position, node8.transform.position, speed * Time.deltaTime);
        cust.transform.LookAt(node8.transform.position);
    }
    public void GoToNode9(GameObject cust)
    {
        cust.transform.position = Vector3.MoveTowards(cust.transform.position, node9.transform.position, speed * Time.deltaTime);
        cust.transform.LookAt(node9.transform.position);
    }

    public void GoToNodeEnd(GameObject cust)
    {
        cust.transform.position = Vector3.MoveTowards(cust.transform.position, endNode.transform.position, speed * Time.deltaTime);
        cust.transform.LookAt(endNode.transform.position);
    }

    public class Customer
    {
        public int itemToBuy;
        public int speed;

        public int nodeNum;

        public GameObject nextNode;
        public CustomerMovement cm;

        public bool alreadyPickedupItem = false;
        public bool alreadyGeneratedSaleChance = false;

        public Customer(int merchId, CustomerMovement instance)
        {
            this.itemToBuy = merchId;
            this.speed = 1;
            this.nodeNum = 1;
            this.cm = instance;
            this.nextNode = cm.node1;
        }
    }
}

