using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Material mat;

    public PlayerSpawnManager.PlayerColor Color = PlayerSpawnManager.PlayerColor.None;
    

    public void Start()
    {
        //FindObjectOfType<PlayerSpawnManager>().SpawnPlayer(this);
        mat = CreateMaterial();
    }

    public Material CreateMaterial()
    {
        Material createMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        switch (Color)
        {
            case PlayerSpawnManager.PlayerColor.Red:
                createMaterial.color = UnityEngine.Color.red;
                break;
            case PlayerSpawnManager.PlayerColor.Green:
                createMaterial.color = UnityEngine.Color.green;
                break;
            case PlayerSpawnManager.PlayerColor.Blue:
                createMaterial.color = UnityEngine.Color.blue;
                break;
            case PlayerSpawnManager.PlayerColor.Yellow:
                createMaterial.color = UnityEngine.Color.yellow;
                break;
            case PlayerSpawnManager.PlayerColor.None:
                createMaterial.color = UnityEngine.Color.black;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return createMaterial;
    }
}