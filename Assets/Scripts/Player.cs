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

    public void Awake()
    {
        CreateMaterial();
    }

    private void CreateMaterial()
    {
        PlayerMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = UnityEngine.Color.grey
        };
    }

}