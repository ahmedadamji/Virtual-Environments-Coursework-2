using System;
using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using Ubiq.Avatars;
using Ubiq.Messaging;
using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
    private Color spawnSpotColor;

    private void Awake()
    {
        spawnSpotColor = GetComponentInParent<Light>().color;
    }

    public bool SpotTaken { get; set; }

    public void TakeSpot(Player player)
    {
        if (!SpotTaken)
        {
            SpotTaken = true;
            player.PlayerMaterial.color = spawnSpotColor;
            player.PlayerNumber = transform.parent.GetSiblingIndex();
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
            Debug.Log("HELLO: You just took a spot. Your number is " + player.PlayerNumber);
            NetworkScene networkScene = NetworkScene.FindNetworkScene(this);
            if (networkScene != null)
            {
                var avatar = networkScene.GetComponentInChildren<AvatarManager>().LocalAvatar.transform.GetChild(0);
                avatar.GetChild(1).GetComponent<MeshRenderer>().material.color = spawnSpotColor;
                avatar.GetChild(2).GetComponentInChildren<SkinnedMeshRenderer>().material.color = spawnSpotColor;
                avatar.GetChild(3).GetComponentInChildren<SkinnedMeshRenderer>().material.color = spawnSpotColor;
            }

        }
    }
}
