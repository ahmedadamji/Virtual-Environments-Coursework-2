using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using Ubiq.Avatars;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour, INetworkComponent, INetworkObject
{
    private SpawnSpot[] spawnSpots;
    private int playerCount = 0;
    private bool spawned;

    [SerializeField] private Color sharedMaterialColor;
    [SerializeField] private Color othersMaterialColor;
    public static Material SharedMaterial;
    public static Material OthersMaterial;

    private RoomClient roomClient;
    
    public string id;
    NetworkId INetworkObject.Id => new NetworkId(id);
    
    private NetworkContext context;
    
    public static event Action OnGameStart;

    [SerializeField] private bool debugMode;

    private HashSet<string> IDs = new HashSet<string>();


    void Start()
    {
        context = NetworkScene.Register(this);
        if (debugMode)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        for (int i = 3; i > 0; i--)
        {
            Debug.Log("HELLO: Game starts in " + i);
            yield return new WaitForSeconds(1);
        }

        int index = 0;
        foreach (var id in IDs)
        {
            index++;
            if (id == NetworkScene.FindNetworkScene(this).GetComponentInChildren<AvatarManager>().LocalAvatar.Id.ToString())
            {
                SpawnPlayer(FindObjectOfType<Player>(), index);
                break;
            }
        }
        
        if (OnGameStart != null) OnGameStart();
    }

    private void Awake()
    {
        roomClient = FindObjectOfType<RoomClient>();
        roomClient.OnPeerAdded.AddListener(OnAdded);
        spawnSpots = GetComponentsInChildren<SpawnSpot>();
        
        SharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = sharedMaterialColor
        };
        
        OthersMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = othersMaterialColor
        };
        
        
    }

    private void OnAdded(IPeer peer)
    {
        Debug.Log("HELLO UUID " + NetworkScene.FindNetworkScene(this).GetComponentInChildren<AvatarManager>().LocalAvatar.Id);
        //IDs.Add(NetworkScene.FindNetworkScene(this).GetComponentInChildren<AvatarManager>().LocalAvatar.Id.ToString());
        context.SendJson(new Message(NetworkScene.FindNetworkScene(this).GetComponentInChildren<AvatarManager>().LocalAvatar.Id.ToString()));
        if (IDs.Count == 3)
        {
            StartCoroutine(StartGame());
        }
    }
    
    private void SpawnPlayer(Player player, int number)
    {
        spawnSpots[number].TakeSpot(player);
    }

    private struct Message
    {
        public readonly string ID;

        public Message(string anID)
        {
            ID = anID;
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        IDs.Add(msg.ID);
    }

}
