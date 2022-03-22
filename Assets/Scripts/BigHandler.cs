using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class BigHandler : MonoBehaviour, INetworkComponent, INetworkObject
{
    public HashSet<GameObject> movers = new HashSet<GameObject>();

    public int nOfMoversNeeded;
    
    private NetworkContext context;
    NetworkId INetworkObject.Id => new NetworkId(123456789);

    public void MoverReady(GameObject mover)
    {
        movers.Add(mover.gameObject);
        context.SendJson(new Message(transform.position, mover.gameObject, true));
    }
    
    public void MoverNot(GameObject mover)
    {
        movers.Remove(mover.gameObject);
        context.SendJson(new Message(transform.position, mover.gameObject, false));
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
        if (movers.Count == nOfMoversNeeded)
        {
            Vector3 movement = Vector3.zero;
            foreach (var mover in movers)
            {
                movement += mover.transform.position;
            }

            movement /= nOfMoversNeeded;

            transform.localPosition = movement;
        }
    }

    private struct Message
    {
        public Vector3 Position;
        public GameObject Movers;
        public bool IsGrasp;

        public Message(Vector3 position, GameObject movers, bool isGrasp)
        {
            Movers = movers;
            Position = position;
            IsGrasp = isGrasp;
        }
    }
    
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        if (msg.IsGrasp)
        {
            movers.Add(msg.Movers);
            transform.localPosition = msg.Position;

        }
        else
        {
            movers.Remove(msg.Movers);
        }
    }

}
