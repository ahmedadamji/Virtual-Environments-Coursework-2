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
        playerCount = avatarManager.transform.childCount;

        SortedDictionary<string, int> values = new SortedDictionary<string, int>();
        string str = avatarManager.transform.GetChild(0).name;
        string value = str.Substring(str.Length - 17, 17);
        for (int i = 0; i < 4; i++)
        {
            string _str = avatarManager.transform.GetChild(i).name;
            string _value = str.Substring(str.Length - 17, 17);
            values[_value] = i;
        }

        SpawnPlayer(values[value]);
        
        if (OnGameStart != null) OnGameStart();
    }

    private void Awake()
    {
        roomClient = FindObjectOfType<RoomClient>();
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

    private void SpawnPlayer(int number)
    {
        spawnSpots[number].TakeSpot(FindObjectOfType<Player>());
    }

    private void Update()
    {
        if (!spawned)
        {
            spawned = true;
            if (playerCount == 4)
            {
                StartCoroutine(StartGame());
            }
            
        }
    }
}
