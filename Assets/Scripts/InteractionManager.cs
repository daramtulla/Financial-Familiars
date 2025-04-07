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
    //IMPORTANT! Any object that is meant to be interactable must be assigned to the interactable layer (#3) or display layer (#6).

    [SerializeField] GameObject interactSource;
    [SerializeField] float interactRayRange;

    public Rigidbody playerModel;
    public Animator playerAnimator;

    [SerializeField] GameObject redArrow;
    [SerializeField] GameObject interactableNameText;

    [SerializeField] JSONDatabaseOperations db;

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
                    rayInfoF2.collider.gameObject.GetComponent<InteractableMenu>().interactableNameText.text = rayInfoF2.collider.gameObject.GetComponent<InteractableMenu>().interactableNameTextString;
                    interactableNameText.SetActive(true);
                    redArrow.SetActive(true);
                    redArrow.transform.position = new Vector3(interactableTransform.transform.position.x, interactableTransform.transform.position.y + 3, interactableTransform.transform.position.z);
                }
                else if (rayInfoF2.collider.gameObject.layer == 6)
                {
                    String display = rayInfoF2.collider.gameObject.GetComponent<InteractableDisplay>().displayToModify.name;

                    rayInfoF2.collider.gameObject.GetComponent<InteractableDisplay>().interactableNameText.text = rayInfoF2.collider.gameObject.GetComponent<InteractableDisplay>().interactableNameTextString + " (" + AmountInStock(display) + ")";
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

    public int AmountInStock(String displayToModify)
    {
        if (db == null)
        {
            return -1;
        }


        if (displayToModify == "Potion T1 Pedestal")
        {
            return db.currentPlayer.merch[0].quantity;
        }
        else if (displayToModify == "Potion T2 Pedestal")
        {
            return db.currentPlayer.merch[1].quantity;
        }
        else if (displayToModify == "Potion T3 Pedestal")
        {
            return db.currentPlayer.merch[2].quantity;
        }
        else if (displayToModify == "Accessory T1 Pedestal")
        {
            return db.currentPlayer.merch[3].quantity;
        }
        else if (displayToModify == "Accessory T2 Pedestal")
        {
            return db.currentPlayer.merch[4].quantity;
        }
        else if (displayToModify == "Accessory T3 Pedestal")
        {
            return db.currentPlayer.merch[5].quantity;
        }
        else if (displayToModify == "Weapon T1 Pedestal")
        {
            return db.currentPlayer.merch[6].quantity;
        }
        else if (displayToModify == "Weapon T2 Pedestal")
        {
            return db.currentPlayer.merch[7].quantity;
        }
        else if (displayToModify == "Weapon T3 Pedestal")
        {
            return db.currentPlayer.merch[8].quantity;
        }
        else if (displayToModify == "Special T1 Pedestal")
        {
            return db.currentPlayer.merch[9].quantity;
        }
        else if (displayToModify == "Special T2 Pedestal")
        {
            return db.currentPlayer.merch[10].quantity;
        }
        else if (displayToModify == "Special T3 Pedestal")
        {
            return db.currentPlayer.merch[11].quantity;
        }
        else if (displayToModify == "Shield T1 Pedestal")
        {
            return db.currentPlayer.merch[12].quantity;
        }
        else if (displayToModify == "Shield T2 Pedestal")
        {
            return db.currentPlayer.merch[13].quantity;
        }
        else if (displayToModify == "Shield T3 Pedestal")
        {
            return db.currentPlayer.merch[14].quantity;
        }
        else if (displayToModify == "Rune T1 Pedestal")
        {
            return db.currentPlayer.merch[15].quantity;
        }
        else if (displayToModify == "Rune T2 Pedestal")
        {
            return db.currentPlayer.merch[16].quantity;
        }
        else if (displayToModify == "Rune T3 Pedestal")
        {
            return db.currentPlayer.merch[17].quantity;
        }

        return -1;
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


