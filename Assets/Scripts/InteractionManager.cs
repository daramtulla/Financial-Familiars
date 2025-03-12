using Unity.VisualScripting;
using UnityEngine;
using System;
using TMPro;

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
    [SerializeField] GameObject interactableNameText;

    [SerializeField] TMP_InputField textInputField;

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
                    rayInfoF2.collider.gameObject.GetComponent<Interactable>().interactableNameText.text = rayInfoF2.collider.gameObject.GetComponent<Interactable>().interactableNameTextString;
                    interactableNameText.SetActive(true);
                    redArrow.SetActive(true);
                    redArrow.transform.position = new Vector3(interactableTransform.transform.position.x, interactableTransform.transform.position.y + 3, interactableTransform.transform.position.z);
                }
            }
        }
        else
        {
            interactableNameText.SetActive(false);
            redArrow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && !textInputField.isFocused)
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


