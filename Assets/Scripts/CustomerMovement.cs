using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    //General pathing nodes
    [SerializeField] GameObject startNode;
    [SerializeField] GameObject node1;
    [SerializeField] GameObject node2a;
    [SerializeField] GameObject node2b;
    [SerializeField] GameObject node2c;
    [SerializeField] GameObject node2d;
    [SerializeField] GameObject node2e;
    [SerializeField] GameObject node2f;

    [SerializeField] GameObject node3a;
    [SerializeField] GameObject node3b;
    [SerializeField] GameObject node3c;
    [SerializeField] GameObject node3d;
    [SerializeField] GameObject node3e;
    [SerializeField] GameObject node3f;

    [SerializeField] GameObject nodeReg1;
    [SerializeField] GameObject nodeReg2;

    [SerializeField] GameObject node4;
    [SerializeField] GameObject node5;
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

    //Non node refs
    private GameObject customerPrefab;


    public void CreateCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, startNode.transform.position, startNode.transform.rotation);
    }
}
