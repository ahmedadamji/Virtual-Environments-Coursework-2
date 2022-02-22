using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderScore : MonoBehaviour
{
    public int Score = 0;
    public TextHandler textHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score += 1;
        textHandler.tm.text = Score.ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Cylinder")
        {
            Score++;
            Debug.Log("collide");
        }
    }
}
