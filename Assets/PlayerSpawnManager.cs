using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : MonoBehaviour
{
    public enum PlayerColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }
    private SpawnSpot[] spawnSpots;
    private void Awake()
    {
        spawnSpots = GetComponentsInChildren<SpawnSpot>();
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
            player.Color = availableSpots[i].Color;
            availableSpots[i].SpotTaken = true;
            availableSpots.RemoveAt(i);
        }

        if (availableSpots.Count != 0)
        {
            // Start Game;
        }
        
    }
}
