using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
    private bool spotTaken;
    public Texture2D texture;
    [SerializeField] private PlayerSpawnManager.PlayerColor color;

    public bool SpotTaken
    {
        get => spotTaken;
        set => spotTaken = value;
    }

    public PlayerSpawnManager.PlayerColor Color => color;
}
