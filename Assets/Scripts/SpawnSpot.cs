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

    public Color SpawnSpotColor => spawnSpotColor;
}
