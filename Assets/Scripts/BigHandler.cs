using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class BigHandler : MonoBehaviour, INetworkComponent, INetworkObject
{
    public Dictionary<string, Vector3> movers = new Dictionary<string, Vector3>(2);

    public int nOfMoversNeeded;
    
    private NetworkContext context;
    NetworkId INetworkObject.Id => new NetworkId(123456789);

    public void GraspMover(string id, Vector3 mover)
    {
        movers[id] = mover;
        context.SendJson(new Message(id, mover, true));
    }

    public void ReleaseMover(string id, Vector3 mover)
    {
        movers.Remove(id);
        context.SendJson(new Message(id, mover, false));
    }

    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        Debug.Log(movers.Count);
        if (movers.Count == nOfMoversNeeded)
        {
            Vector3 movement = Vector3.zero;
            foreach (var mover in movers)
            {
                // if (mover.Value.magnitude < 5)
                // {
                //     movement = Vector3.zero;
                //     break;
                // }
                movement += mover.Value;
            }

            movement /= nOfMoversNeeded;

            transform.GetComponent<Rigidbody>().velocity = movement;
        }
    }

    private struct Message
    {
        public string Id;
        public Vector3 Mover;
        public bool IsAdd;
        
        public Message(string id, Vector3 mover, bool isAdd)
        {
            Id = id;
            Mover = mover;
            IsAdd = isAdd;
        }
    }
    
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        if (msg.IsAdd)
        {
            movers[msg.Id] = msg.Mover;
        }
        else
        {
            movers.Remove(msg.Id);
        }
    }

}
