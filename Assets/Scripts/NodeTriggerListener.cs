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
    CustomerMovement cm;

    RandomGenNum rnd;



    public void OnTriggerEnter(Collider col)
    {
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
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;
        cm.linkTable[obj].nextNode = cm.node2;
    }
    public void ReachNode2(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;
        cm.linkTable[obj].nextNode = cm.node3;
    }
    public void ReachNode3(Collider col)
    {
        GameObject obj = col.gameObject;
        int path = rnd.GetBinary();

        if (cm.linkTable[obj].itemToBuy % 3 == 0 && col == trigger4a
        || cm.linkTable[obj].itemToBuy % 2 == 0 && col == trigger4a && path == 0)
        {
            cm.linkTable[obj].nextNode = cm.node4a;
            cm.linkTable[obj].nodeNum++;
        }
        else if (cm.linkTable[obj].itemToBuy % 3 == 1 && col == trigger4b
        || cm.linkTable[obj].itemToBuy % 2 == 0 && col == trigger4b)
        {
            cm.linkTable[obj].nextNode = cm.node4b;
            cm.linkTable[obj].nodeNum++;
        }

    }
    public void ReachNode4(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;

        if (col == trigger4a && cm.linkTable[obj].nextNode == cm.node4a)
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
        else if (col == trigger4b && cm.linkTable[obj].nextNode == cm.node4b)
        {
            //Go on path to 6 and skip 5s
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
    public void ReachNode5(Collider col)
    {
        GameObject obj = col.gameObject;


        if (cm.linkTable[obj].nextNode == cm.node5a && col == trigger5a)
        {
            //Skip node 6 path
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 18:
                    cm.linkTable[obj].nextNode = cm.nodeT3Shield;
                    break;
                case 17:
                    cm.linkTable[obj].nextNode = cm.nodeT2Shield;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node5b && col == trigger5b)
        {
            //Skip node 6 path
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 15:
                    cm.linkTable[obj].nextNode = cm.nodeT3Rune;
                    break;
                case 14:
                    cm.linkTable[obj].nextNode = cm.nodeT2Rune;
                    break;
            }

        }
        else if (cm.linkTable[obj].nextNode == cm.node5c && col == trigger5c)
        {
            //Skip node 6 path
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 9:
                    cm.linkTable[obj].nextNode = cm.nodeT3Weapon;
                    break;
                case 8:
                    cm.linkTable[obj].nextNode = cm.nodeT2Weapon;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node5d && col == trigger5d)
        {
            //Skip node 6 path
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 6:
                    cm.linkTable[obj].nextNode = cm.nodeT3Accessory;
                    break;
                case 5:
                    cm.linkTable[obj].nextNode = cm.nodeT2Accessory;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node5e && col == trigger5e)
        {
            //Skip node 6 path
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 12:
                    cm.linkTable[obj].nextNode = cm.nodeT3Special;
                    break;
                case 11:
                    cm.linkTable[obj].nextNode = cm.nodeT2Special;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node5f && col == trigger5f)
        {
            //Skip node 6 path
            cm.linkTable[obj].nodeNum++;
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
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
    public void ReachNode6(Collider col)
    {
        GameObject obj = col.gameObject;


        if (cm.linkTable[obj].nextNode == cm.node6a && col == trigger6a)
        {
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 16:
                    cm.linkTable[obj].nextNode = cm.nodeT1Shield;
                    break;
                case 17:
                    cm.linkTable[obj].nextNode = cm.nodeT2Shield;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node6b && col == trigger6b)
        {
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 13:
                    cm.linkTable[obj].nextNode = cm.nodeT1Rune;
                    break;
                case 14:
                    cm.linkTable[obj].nextNode = cm.nodeT2Rune;
                    break;
            }

        }
        else if (cm.linkTable[obj].nextNode == cm.node6c && col == trigger6c)
        {
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 7:
                    cm.linkTable[obj].nextNode = cm.nodeT1Weapon;
                    break;
                case 8:
                    cm.linkTable[obj].nextNode = cm.nodeT2Weapon;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node6d && col == trigger6d)
        {
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 4:
                    cm.linkTable[obj].nextNode = cm.nodeT1Accessory;
                    break;
                case 5:
                    cm.linkTable[obj].nextNode = cm.nodeT2Accessory;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node6e && col == trigger6e)
        {
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 11:
                    cm.linkTable[obj].nextNode = cm.nodeT1Special;
                    break;
                case 10:
                    cm.linkTable[obj].nextNode = cm.nodeT2Special;
                    break;
            }
        }
        else if (cm.linkTable[obj].nextNode == cm.node6f && col == trigger6f)
        {
            cm.linkTable[obj].nodeNum++;

            switch (cm.linkTable[obj].nodeNum)
            {
                case 2:
                    cm.linkTable[obj].nextNode = cm.nodeT1Potion;
                    break;
                case 1:
                    cm.linkTable[obj].nextNode = cm.nodeT2Potion;
                    break;
            }
        }
    }
    public void ReachNodeItem(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;

        switch (cm.linkTable[obj].itemToBuy)
        {
            case 18:
                if (col != triggerItemT3Shield) { return; }
                break;
            case 17:
                if (col != triggerItemT2Shield) { return; }
                break;
            case 16:
                if (col != triggerItemT1Shield) { return; }
                break;
            case 15:
                if (col != triggerItemT3Rune) { return; }
                break;
            case 14:
                if (col != triggerItemT2Rune) { return; }
                break;
            case 13:
                if (col != triggerItemT1Rune) { return; }
                break;
            case 12:
                if (col != triggerItemT3Weapon) { return; }
                break;
            case 11:
                if (col != triggerItemT2Weapon) { return; }
                break;
            case 10:
                if (col != triggerItemT1Weapon) { return; }
                break;
            case 9:
                if (col != triggerItemT3Accessory) { return; }
                break;
            case 8:
                if (col != triggerItemT2Accessory) { return; }
                break;
            case 7:
                if (col != triggerItemT1Accessory) { return; }

                break;
            case 6:
                if (col != triggerItemT3Special) { return; }
                break;
            case 5:
                if (col != triggerItemT2Special) { return; }
                break;
            case 4:
                if (col != triggerItemT1Special) { return; }
                break;
            case 3:
                if (col != triggerItemT3Potion) { return; }
                break;
            case 2:
                if (col != triggerItemT3Potion) { return; }
                break;
            case 1:
                if (col != triggerItemT3Potion) { return; }
                break;
        }

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
    }
    public void ReachNode7(Collider col)
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
    public void ReachNodeReg(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;
        cm.linkTable[obj].nextNode = cm.node8;
    }
    public void ReachNode8(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;
        cm.linkTable[obj].nextNode = cm.node9;
    }
    public void ReachNode9(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable[obj].nodeNum++;
        cm.linkTable[obj].nextNode = cm.endNode;
    }
    public void ReachNodeEnd(Collider col)
    {
        GameObject obj = col.gameObject;
        cm.linkTable.Remove(obj);
    }
}