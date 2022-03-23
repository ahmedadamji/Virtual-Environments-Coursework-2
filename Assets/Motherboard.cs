using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;

public class Motherboard : MonoBehaviour, INetworkComponent, INetworkObject
{
    public int partsReplacedCount;

    public int totalPartsNumber = 10;
    
    NetworkId INetworkObject.Id => new NetworkId(3457483);

    private NetworkContext context;
    
    void Start()
    {
        context = NetworkScene.Register(this);
    }
    public void PartReplaced()
    {
        partsReplacedCount++;
        if (partsReplacedCount == totalPartsNumber)
        {
            MoveMotherboard();
            context.SendJson(new Message(partsReplacedCount));
        }
    }

    struct Message
    {
        public int PartsReplacedCount;

        public Message(int totalParts)
        {
            PartsReplacedCount = totalParts;
        }
    }

    private void MoveMotherboard()
    {
        GetComponent<MoveAndSync>().enabled = true;
        GetComponent<AccessManager>().locked = false;
        GetComponent<AccessManager>().available = true;
        GetComponent<AccessManager>().shareable = true;
        //GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Snapper>().enabled = true;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        partsReplacedCount = msg.PartsReplacedCount;
    } 
}
