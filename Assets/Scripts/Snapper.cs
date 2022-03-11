using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private float distance;
    private MoveAndSync mas;
    [SerializeField] private Material snapMaterial;

    private void Start()
    {
        mas = GetComponent<MoveAndSync>();
        ChangeMaterials(destination, snapMaterial);
        destination.SetActive(false);

    }
    
    void ChangeMaterials(GameObject target, Material material)
    {
        var children = target.GetComponentsInChildren<MeshRenderer>();
        foreach (var rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++) 
            { 
                mats[j] = material; 
            }
            rend.materials = mats;
        }
    }


    private void Update()
    {
        if (!mas.grasped || !mas.movable || mas.locked)
        {
            return;
        }
        distance = Vector3.Distance(transform.position, destination.transform.position);
        if (distance < .5f)
        {
            
                destination.SetActive(false);
                transform.position = destination.transform.position;
                transform.rotation = destination.transform.rotation;
                GetComponent<MoveAndSync>().ForceRelease();

        }
        else if (distance < 3.0f)
        {
            destination.SetActive(true);
        }
        else
        {
            destination.SetActive(false);
        }
    }
}
