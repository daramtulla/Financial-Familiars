using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDisplay : MonoBehaviour, InteractDisplay
{
    public MovementController movementController;

    public SoundManager soundManager;
    public static bool isPlayingSound = false;

    //Merch
    [SerializeField] GameObject potionT1;
    [SerializeField] GameObject potionT2;
    [SerializeField] GameObject potionT3;

    [SerializeField] GameObject accesoryT1;
    [SerializeField] GameObject accesoryT2;
    [SerializeField] GameObject accesoryT3;

    [SerializeField] GameObject weaponT1;
    [SerializeField] GameObject weaponT2;
    [SerializeField] GameObject weaponT3;

    [SerializeField] GameObject specialT1;
    [SerializeField] GameObject specialT2;
    [SerializeField] GameObject specialT3;

    [SerializeField] GameObject runeT1;
    [SerializeField] GameObject runeT2;
    [SerializeField] GameObject runeT3;

    [SerializeField] GameObject shieldT1;
    [SerializeField] GameObject shieldT2;
    [SerializeField] GameObject shieldT3;

    //Displays
    public GameObject displayToModify;
    [SerializeField] GameObject potionT1Pedestal;
    [SerializeField] GameObject potionT2Pedestal;
    [SerializeField] GameObject potionT3Pedestal;

    [SerializeField] GameObject accesoryT1Pedestal;
    [SerializeField] GameObject accesoryT2Pedestal;
    [SerializeField] GameObject accesoryT3Pedestal;

    [SerializeField] GameObject weaponT1Pedestal;
    [SerializeField] GameObject weaponT2Pedestal;
    [SerializeField] GameObject weaponT3Pedestal;

    [SerializeField] GameObject specialT1Pedestal;
    [SerializeField] GameObject specialT2Pedestal;
    [SerializeField] GameObject specialT3Pedestal;

    [SerializeField] GameObject runeT1Pedestal;
    [SerializeField] GameObject runeT2Pedestal;
    [SerializeField] GameObject runeT3Pedestal;

    [SerializeField] GameObject shieldT1Pedestal;
    [SerializeField] GameObject shieldT2Pedestal;
    [SerializeField] GameObject shieldT3Pedestal;

    [SerializeField] GameObject poof;

    public Text interactableNameText;
    public string interactableNameTextString;

    [SerializeField] JSONDatabaseOperations db;

    private bool Active = false;
    int frames = 0;

    Dictionary<string, GameObject> solutions = new Dictionary<string, GameObject>();

    /* Make sure to add display item pedestal to layer 6 and match the name of the gameobject to the name in the script. Set script onto table as non active item scripts do not run */
    public void InteractDisplay()
    {

        //Do nothing if already active as player can not buy own items
        //Do not allow display if player is out of stock of item
        //Inventory count gets decremented when item is sold
        if (displayToModify.activeInHierarchy)
        {
            StartCoroutine(IsPlayingInteractAnimationRoutine());
        }
    }
    IEnumerator IsPlayingInteractAnimationRoutine()
    {
        if (displayToModify.name == "Potion T1 Pedestal")
        {
            Debug.Log("Breakpoint");
            if (db.currentPlayer.merch[0].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Potion T2 Pedestal")
        {
            if (db.currentPlayer.merch[1].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Potion T3 Pedestal")
        {
            if (db.currentPlayer.merch[2].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Accessory T1 Pedestal")
        {
            if (db.currentPlayer.merch[3].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Accessory T2 Pedestal")
        {
            if (db.currentPlayer.merch[4].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Accessory T3 Pedestal")
        {
            if (db.currentPlayer.merch[5].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Weapon T1 Pedestal")
        {
            if (db.currentPlayer.merch[6].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Weapon T2 Pedestal")
        {
            if (db.currentPlayer.merch[7].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Weapon T3 Pedestal")
        {
            if (db.currentPlayer.merch[8].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Special T1 Pedestal")
        {
            if (db.currentPlayer.merch[9].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Special T2 Pedestal")
        {
            if (db.currentPlayer.merch[10].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Special T3 Pedestal")
        {
            if (db.currentPlayer.merch[11].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Shield T1 Pedestal")
        {
            if (db.currentPlayer.merch[15].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Shield T2 Pedestal")
        {
            if (db.currentPlayer.merch[16].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Shield T3 Pedestal")
        {
            if (db.currentPlayer.merch[17].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Rune T1 Pedestal")
        {
            if (db.currentPlayer.merch[12].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Rune T2 Pedestal")
        {
            if (db.currentPlayer.merch[13].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }
        else if (displayToModify.name == "Rune T3 Pedestal")
        {
            if (db.currentPlayer.merch[14].quantity > 0)
            {
                movementController.PlayInteractAnimation();
            }
        }

        yield return new WaitForSeconds(0.3f);

        Debug.Log("Interacting with Display");
        if (displayToModify.name == "Potion T1 Pedestal")
        {
            Debug.Log("Breakpoint");
            if (db.currentPlayer.merch[0].quantity > 0)
            {
                Debug.Log("Placed Potion T1");
                potionT1.SetActive(true);

                db.currentPlayer.active[0] = 1;
            }
        }
        else if (displayToModify.name == "Potion T2 Pedestal")
        {
            if (db.currentPlayer.merch[1].quantity > 0)
            {
                potionT2.SetActive(true);
                db.currentPlayer.active[1] = 1;
            }
        }
        else if (displayToModify.name == "Potion T3 Pedestal")
        {
            if (db.currentPlayer.merch[2].quantity > 0)
            {
                potionT3.SetActive(true);
                db.currentPlayer.active[2] = 1;
            }
        }
        else if (displayToModify.name == "Accessory T1 Pedestal")
        {
            if (db.currentPlayer.merch[3].quantity > 0)
            {
                accesoryT1.SetActive(true);
                db.currentPlayer.active[3] = 1;
            }
        }
        else if (displayToModify.name == "Accessory T2 Pedestal")
        {
            if (db.currentPlayer.merch[4].quantity > 0)
            {
                accesoryT2.SetActive(true);
                db.currentPlayer.active[4] = 1;
            }
        }
        else if (displayToModify.name == "Accessory T3 Pedestal")
        {
            if (db.currentPlayer.merch[5].quantity > 0)
            {
                accesoryT3.SetActive(true);
                db.currentPlayer.active[5] = 1;
            }
        }
        else if (displayToModify.name == "Weapon T1 Pedestal")
        {
            if (db.currentPlayer.merch[6].quantity > 0)
            {
                weaponT1.SetActive(true);
                db.currentPlayer.active[6] = 1;
            }
        }
        else if (displayToModify.name == "Weapon T2 Pedestal")
        {
            if (db.currentPlayer.merch[7].quantity > 0)
            {
                weaponT2.SetActive(true);
                db.currentPlayer.active[7] = 1;
            }
        }
        else if (displayToModify.name == "Weapon T3 Pedestal")
        {
            if (db.currentPlayer.merch[8].quantity > 0)
            {
                weaponT3.SetActive(true);
                db.currentPlayer.active[8] = 1;
            }
        }
        else if (displayToModify.name == "Special T1 Pedestal")
        {
            if (db.currentPlayer.merch[9].quantity > 0)
            {
                specialT1.SetActive(true);
                db.currentPlayer.active[9] = 1;
            }
        }
        else if (displayToModify.name == "Special T2 Pedestal")
        {
            if (db.currentPlayer.merch[10].quantity > 0)
            {
                specialT2.SetActive(true);
                db.currentPlayer.active[10] = 1;
            }
        }
        else if (displayToModify.name == "Special T3 Pedestal")
        {
            if (db.currentPlayer.merch[11].quantity > 0)
            {
                specialT3.SetActive(true);
                db.currentPlayer.active[11] = 1;
            }
        }
        else if (displayToModify.name == "Shield T1 Pedestal")
        {
            if (db.currentPlayer.merch[15].quantity > 0)
            {
                shieldT1.SetActive(true);
                db.currentPlayer.active[15] = 1;
            }
        }
        else if (displayToModify.name == "Shield T2 Pedestal")
        {
            if (db.currentPlayer.merch[16].quantity > 0)
            {
                shieldT2.SetActive(true);
                db.currentPlayer.active[16] = 1;
            }
        }
        else if (displayToModify.name == "Shield T3 Pedestal")
        {
            if (db.currentPlayer.merch[17].quantity > 0)
            {
                shieldT3.SetActive(true);
                db.currentPlayer.active[17] = 1;
            }
        }
        else if (displayToModify.name == "Rune T1 Pedestal")
        {
            if (db.currentPlayer.merch[12].quantity > 0)
            {
                runeT1.SetActive(true);
                db.currentPlayer.active[12] = 1;
            }
        }
        else if (displayToModify.name == "Rune T2 Pedestal")
        {
            if (db.currentPlayer.merch[13].quantity > 0)
            {
                runeT2.SetActive(true);
                db.currentPlayer.active[13] = 1;
            }
        }
        else if (displayToModify.name == "Rune T3 Pedestal")
        {
            if (db.currentPlayer.merch[14].quantity > 0)
            {
                runeT3.SetActive(true);
                db.currentPlayer.active[14] = 1;
            }
        }
    }

    void pastepoof(GameObject point)
    {
        Instantiate(poof, point.transform.position + Vector3.up * 0.85f, poof.transform.rotation);
        if (!isPlayingSound)
        //Prevents itemPlaced sound to stack due to V key being pressed and an item being placed on every pedestal
        {
            soundManager.soundAudioSource.PlayOneShot(soundManager.itemPlaced, 1.0f);
            isPlayingSound = true;
            StartCoroutine(IsPlayingSoundRoutine());
        }
    }

    IEnumerator IsPlayingSoundRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        isPlayingSound = false;
    }


    void mapper()
    {
        solutions.Add(potionT1Pedestal.gameObject.name, potionT1);
        solutions.Add(potionT2Pedestal.gameObject.name, potionT2);
        solutions.Add(potionT3Pedestal.gameObject.name, potionT3);

        solutions.Add(accesoryT1Pedestal.gameObject.name, accesoryT1);
        solutions.Add(accesoryT2Pedestal.gameObject.name, accesoryT2);
        solutions.Add(accesoryT3Pedestal.gameObject.name, accesoryT3);

        solutions.Add(weaponT1Pedestal.gameObject.name, weaponT1);
        solutions.Add(weaponT2Pedestal.gameObject.name, weaponT2);
        solutions.Add(weaponT3Pedestal.gameObject.name, weaponT3);

        solutions.Add(specialT1Pedestal.gameObject.name, specialT1);
        solutions.Add(specialT2Pedestal.gameObject.name, specialT2);
        solutions.Add(specialT3Pedestal.gameObject.name, specialT3);

        solutions.Add(runeT1Pedestal.gameObject.name, runeT1);
        solutions.Add(runeT2Pedestal.gameObject.name, runeT2);
        solutions.Add(runeT3Pedestal.gameObject.name, runeT3);

        solutions.Add(shieldT1Pedestal.gameObject.name, shieldT1);
        solutions.Add(shieldT2Pedestal.gameObject.name, shieldT2);
        solutions.Add(shieldT3Pedestal.gameObject.name, shieldT3);
    }

    public void Update()
    {
        if (solutions.Count == 0)
        {
            mapper();
            Active = false;
        }
        if (solutions[displayToModify.name].activeInHierarchy && !Active && frames != 0)
        {
            Active = true;
            //Debug.Log(solutions[displayToModify.name].name);
            pastepoof(displayToModify);
        }
        else if (!solutions[displayToModify.name].activeInHierarchy)
        {
            Active = false;
        }


        //Check to see if display needs to be set inactive when item is sold
        for (int i = 0; i < 18; i++)
        {

            if (db.currentPlayer.active[i] != 1)
            {
                switch (i)
                {
                    case 0:
                        potionT1.SetActive(false);
                        break;
                    case 1:
                        potionT2.SetActive(false);
                        break;
                    case 2:
                        potionT3.SetActive(false);
                        break;
                    case 3:
                        accesoryT1.SetActive(false);
                        break;
                    case 4:
                        accesoryT2.SetActive(false);
                        break;
                    case 5:
                        accesoryT3.SetActive(false);
                        break;
                    case 6:
                        weaponT1.SetActive(false);
                        break;
                    case 7:
                        weaponT2.SetActive(false);
                        break;
                    case 8:
                        weaponT3.SetActive(false);
                        break;
                    case 9:
                        specialT1.SetActive(false);
                        break;
                    case 10:
                        specialT2.SetActive(false);
                        break;
                    case 11:
                        specialT3.SetActive(false);
                        break;
                    case 12:
                        runeT1.SetActive(false);
                        break;
                    case 13:
                        runeT2.SetActive(false);
                        break;
                    case 14:
                        runeT3.SetActive(false);
                        break;
                    case 15:
                        shieldT1.SetActive(false);
                        break;
                    case 16:
                        shieldT2.SetActive(false);
                        break;
                    case 17:
                        shieldT3.SetActive(false);
                        break;
                }
            }
            else if (db.currentPlayer.active[i] == 1)
            {
                switch (i)
                {
                    case 0:
                        potionT1.SetActive(true);
                        break;
                    case 1:
                        potionT2.SetActive(true);
                        break;
                    case 2:
                        potionT3.SetActive(true);
                        break;
                    case 3:
                        accesoryT1.SetActive(true);
                        break;
                    case 4:
                        accesoryT2.SetActive(true);
                        break;
                    case 5:
                        accesoryT3.SetActive(true);
                        break;
                    case 6:
                        weaponT1.SetActive(true);
                        break;
                    case 7:
                        weaponT2.SetActive(true);
                        break;
                    case 8:
                        weaponT3.SetActive(true);
                        break;
                    case 9:
                        specialT1.SetActive(true);
                        break;
                    case 10:
                        specialT2.SetActive(true);
                        break;
                    case 11:
                        specialT3.SetActive(true);
                        break;
                    case 12:
                        runeT1.SetActive(true);
                        break;
                    case 13:
                        runeT2.SetActive(true);
                        break;
                    case 14:
                        runeT3.SetActive(true);
                        break;
                    case 15:
                        shieldT1.SetActive(true);
                        break;
                    case 16:
                        shieldT2.SetActive(true);
                        break;
                    case 17:
                        shieldT3.SetActive(true);
                        break;
                }
            }
        }

        if (frames < 1)
        {
            frames++;
        }


    }
}