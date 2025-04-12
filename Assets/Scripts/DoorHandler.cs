using Unity.VisualScripting;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Animator controller;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.SetBool("isOpen", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.SetBool("isOpen", false);
        }
    }
}
