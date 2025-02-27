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
    public Animator playerAnimator;

    [SerializeField] GameObject redArrow;

    void Update()
    {
        if (Physics.Raycast(interactSource.transform.position,
           transform.TransformDirection(Vector3.forward),
           out RaycastHit rayInfoF2, interactRayRange))
        {
            if (rayInfoF2.collider.gameObject.
            TryGetComponent(out Transform interactableTransform))
            {
                if (rayInfoF2.collider.gameObject.layer == 3)
                {
                    redArrow.SetActive(true);
                    redArrow.transform.position = new Vector3(interactableTransform.transform.position.x, interactableTransform.transform.position.y + 3, interactableTransform.transform.position.z);
                }
            }
        }
        else
        {
            redArrow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
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
                        switchInteractState();
                        interactable.Interact();
                    }
                }
            }
            /*Commented out because 2 menus can open at once on an interaction.
            if (Physics.Raycast(interactSource.transform.position, transform.TransformDirection(Vector3.back), out RaycastHit rayInfoB, interactRayRange))
            {
                if (rayInfoB.collider.gameObject.TryGetComponent(out Interact interactable))
                {
                    if (rayInfoB.collider.gameObject.layer == 3)
                    {
                        switchInteractState();
                        interactable.Interact();
                    }
                }
            }
            */

            /* Commented out because of the issue of the front raycast and one of the side raycasts hitting the same object.
            if (Physics.Raycast(interactSource.transform.position, transform.TransformDirection(Vector3.left), out RaycastHit rayInfoL, interactRayRange))
            {
                if (rayInfoL.collider.gameObject.TryGetComponent(out Interact interactable))
                {
                    if (rayInfoL.collider.gameObject.layer == 3)
                    {
                        switchInteractState();
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
                        switchInteractState();
                        interactable.Interact();
                    }
                }
            }
            */

        }
    }

    public void switchInteractState()
    {
        IsInteracting(!GetInteractState());
        ZeroPlayerVelocity(playerModel);
        ZeroPlayerRotation(playerModel);
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

        //To stop the player walking animation
        playerAnimator.SetFloat("Speed_f", 0);
    }

    public void ZeroPlayerRotation(Rigidbody playerModel)
    {
        playerModel.constraints = RigidbodyConstraints.FreezeRotation;
    }
}


