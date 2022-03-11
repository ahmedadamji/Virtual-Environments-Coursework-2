using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;


public class MoveAndSync : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    [HideInInspector] public Hand grasped;
    [HideInInspector] public bool locked;
    [HideInInspector] public bool shareable;
    private bool movable;
    public PlayerSpawnManager.PlayerColor Color;
    private float distance;
    public uint id;
    NetworkId INetworkObject.Id => new NetworkId(id);

    private NetworkContext context;
    
    void Start()
    {
        context = NetworkScene.Register(this);
        Player player = FindObjectOfType<Player>();
        PlayerSpawnManager.PlayerColor playerColor = player.Color;
        if (playerColor == Color)
        {
            movable = true;
            ChangeMaterials(player.mat);
        }
        else if (shareable)
        {
            movable = true;
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

    void IGraspable.Grasp(Hand controller)
    {
        //distance = Vector3.Distance(transform.position, controller.transform.parent.position);
        if (movable && !locked && grasped == null)
        {
            grasped = controller;
        }
        
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        var objTransform = transform;
        
        objTransform.position = msg.Position;
        objTransform.rotation = msg.Rotation;
    }

    public void ForceRelease()
    {
        grasped = null;
        locked = true;
    }

    void IGraspable.Release(Hand controller)
    {
        if (movable)
        {
            grasped = null;
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    

    struct Message
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public Message(Transform transform)
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }
    }
    
    void Update()
    {
        if (grasped)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            transform.parent = grasped.gameObject.transform;

            Message message = new Message(transform);
            context.SendJson(message);
        }
    }
}
