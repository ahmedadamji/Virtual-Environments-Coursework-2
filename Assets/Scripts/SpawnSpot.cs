using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using Ubiq.Avatars;
using Ubiq.Messaging;
using UnityEngine;

public class SpawnSpot : MonoBehaviour, INetworkComponent, INetworkObject
{
    private Color spawnSpotColor;
    public string id;
    NetworkId INetworkObject.Id => new NetworkId(id);

    private NetworkContext context;
    private bool isSpawned;

    public int SpotTaken;

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        SpotTaken = msg.IsSpotTaken;
        Debug.Log("HELLO: message from a server: " + spawnSpotColor + msg.IsSpotTaken);
    }

    
    private void Awake()
    {
        spawnSpotColor = GetComponentInParent<Light>().color;
        PlayerSpawnManager.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        isSpawned = true;
    }
    
    void Start()
    {
        context = NetworkScene.Register(this);
    }
    
    private void AddPlayer(Player player)
    {
        SpotTaken++;
        player.PlayerMaterial.color = spawnSpotColor;
        player.PlayerNumber = transform.parent.GetSiblingIndex();
        Debug.Log("HELLO: You just took a spot. Your number is " + player.PlayerNumber);
        NetworkScene networkScene = NetworkScene.FindNetworkScene(this);
        if (networkScene != null)
        {
            var avatar = networkScene.GetComponentInChildren<AvatarManager>().LocalAvatar.transform.GetChild(0);
            avatar.GetChild(1).GetComponent<MeshRenderer>().material = FindObjectOfType<PlayerSpawnManager>().PlayerMaterials[transform.parent.GetSiblingIndex()];
            avatar.GetChild(2).GetComponentInChildren<SkinnedMeshRenderer>().material = FindObjectOfType<PlayerSpawnManager>().PlayerMaterials[transform.parent.GetSiblingIndex()];
            avatar.GetChild(3).GetComponentInChildren<SkinnedMeshRenderer>().material = FindObjectOfType<PlayerSpawnManager>().PlayerMaterials[transform.parent.GetSiblingIndex()];
        }
    }

    private void RemovePlayer(Player player)
    {
        SpotTaken--;
        if (!isSpawned)
        {
            player.PlayerMaterial.color = Color.gray;
            player.PlayerNumber = -1;
            Debug.Log("HELLO: You just left a spot. Your number is " + player.PlayerNumber);
            NetworkScene networkScene = NetworkScene.FindNetworkScene(this);
            if (networkScene != null)
            {
                var avatar = networkScene.GetComponentInChildren<AvatarManager>().LocalAvatar.transform.GetChild(0);
                avatar.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
                avatar.GetChild(2).GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
                avatar.GetChild(3).GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            }
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        AddPlayer(player);
        context.SendJson(new Message(SpotTaken));
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        RemovePlayer(player);
        context.SendJson(new Message(SpotTaken));
    }

    private struct Message
    {
        public int IsSpotTaken;

        public Message(int aIsSpotTaken)
        {
            IsSpotTaken = aIsSpotTaken;
        }
    }

}
