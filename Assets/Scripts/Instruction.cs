using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour, INetworkComponent, INetworkObject
{
    // Copied from Cylinder Score (Ahmed)
    NetworkId INetworkObject.Id => new NetworkId(3190);

    private NetworkContext context;

    public int Score = 0;

    public TextHandler textHandler;

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        Score = msg.score;
    }

    // Start is called before the first frame update
    void Start()
    {
        //from ahmed context thingy
        context = NetworkScene.Register(this);        
    }

    struct Message
    {
        public int score;

    }

    // Update is called once per frame
    void Update()
    {
        //from ahmed  conversion of 
        textHandler.tm.text = Score.ToString();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Cylinder")
        {
            Score++;
            Debug.Log("collide");
            Message message;
            message.score = Score;
            context.SendJson(message);
        }
    }

}
