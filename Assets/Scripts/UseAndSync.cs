using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;


public class UseAndSync : MonoBehaviour, IUseable, INetworkComponent, INetworkObject
{
    [HideInInspector] public Hand used;
    [HideInInspector] public bool isOn;
    
    public PlayerSpawnManager.PlayerColor Color;
    private bool usable;
    [HideInInspector] public bool shareable;


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
        Player player = FindObjectOfType<Player>();
        PlayerSpawnManager.PlayerColor playerColor = player.Color;
        if (playerColor == Color)
        {
            usable = true;
            ChangeMaterials(player.mat);
        }
        else if (shareable)
        {
            usable = true;
            ChangeMaterials(PlayerSpawnManager.Any);
        }
        else
        {
            ChangeMaterials(PlayerSpawnManager.Black);
        }
    }
    
    void ChangeMaterials(Material material)
    {
        MeshRenderer[] children;
        children = GetComponentsInChildren<MeshRenderer>();
        foreach (var rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++) 
            { 
                mats[j] = material; 
            }
            rend.materials = mats;
        }

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
        if (usable)
        {
            isOn = !isOn;
            indicator.ChangeState(isOn);
            Message message = new Message(isOn);
            context.SendJson(message);
        }
    }

    public void UnUse(Hand controller)
    {
        
    }
}