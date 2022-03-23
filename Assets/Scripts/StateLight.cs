using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLight : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    [SerializeField] private CollabAction collabAction;
    
    public bool State;

    public void ChangeState(bool aState)
    {
        Debug.Log(transform.parent.GetSiblingIndex() +" state: " + aState);
        State = aState;
        GetComponent<MeshRenderer>().material = State ? green : red;
        if (collabAction != null)
        {
            collabAction.CheckAllButtons();
        }
    }

}
