using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

using PlayerNumber = System.Int32;

public class MoveAndSync : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    [HideInInspector] public Hand grasped;
    private AccessManager accessManager;
    private float distance;
    public string id;
    NetworkId INetworkObject.Id => new NetworkId(id);

    private NetworkContext context;
    
    private HandController[] handControllers;

    private void Awake()
    {
        accessManager = GetComponent<AccessManager>();
        handControllers = FindObjectsOfType<HandController>();
    }
    
    void Start()
    {
        context = NetworkScene.Register(this);
    }

    void IGraspable.Grasp(Hand controller)
    {
        if (accessManager.available && !accessManager.locked && grasped == null)
        {
            grasped = controller;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        transform.localPosition = msg.Transform.position;
        transform.localRotation = msg.Transform.rotation;
    }

    public void ForceRelease()
    {
        grasped = null;
        accessManager.locked = true;
        foreach (var handController in handControllers)
        {
            handController.Vibrate(0.3f, 0.2f);
        }
        
    }

    void IGraspable.Release(Hand controller)
    {
        if (grasped)
        {
            grasped = null;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    

    public struct Message
    {
        public TransformMessage Transform;
        public Message(Transform transform)
        {
            this.Transform = new TransformMessage(transform);
        }
    }
    
    void Update()
    {
        if(grasped)
        {
            transform.position = grasped.transform.position;
            transform.rotation = grasped.transform.rotation;
            context.SendJson(new Message(transform));
        }
    }
}
