using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSpawnManager.PlayerColor color;

    public PlayerSpawnManager.PlayerColor Color
    {
        get => color;
        set => color = value;
    }

    private void Start()
    {
        FindObjectOfType<PlayerSpawnManager>().SpawnPlayer(this);
    }
}
