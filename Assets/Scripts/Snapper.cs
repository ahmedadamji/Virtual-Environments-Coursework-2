using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    [SerializeField] private GameObject rightTransform;
    private float distance;
    private MoveAndSync mas;

    private void Start()
    {
        mas = GetComponent<MoveAndSync>();
    }


    private void Update()
    {
        if (!mas.grasped)
        {
            return;
        }
        distance = Vector3.Distance(transform.position, rightTransform.transform.position);
        if (distance < 1f)
        {
            if (rightTransform.activeSelf)
            {
                transform.position = rightTransform.transform.position;
                transform.rotation = rightTransform.transform.rotation;
                GetComponent<MoveAndSync>().ForceRelease();
            }
        }
        if (distance < 10.0f)
        {
            rightTransform.SetActive(true);
        }
        else
        {
            rightTransform.SetActive(false);
        }
    }
}
