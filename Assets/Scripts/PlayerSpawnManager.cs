using System;
using System.Collections;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
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
        SpawnPlayer(FindObjectOfType<Player>());
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
        Debug.Log("HELLO " + peer.UUID);
        context.SendJson(new Message(false));
        if (playerCount == 3)
        {
            context.SendJson(new Message(true));
            if (OnGameStart != null) OnGameStart.Invoke();
        }
    }
    
    private void SpawnPlayer(Player player)
    {
        spawnSpots[playerCount].TakeSpot(player);
    }

    private struct Message
    {
        public readonly bool IsEveryoneHere;

        public Message(bool isEveryoneHere)
        {
            IsEveryoneHere = isEveryoneHere;
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        if (msg.IsEveryoneHere)
        {
            StartCoroutine(StartGame());

        }
        else
        {            
            playerCount++;
        }

        Debug.Log("HELLO: Is Everyone Here: " + msg.IsEveryoneHere + ". If Not, Player Added: " + playerCount);
    }

}
