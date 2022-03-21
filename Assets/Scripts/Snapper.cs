using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private float distance;
    private AccessManager accessManager;
    private MoveAndSync moveAndSync;
    [SerializeField] private Material snapMaterial;
    public bool isMotherboard;

    private void Start()
    {
        accessManager = GetComponent<AccessManager>();
        moveAndSync = GetComponent<MoveAndSync>();
        if (destination != null)
        {
            ChangeMaterials(destination, snapMaterial);
            destination.SetActive(false);
        }
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
        if (destination == null)
        {
            return;
        }
        if (!moveAndSync.grasped || !accessManager.available || accessManager.locked)
        {
            return;
        }
        distance = Vector3.Distance(transform.position, destination.transform.position);
        if (distance < .5f)
        {
                if (isMotherboard)
                {
                    var motherboard = FindObjectOfType<Motherboard>();

                    Debug.Log("mb part replaced");
                    motherboard.PartReplaced();
                }

                ChangeMaterials(GetComponent<MeshRenderer>().material);
                gameObject.SetActive(false);
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
    
    private void ChangeMaterials(Material material)
    {
        var children = destination.GetComponentsInChildren<MeshRenderer>();
        foreach (var rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (int j = 0; j < rend.materials.Length; j++) 
            { 
                mats[j] = material; 
            }
            rend.materials = mats;
        }

    }
}
