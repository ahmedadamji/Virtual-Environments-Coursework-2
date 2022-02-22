using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePart : MonoBehaviour
{
    [SerializeField] private GameObject helper;
    [SerializeField] private Transform rightTransform;
    private float distance;
    private void Update()
    {
        distance = Vector3.Distance(transform.position, rightTransform.position);
        if (distance < 10.0f)
        {
            if (rightTransform.childCount == 0)
            {
                Instantiate (helper, rightTransform.position, rightTransform.rotation, rightTransform);
            }
        }
    }
}
