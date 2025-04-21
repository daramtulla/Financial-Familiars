using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CustomerInteractHandler : MonoBehaviour
{
    //This script is attached to the gameobject that is a child gameobject of the pedestal that holds an item
    //The pedestal's item should have the same item ID as the tableMerchID
    public int tableMerchID;

    //If the customer arm animation when purchasing items should be played
    public bool activateCustomerInteractAnimation = false;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            if (activateCustomerInteractAnimation == true)
            {
                other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Interact_trig");
                activateCustomerInteractAnimation = false;
            }
        }
    }
}
