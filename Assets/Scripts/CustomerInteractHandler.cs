using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CustomerInteractHandler : MonoBehaviour
{
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
