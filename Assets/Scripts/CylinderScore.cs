using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class CylinderScore : MonoBehaviour, INetworkComponent, INetworkObject
{
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
        context = NetworkScene.Register(this);
    }

    struct Message
    {
        public int score;
    }

    // Update is called once per frame
    void Update()
    {
        //Score += 1;
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
