using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using Ubiq.Avatars;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
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
    
    private NetworkContext context;

    [SerializeField] private GameObject avatarManager;
    
    public static event Action OnGameStart;

    [SerializeField] private bool debugMode;

    private HashSet<string> IDs = new HashSet<string>();


    void Start()
    {
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

        SortedDictionary<string, int> values = new SortedDictionary<string, int>();
        for (int i = 0; i < 4; i++)
        {
            string str = avatarManager.transform.GetChild(i).name;
            string value = str.Substring(str.Length - 17, 17);
            values[value] = i;
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnPlayer(i);
        }

        if (OnGameStart != null) OnGameStart();
    }

    private void Awake()
    {
        roomClient = FindObjectOfType<RoomClient>();
        roomClient.OnJoinedRoom.AddListener(OnAdded);
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

    private void OnAdded(IRoom peer)
    {
        playerCount = avatarManager.transform.childCount;
        if (playerCount == 4)
        {
            StartCoroutine(StartGame());
        }
    }
    
    private void SpawnPlayer(int number)
    {
        spawnSpots[number].TakeSpot(FindObjectOfType<Player>());
    }
    
    
}
