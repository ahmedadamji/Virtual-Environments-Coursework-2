using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerNumber = System.Int32;

public class AccessManager : MonoBehaviour
{
    public PlayerNumber playerNumber;

    [HideInInspector] public bool locked = true;
    [HideInInspector] public bool available;
    
    public bool shareable;

    private PlayerSpawnManager playerSpawnManager;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        playerSpawnManager = FindObjectOfType<PlayerSpawnManager>();
        playerSpawnManager.OnGameStart.AddListener(OnGameStart);

        
        if (playerNumber == player.PlayerNumber)
        {
            available = true;
            ChangeMaterials(player.PlayerMaterial);
        }
        else if (shareable)
        {
            available = true;
            ChangeMaterials(PlayerSpawnManager.SharedMaterial); // change to shared material
        }
        else
        {
            available = false;
            ChangeMaterials(PlayerSpawnManager.OthersMaterial);
        }
    }

    private void OnGameStart()
    {
        locked = false;
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
