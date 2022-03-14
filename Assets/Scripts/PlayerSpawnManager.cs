using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : MonoBehaviour
{
    public AvatarPagePanelController asmc;

    private SpawnSpot[] spawnSpots;
    private int playerCount;
    private bool spawned;
    
    [SerializeField] private Color sharedMaterialColor;
    [SerializeField] private Color othersMaterialColor;
    public static Material SharedMaterial;
    public static Material OthersMaterial;

    private RoomClient roomClient;

    private void Awake()
    {
        roomClient = FindObjectOfType<RoomClient>();
        roomClient.OnPeerAdded.AddListener(OnAdded);
        spawnSpots = GetComponentsInChildren<SpawnSpot>();
        
        SharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = Color.gray
        };
        
        OthersMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = Color.gray
        };
    }

    private void OnAdded(IPeer peer)
    {
        playerCount++;
        Debug.Log("COUNT " + playerCount);
        if (playerCount == 4)
        {
            SpawnPlayer(FindObjectOfType<Player>());
        }
    }
    
    public void SpawnPlayer(Player player)
    {
        List<SpawnSpot> availableSpots = spawnSpots.Where(spawnSpot => !spawnSpot.SpotTaken).ToList();

        if (availableSpots.Count != 0)
        {
            int i = Random.Range(0, availableSpots.Count);
            var playerTransform = player.transform;
            playerTransform.position = availableSpots[i].transform.position;
            player.transform.rotation = Quaternion.LookRotation(-playerTransform.position);
            player.PlayerColor = availableSpots[i].SpawnSpotColor;
            
            Debug.Log(asmc);
            asmc.SetTexture(availableSpots[i].texture);

            availableSpots[i].SpotTaken = true;
            availableSpots.RemoveAt(i);
        }

        if (availableSpots.Count != 0)
        {
            // Start Game;
        }
        
    }
}
