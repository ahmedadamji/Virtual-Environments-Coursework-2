using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;


public class UseAndSync : MonoBehaviour, IUseable, INetworkComponent, INetworkObject
{
    [HideInInspector] public Hand used;
    [HideInInspector] public bool isOn;

    public StateLight indicator;
    
    public uint id;
    NetworkId INetworkObject.Id => new NetworkId(id);

    private NetworkContext context;

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        indicator.ChangeState(msg.Value);
    }
    
    void Start()
    {
        context = NetworkScene.Register(this);
    }

    struct Message
    {
        public readonly bool Value;

        public Message(bool value)
        {
            Value = value;
        }
    }
    

    public void Use(Hand controller)
    {
        isOn = !isOn;
        indicator.ChangeState(isOn);
        Message message = new Message(isOn);
        context.SendJson(message);
    }

    public void UnUse(Hand controller)
    {
        
    }
}