using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class BigHandler : MonoBehaviour, INetworkComponent, INetworkObject
{
    public List<Vector3> movers = new List<Vector3>();

    public int nOfMoversNeeded;
    
    private NetworkContext context;
    NetworkId INetworkObject.Id => new NetworkId(123456789);

    public void MoverReady(Vector3 mover, int index)
    {
        movers[index] = mover;
        context.SendJson(new Message(mover, index));
    }

    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    private void Update()
    {
        Move();
        movers.Clear();
    }

    public void Move()
    {
        if (movers.Count == nOfMoversNeeded)
        {
            Vector3 movement = Vector3.zero;
            foreach (var mover in movers)
            {
                if (mover != Vector3.zero)
                {
                    movement += mover;
                }
            }

            movement /= movers.Count;

            transform.localPosition = movement;
        }
    }

    private struct Message
    {
        public Vector3 Position;
        public int Index;
        
        public Message(Vector3 position, int index)
        {
            Position = position;
            Index = index;
        }
    }
    
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        movers[msg.Index] = msg.Position;
    }

}
