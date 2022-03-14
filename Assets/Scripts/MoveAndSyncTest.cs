using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class MoveAndSyncTest : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    public Hand grasped;
    public string id;
    NetworkId INetworkObject.Id => new NetworkId();
    
    private NetworkContext context;
    
    public bool owner;


    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void Grasp(Hand controller)
    {
        grasped = controller;
        owner = true;
        //transform.parent = grasped.gameObject.transform;
    }

    public void Release(Hand controller)
    {
        grasped = null;
    }
    
    public struct Message
    {
        public TransformMessage transform;
        public Message(Transform transform)
        {
            this.transform = new TransformMessage(transform);
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        transform.localPosition = msg.transform.position; // The Message constructor will take the *local* properties of the passed transform.
        transform.localRotation = msg.transform.rotation;
    }
    
    void Update()
    {
        if(grasped)
        {
            transform.position = grasped.transform.position;
            transform.rotation = grasped.transform.rotation;
        }
        if(owner)
        {
            context.SendJson(new Message(transform));
        }
    }
}
