using Unity.VisualScripting;
using UnityEngine;
using System;

interface InteractMenu
{
    public void InteractMenu();
}

interface InteractDisplay
{
    public void InteractDisplay();
}

public class InteractionManager : MonoBehaviour
{
    //IMPORTANT! Any object that is meant to be interactable must be assigned to the interactable layer (#3).

    [SerializeField] GameObject interactSource;
    [SerializeField] float interactRayRange;

    public Rigidbody playerModel;
    public Animator playerAnimator;

    [SerializeField] GameObject redArrow;

    [SerializeField] Glossary gl;

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

        if (Input.GetKeyDown(KeyCode.F) && !gl.glossaryScreen.activeSelf)
        {
            if (Physics.Raycast(interactSource.transform.position,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit rayInfoF, interactRayRange))
            {
                if (rayInfoF.collider.gameObject.
                TryGetComponent(out InteractMenu interactableM))
                {
                    //Debug.Log("Attempting Menu Interaction");
                    if (rayInfoF.collider.gameObject.layer == 3)
                    {
                        SwitchInteractState();
                        interactableM.InteractMenu();
                    }
                }

                if (rayInfoF.collider.gameObject.
                TryGetComponent(out InteractDisplay interactableD))
                {
                    //Debug.Log("Attempting Display Interaction");
                    if (rayInfoF.collider.gameObject.layer == 6)
                    {
                        //Do not want to freeze movement when stocking displays so do not change interact state
                        interactableD.InteractDisplay();
                    }
                }
            }

        }
    }

    public void SwitchInteractState()
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


