using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform rightTransform;
    //[HideInInspector] public bool locked = false;
    private float distance;
    [SerializeField] private Material _material;
    private MoveAndSync mas;

    private void Start()
    {
        prefab.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
        prefab.GetComponent<MeshRenderer>().material = _material;
        mas = GetComponent<MoveAndSync>();
    }


    private void Update()
    {
        if (!mas.grasped)
        {
            return;
        }
        distance = Vector3.Distance(transform.position, rightTransform.position);
        Debug.Log(distance);
        if (distance < 1f)
        {
            if (rightTransform.childCount != 0)
            {
                Destroy(rightTransform.GetChild(0).gameObject);
                transform.position = rightTransform.position;
                transform.rotation = rightTransform.rotation;
                GetComponent<MoveAndSync>().ForceRelease();
                //locked = true;
            }
        }
        if (distance < 10.0f)
        {
            if (rightTransform.childCount == 0)
            {
                Instantiate (prefab, rightTransform.position, rightTransform.rotation, rightTransform);
            }
        }
        else
        {
            if (rightTransform.childCount != 0)
            {
                Destroy(rightTransform.GetChild(0).gameObject);
            }
        }
    }
}
