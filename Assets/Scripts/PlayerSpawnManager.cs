using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : MonoBehaviour
{
    public AvatarPagePanelController asmc;
    public enum PlayerColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }

    public static Material Black;
    public static Material Any;
    private SpawnSpot[] spawnSpots;
    private void Awake()
    {
        spawnSpots = GetComponentsInChildren<SpawnSpot>();
        Black = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = Color.black
        };
        Any = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = Color.gray
        };
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
