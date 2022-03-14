using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;

using PlayerNumber = System.Int32;

public class Player : MonoBehaviour
{
    [HideInInspector] public Material PlayerMaterial;

    public PlayerNumber PlayerNumber;

    public void Start()
    {
        CreateMaterial();
    }

    private void CreateMaterial()
    {
        PlayerMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = UnityEngine.Color.grey
        };
        // switch (Color)
        // {
        //     case PlayerSpawnManager.PlayerColor.Red:
        //         createMaterial.color = UnityEngine.Color.red;
        //         break;
        //     case PlayerSpawnManager.PlayerColor.Green:
        //         createMaterial.color = UnityEngine.Color.green;
        //         break;
        //     case PlayerSpawnManager.PlayerColor.Blue:
        //         createMaterial.color = UnityEngine.Color.blue;
        //         break;
        //     case PlayerSpawnManager.PlayerColor.Yellow:
        //         createMaterial.color = UnityEngine.Color.yellow;
        //         break;
        //     case PlayerSpawnManager.PlayerColor.None:
        //         createMaterial.color = UnityEngine.Color.black;
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
    }

}