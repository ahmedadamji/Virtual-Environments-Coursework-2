using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerNumber = System.Int32;

public class AccessManager : MonoBehaviour
{
    public PlayerNumber playerNumber;

    [HideInInspector] 
    public bool locked = true;
    [HideInInspector] 
    public bool available;
    
    public bool shareable;

    private PlayerSpawnManager playerSpawnManager;
    private Player player;

    private void Awake()
    {
        PlayerSpawnManager.OnGameStart += OnGameStart;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerSpawnManager = FindObjectOfType<PlayerSpawnManager>();
        ChangeMaterials(PlayerSpawnManager.OthersMaterial);

        if (playerNumber == player.PlayerNumber || shareable)
        {
            available = true;
        }
        else
        {
            available = false;
        }
    }

    void OnGameStart()
    {
        locked = false;
        if (available && !shareable)
        {
            ChangeMaterials(player.PlayerMaterial);
        }
        else if (shareable)
        {
            ChangeMaterials(PlayerSpawnManager.SharedMaterial);
        }
    }
    
    private void ChangeMaterials(Material material)
    {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
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
}
