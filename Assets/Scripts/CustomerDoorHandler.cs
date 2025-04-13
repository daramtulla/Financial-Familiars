using Unity.VisualScripting;
using UnityEngine;

public class CustomerDoorHandler : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Animator controller;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            controller.SetBool("isOpen", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            controller.SetBool("isOpen", false);
        }
    }
}
