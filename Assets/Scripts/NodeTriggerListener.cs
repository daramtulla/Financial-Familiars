using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Polybrush;
using UnityEngine.SceneManagement;

public class TriggerListener : MonoBehaviour
{
    public Collider trigger1;
    public Collider trigger2;
    public Collider trigger3;
    public Collider trigger4a;
    public Collider trigger4b;
    public Collider trigger5a;
    public Collider trigger5b;
    public Collider trigger5c;
    public Collider trigger5d;
    public Collider trigger5e;
    public Collider trigger5f;
    public Collider trigger6a;
    public Collider trigger6b;
    public Collider trigger6c;
    public Collider trigger6d;
    public Collider trigger6e;
    public Collider trigger6f;
    public Collider triggerItemT3Shield;
    public Collider triggerItemT2Shield;
    public Collider triggerItemT1Shield;
    public Collider triggerItemT3Rune;
    public Collider triggerItemT2Rune;
    public Collider triggerItemT1Rune;

    public Collider triggerItemT3Potion;
    public Collider triggerItemT2Potion;
    public Collider triggerItemT1Potion;

    public Collider triggerItemT3Accessory;
    public Collider triggerItemT2Accessory;
    public Collider triggerItemT1Accessory;

    public Collider triggerItemT3Special;
    public Collider triggerItemT2Special;
    public Collider triggerItemT1Special;

    public Collider triggerItemT3Weapon;
    public Collider triggerItemT2Weapon;
    public Collider triggerItemT1Weapon;
    public Collider trigger7a;
    public Collider trigger7b;
    public Collider trigger7c;
    public Collider trigger7d;
    public Collider trigger7e;
    public Collider trigger7f;
    public Collider triggerReg1;

    public Collider triggerReg2;
    public Collider trigger8;
    public Collider trigger9;
    public Collider triggerEnd;

    //
    public CustomerMovement cm;

    public CustomerManager custMan;

    public RandomGenNum rnd = new RandomGenNum();

    [SerializeField] Boolean debug;

    public void OnTriggerEnter(Collider col)
    {
        if (debug) { Debug.Log("Obj Id: " + col.gameObject.GetInstanceID()); }

        if (col.gameObject.tag == "Player")
        {
            return;
        }

        if (debug) { Debug.Log("NodeNum: " + cm.linkTable[col.gameObject].nodeNum); }

        if (debug) { Debug.Log("NextNode: " + cm.linkTable[col.gameObject].nextNode); }

        switch (cm.linkTable[col.gameObject].nodeNum)
        {
            case 1:
                ReachNode1(col);
                break;
            case 2:
                ReachNode2(col);
                break;
            case 3:
                ReachNode3(col);
                break;
            case 4:
                ReachNode4(col);
                break;
            case 5:
                ReachNode5(col);
                break;
            case 6:
                ReachNode6(col);
                break;
            case 7:
                ReachNodeItem(col);
                break;
            case 8:
                ReachNode7(col);
                break;
            case 9:
                ReachNodeReg(col);
                break;
            case 10:
                ReachNode8(col);
                break;
            case 11:
                ReachNode9(col);
                break;
            case 12:
                ReachNodeEnd(col);
                break;
            default:
                Debug.Log("Invalid node num");
                break;
        }
    }

    public void ReachNode1(Collider col)
    {
        if (this.name == "Node 1")
        {
            GameObject obj = col.gameObject;
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nextNode = cm.node2;
        }

    }
    public void ReachNode2(Collider col)
    {
        if (this.name == "Node 2")
        {
            GameObject obj = col.gameObject;
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nextNode = cm.node3;
        }
    }
    public void ReachNode3(Collider col)
    {
        if (this.name == "Node 3")
        {

            GameObject obj = col.gameObject;
            int path = rnd.GetBinary();

            if (cm.linkTable[obj].itemToBuy % 3 == 0
            || cm.linkTable[obj].itemToBuy % 3 == 2 && path == 0)
            {
                cm.linkTable[obj].nextNode = cm.node4a;
                cm.linkTable[obj].nodeNum++;
            }
            else if (cm.linkTable[obj].itemToBuy % 3 == 1
            || cm.linkTable[obj].itemToBuy % 3 == 2)
            {
                cm.linkTable[obj].nextNode = cm.node4b;
                cm.linkTable[obj].nodeNum++;
            }
        }



    }
    public void ReachNode4(Collider col)
    {


        GameObject obj = col.gameObject;

        if (this.name == "Node 4a" || this.name == "Node 4b")
        {

            Debug.Log(this.name + " " + cm.linkTable[obj].nextNode);

            if (cm.linkTable[obj].nodeNum != 4)
            {
                return;
            }

            if (this.name == "Node 4a" && cm.linkTable[obj].nextNode == cm.node4a)
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 18:
                        cm.linkTable[obj].nextNode = cm.node5a;
                        break;
                    case 15:
                        cm.linkTable[obj].nextNode = cm.node5b;
                        break;
                    case 12:
                        cm.linkTable[obj].nextNode = cm.node5c;
                        break;
                    case 9:
                        cm.linkTable[obj].nextNode = cm.node5d;
                        break;
                    case 6:
                        cm.linkTable[obj].nextNode = cm.node5e;
                        break;
                    case 3:
                        cm.linkTable[obj].nextNode = cm.node5f;
                        break;
                    case 17:
                        cm.linkTable[obj].nextNode = cm.node5a;
                        break;
                    case 14:
                        cm.linkTable[obj].nextNode = cm.node5b;
                        break;
                    case 11:
                        cm.linkTable[obj].nextNode = cm.node5c;
                        break;
                    case 8:
                        cm.linkTable[obj].nextNode = cm.node5d;
                        break;
                    case 5:
                        cm.linkTable[obj].nextNode = cm.node5e;
                        break;
                    case 2:
                        cm.linkTable[obj].nextNode = cm.node5f;
                        break;
                }
            }
            else if (this.name == "Node 4b" && cm.linkTable[obj].nextNode == cm.node4b)
            {

                //Go on path to 6 and skip 5s
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 16:
                        cm.linkTable[obj].nextNode = cm.node6a;
                        break;
                    case 13:
                        cm.linkTable[obj].nextNode = cm.node6b; ;
                        break;
                    case 10:
                        cm.linkTable[obj].nextNode = cm.node6c;
                        break;
                    case 7:
                        cm.linkTable[obj].nextNode = cm.node6d;
                        break;
                    case 4:
                        cm.linkTable[obj].nextNode = cm.node6e;
                        break;
                    case 1:
                        cm.linkTable[obj].nextNode = cm.node6f;
                        break;
                    case 17:
                        cm.linkTable[obj].nextNode = cm.node6a;
                        break;
                    case 14:
                        cm.linkTable[obj].nextNode = cm.node6b;
                        break;
                    case 11:
                        cm.linkTable[obj].nextNode = cm.node6c;
                        break;
                    case 8:
                        cm.linkTable[obj].nextNode = cm.node6d;
                        break;
                    case 5:
                        cm.linkTable[obj].nextNode = cm.node6e;
                        break;
                    case 2:
                        cm.linkTable[obj].nextNode = cm.node6f;
                        break;
                }
            }
        }
    }
    public void ReachNode5(Collider col)
    {
        GameObject obj = col.gameObject;

        if (this.name == "Node 5a" || this.name == "Node 5b" || this.name == "Node 5c" || this.name == "Node 5d" || this.name == "Node 5e" || this.name == "Node 5f")
        {

            if (cm.linkTable[obj].nextNode == cm.node5a && this.name == "Node 5a")
            {
                //Skip node 6 path
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 18:
                        cm.linkTable[obj].nextNode = cm.nodeT3Shield;
                        break;
                    case 17:
                        cm.linkTable[obj].nextNode = cm.nodeT2Shield;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node5b && this.name == "Node 5b")
            {
                //Skip node 6 path
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 15:
                        cm.linkTable[obj].nextNode = cm.nodeT3Rune;
                        break;
                    case 14:
                        cm.linkTable[obj].nextNode = cm.nodeT2Rune;
                        break;
                }

            }
            else if (cm.linkTable[obj].nextNode == cm.node5c && this.name == "Node 5c")
            {
                //Skip node 6 path
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 12:
                        cm.linkTable[obj].nextNode = cm.nodeT3Special;
                        break;
                    case 11:
                        cm.linkTable[obj].nextNode = cm.nodeT2Special;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node5d && this.name == "Node 5d")
            {
                //Skip node 6 path
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 9:
                        cm.linkTable[obj].nextNode = cm.nodeT3Weapon;
                        break;
                    case 8:
                        cm.linkTable[obj].nextNode = cm.nodeT2Weapon;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node5e && this.name == "Node 5e")
            {
                //Skip node 6 path
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 6:
                        cm.linkTable[obj].nextNode = cm.nodeT3Accessory;
                        break;
                    case 5:
                        cm.linkTable[obj].nextNode = cm.nodeT2Accessory;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node5f && this.name == "Node 5f")
            {
                //Skip node 6 path
                cm.linkTable[obj].nodeNum++;
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 3:
                        cm.linkTable[obj].nextNode = cm.nodeT3Potion;
                        break;
                    case 2:
                        cm.linkTable[obj].nextNode = cm.nodeT2Potion;
                        break;
                }
            }
        }
    }
    public void ReachNode6(Collider col)
    {
        GameObject obj = col.gameObject;

        if (this.name == "Node 6a" || this.name == "Node 6b" || this.name == "Node 6c" || this.name == "Node 6d" || this.name == "Node 6e" || this.name == "Node 6f")
        {

            if (cm.linkTable[obj].nextNode == cm.node6a && this.name == "Node 6a")
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 16:
                        cm.linkTable[obj].nextNode = cm.nodeT1Shield;
                        break;
                    case 17:
                        cm.linkTable[obj].nextNode = cm.nodeT2Shield;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node6b && this.name == "Node 6b")
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 13:
                        cm.linkTable[obj].nextNode = cm.nodeT1Rune;
                        break;
                    case 14:
                        cm.linkTable[obj].nextNode = cm.nodeT2Rune;
                        break;
                }

            }
            else if (cm.linkTable[obj].nextNode == cm.node6c && this.name == "Node 6c")
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 10:
                        cm.linkTable[obj].nextNode = cm.nodeT1Special;
                        break;
                    case 11:
                        cm.linkTable[obj].nextNode = cm.nodeT2Special;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node6d && this.name == "Node 6d")
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 7:
                        cm.linkTable[obj].nextNode = cm.nodeT1Weapon;
                        break;
                    case 8:
                        cm.linkTable[obj].nextNode = cm.nodeT2Weapon;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node6e && this.name == "Node 6e")
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 4:
                        cm.linkTable[obj].nextNode = cm.nodeT1Accessory;
                        break;
                    case 5:
                        cm.linkTable[obj].nextNode = cm.nodeT2Accessory;
                        break;
                }
            }
            else if (cm.linkTable[obj].nextNode == cm.node6f && this.name == "Node 6f")
            {
                cm.linkTable[obj].nodeNum++;

                switch (cm.linkTable[obj].itemToBuy)
                {
                    case 1:
                        cm.linkTable[obj].nextNode = cm.nodeT1Potion;
                        break;
                    case 2:
                        cm.linkTable[obj].nextNode = cm.nodeT2Potion;
                        break;
                }
            }
        }
    }
    public void ReachNodeItem(Collider col)
    {
        GameObject obj = col.gameObject;
        //obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Interact_trig");

        if (this.name == triggerItemT3Shield.name ||
    this.name == triggerItemT2Shield.name ||
    this.name == triggerItemT1Shield.name ||
    this.name == triggerItemT3Rune.name ||
    this.name == triggerItemT2Rune.name ||
    this.name == triggerItemT1Rune.name ||
    this.name == triggerItemT3Potion.name ||
    this.name == triggerItemT2Potion.name ||
    this.name == triggerItemT1Potion.name ||
    this.name == triggerItemT3Accessory.name ||
    this.name == triggerItemT2Accessory.name ||
    this.name == triggerItemT1Accessory.name ||
    this.name == triggerItemT3Special.name ||
    this.name == triggerItemT2Special.name ||
    this.name == triggerItemT1Special.name ||
    this.name == triggerItemT3Weapon.name ||
    this.name == triggerItemT2Weapon.name ||
    this.name == triggerItemT1Weapon.name)
        {

            switch (cm.linkTable[obj].itemToBuy)
            {
                case 18:
                case 17:
                case 16:
                    cm.linkTable[obj].nextNode = cm.node7a;
                    break;
                case 15:
                case 14:
                case 13:
                    cm.linkTable[obj].nextNode = cm.node7b;
                    break;
                case 12:
                case 11:
                case 10:
                    cm.linkTable[obj].nextNode = cm.node7c;
                    break;
                case 9:
                case 8:
                case 7:
                    cm.linkTable[obj].nextNode = cm.node7d;
                    break;
                case 6:
                case 5:
                case 4:
                    cm.linkTable[obj].nextNode = cm.node7e;
                    break;
                case 3:
                case 2:
                case 1:
                    cm.linkTable[obj].nextNode = cm.node7f;
                    break;
            }

            cm.linkTable[obj].nodeNum++;
        }
    }
    public void ReachNode7(Collider col)
    {
        if (this.name == "Node 7a" || this.name == "Node 7b" || this.name == "Node 7c" || this.name == "Node 7d" || this.name == "Node 7e" || this.name == "Node 7f")
        {
            GameObject obj = col.gameObject;
            cm.linkTable[obj].nodeNum++;

            if (rnd.GetBinary() == 0)
            {
                cm.linkTable[obj].nextNode = cm.nodeReg1;
            }
            else
            {
                cm.linkTable[obj].nextNode = cm.nodeReg2;
            }
        }

    }
    public void ReachNodeReg(Collider col)
    {
        if (this.name == "Node Reg1" || this.name == "Node Reg2")
        {
            GameObject obj = col.gameObject;
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nextNode = cm.node8;
        }
    }
    public void ReachNode8(Collider col)
    {
        if (this.name == "Node 8")
        {
            GameObject obj = col.gameObject;
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nextNode = cm.node9;
        }
    }
    public void ReachNode9(Collider col)
    {
        if (this.name == "Node 9")
        {
            GameObject obj = col.gameObject;
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nextNode = cm.endNode;
        }
    }
    public void ReachNodeEnd(Collider col)
    {
        if (this.name == "End Node")
        {
            GameObject obj = col.gameObject;
            int merchID = cm.linkTable[obj].itemToBuy;
            cm.linkTable.Remove(obj);
            bool additionalCust = false;

            foreach (var pair in cm.linkTable)
            {
                if (merchID == pair.Value.itemToBuy)
                {
                    additionalCust = true;
                }
            }

            if (additionalCust)
            {
                custMan.customerReached[merchID - 1] = 1;
            }
            else
            {
                custMan.customerReached[merchID - 1] = 0;
            }
            Destroy(obj);


        }
    }
}