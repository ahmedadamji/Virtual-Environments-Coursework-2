using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePart : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform rightTransform;
    public bool locked = false;
    private float distance;


    private void Update()
    {
        if (locked)
        {
            return;
        }
        distance = Vector3.Distance(transform.position, rightTransform.position);
        Debug.Log(distance);
        if (distance < 1f)
        {
            Destroy(rightTransform.GetChild(0).gameObject);
            transform.position = rightTransform.position;
            transform.rotation = rightTransform.rotation;
            locked = true;
            
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
