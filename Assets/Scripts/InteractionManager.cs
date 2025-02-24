using Unity.VisualScripting;
using UnityEngine;
using System;

interface Interact
{
    public void Interact();
}

public class InteractionManager : MonoBehaviour
{
    //IMPORTANT! Any object that is meant to be interactable must be assigned to the interactable layer (#3).

    [SerializeField] GameObject interactSource;
    [SerializeField] float interactRayRange;

    public Rigidbody playerModel;

    void Update()
    {
        switchInteractState();

        if (Input.GetKey(KeyCode.F))
        {
            if (Physics.Raycast(interactSource.transform.position,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit rayInfoF, interactRayRange))
            {
                if (rayInfoF.collider.gameObject.
                TryGetComponent(out Interact interactable))
                {
                    if (rayInfoF.collider.gameObject.layer == 3)
                    {
                        interactable.Interact();
                    }
                }
            }

            if (Physics.Raycast(interactSource.transform.position, transform.TransformDirection(Vector3.back), out RaycastHit rayInfoB, interactRayRange))
            {
                if (rayInfoB.collider.gameObject.TryGetComponent(out Interact interactable))
                {
                    if (rayInfoB.collider.gameObject.layer == 3)
                    {
                        interactable.Interact();
                    }
                }
            }

            if (Physics.Raycast(interactSource.transform.position, transform.TransformDirection(Vector3.left), out RaycastHit rayInfoL, interactRayRange))
            {
                if (rayInfoL.collider.gameObject.TryGetComponent(out Interact interactable))
                {
                    if (rayInfoL.collider.gameObject.layer == 3)
                    {
                        interactable.Interact();
                    }
                }
            }

            if (Physics.Raycast(interactSource.transform.position, transform.TransformDirection(Vector3.right), out RaycastHit rayInfoR, interactRayRange))
            {
                if (rayInfoR.collider.gameObject.TryGetComponent(out Interact interactable))
                {
                    if (rayInfoR.collider.gameObject.layer == 3)
                    {
                        interactable.Interact();
                    }
                }
            }

        }
    }

    public void switchInteractState()
    {
        if (Input.GetKey(KeyCode.F))
        {
            IsInteracting(!GetInteractState());
            ZeroPlayerVelocity(playerModel);
        }
    }

    public static void IsInteracting(Boolean state)
    {
        MovementController.interacting = state;
    }

    public static Boolean GetInteractState()
    {
        return MovementController.interacting;
    }

    public void ZeroPlayerVelocity(Rigidbody playerModel)
    {
        playerModel.linearVelocity = Vector3.zero;
    }

}


