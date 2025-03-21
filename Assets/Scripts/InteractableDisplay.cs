using System.Collections;
using UnityEngine;

public class InteractableDisplay : MonoBehaviour, InteractDisplay
{

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
    [SerializeField] GameObject displayToModify;
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

    [SerializeField] JSONDatabaseOperations db;

    /* Make sure to add display item pedestal to layer 6 and match the name of the gameobject to the name in the script. Set script onto table as non active item scripts do not run */
    public void InteractDisplay()
    {
        //Do nothing if already active as player can not buy own items
        //Do not allow display if player is out of stock of item
        //Inventory count gets decremented when item is sold
        if (displayToModify.activeInHierarchy)
        {
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
    }

    public void Update()
    {
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
        }
    }
}