using System;
using System.Collections;
using System.Linq;
using Ubiq.Messaging;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private SpawnSpot[] spawnSpots;
    private bool isSpawned;

    [SerializeField] private Color sharedMaterialColor;
    [SerializeField] private Color othersMaterialColor;
    public static Material SharedMaterial;
    public static Material OthersMaterial;
    public Material[] PlayerMaterials;
    
    public string id;
    
    private NetworkContext context;

    [SerializeField] private GameObject avatarManager;
    
    public static event Action OnGameStart;

    [SerializeField] private bool debugMode;

    
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
            yield return new WaitForSeconds(.1f);
        }

        if (OnGameStart != null) OnGameStart();
    }

    private void Awake()
    {
        spawnSpots = GetComponentsInChildren<SpawnSpot>(false);
        
        SharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = sharedMaterialColor
        };
        
        OthersMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = othersMaterialColor
        };
    }


    private void Update()
    {
        if (!isSpawned)
        {
            int playerCount = avatarManager.transform.childCount;

            if (playerCount == 4)
            {
                if (spawnSpots.All(ss => ss.SpotTaken == 1))
                {
                    isSpawned = true;
                    StartCoroutine(StartGame());
                }
                
            }
            
        }
    }
}
