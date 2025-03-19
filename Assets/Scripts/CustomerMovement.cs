using System.Collections.Generic;
using UnityEngine;

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
    private GameObject customerPrefab;

    private Dictionary<Customer, GameObject> linkTable;




    public void CreateCustomer(int merchId)
    {
        GameObject customerObj = Instantiate(customerPrefab, startNode.transform.position, startNode.transform.rotation);
        customerObj.SetActive(true);
        Customer customerClass = new Customer(merchId);
        linkTable.Add(customerClass, customerObj);

        GoToNode1(customerClass);
    }

    public void GoToNode1(Customer cust)
    {
        transform.position = Vector3.Lerp(transform.position, node1.transform.position, speed * Time.deltaTime);
    }

    public void GoToNode2(Customer cust)
    {
        switch (cust.itemToBuy)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 16:
            case 17:
            case 18:
                //transform.position = Vector3.Lerp(transform.position, node2f.transform.position, speed * Time.deltaTime);
                break;
            default:
                Debug.Log("Invalid Node to move to");
                break;
        }


    }

    public void GoToNode3()
    {

    }

    public void GoToNode4()
    {

    }

    public void GoToNode5()
    {

    }

    public void GoToNodeEnd()
    {

    }
}

public class Customer
{
    public int itemToBuy;
    public int speed;

    public Customer(int merchId)
    {
        this.itemToBuy = merchId;

        //TODO dynamically set
        this.speed = 1;
    }
}
