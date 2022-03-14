using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLight : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    [SerializeField] private CollabAction collabAction;
    
    protected bool state;
    
    public void ChangeState(bool aState)
    {
        state = aState;
        GetComponent<MeshRenderer>().material = state ? green : red;
        if (collabAction != null)
        {
            collabAction.CheckAllButtons();
        }
    }

    public bool State => state;
}
