using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class Timer : MonoBehaviour, INetworkComponent, INetworkObject
{
    NetworkId INetworkObject.Id => new NetworkId(4000);

    private NetworkContext context;

    public float timeValue = 0;

    public TextHandler textHandler;

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        timeValue = msg.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
    }

    struct Message
    {
        public float time;
    }

    // Update is called once per frame
    void Update()
    {
        //Score += 1;
        timeValue += Time.deltaTime;
        DisplayText(timeValue);
    }
    void DisplayText(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        string minute_str = minutes.ToString();
        string seconds_str = seconds.ToString();

        if(minutes < 10)
        {
            minute_str = "0" + minute_str;
        }

        if(seconds < 10)
        {
            seconds_str = "0" + seconds_str;
        }

        GetComponent<TextMesh>().text = minute_str + ":" + seconds_str;

        Message message;
        message.time = timeValue;
        context.SendJson(message);
    }
    
}
