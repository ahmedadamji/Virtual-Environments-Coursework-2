using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSpawnManager.PlayerColor color;

    public PlayerSpawnManager.PlayerColor Color
    {
        get => color;
        set => color = value;
    }

    public void Start()
    {
        FindObjectOfType<PlayerSpawnManager>().SpawnPlayer(this);
    }
}