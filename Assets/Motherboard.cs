using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherboard : MonoBehaviour
{
    public int partsReplacedCount;

    public int totalPartsNumber = 10;
    
    public void PartReplaced()
    {
        partsReplacedCount++;
        if (partsReplacedCount == totalPartsNumber)
        {
            MoveMotherboard();
        }
    }

    private void MoveMotherboard()
    {
        GetComponent<MoveAndSync>().enabled = true;
        GetComponent<AccessManager>().locked = false;
        GetComponent<AccessManager>().available = true;
        //GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Snapper>().enabled = true;
    }
}
