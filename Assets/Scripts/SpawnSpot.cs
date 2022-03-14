using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
    public Texture2D texture;
    [SerializeField] private Color spawnSpotColor;

    private void Awake()
    {
        spawnSpotColor = GetComponentInChildren<Light>().color;
    }

    public bool SpotTaken { get; set; }

    public void TakeSpot(Player player)
    {
        if (!SpotTaken)
        {
            SpotTaken = true;
            player.PlayerColor = spawnSpotColor;
            player.PlayerNumber = transform.GetSiblingIndex();
            player.transform.position = transform.position;
        }
    }
}
