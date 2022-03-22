using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;
using PlayerNumber = System.Int32;

public class MoveAndSyncBig : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    public Hand grasped;
    public string id;
    private AccessManager accessManager;

    private bool isOwned;
    private NetworkContext context;
    private float distance;

    private HandController[] handControllers;

    public BigHandler bigHandler;

    private void Awake()
    {
        accessManager = GetComponent<AccessManager>();
        handControllers = FindObjectsOfType<HandController>();
    }

    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    private void Update()
    {
        if (grasped)
        {
            bigHandler.MoverReady(grasped.gameObject);
        }
    }

    void IGraspable.Grasp(Hand controller)
    {
        grasped = controller;
    }

    void IGraspable.Release(Hand controller)
    {
        grasped = null;
        bigHandler.MoverNot(controller.gameObject);

    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        grasped = msg.Grasped;
    }

    NetworkId INetworkObject.Id => new NetworkId(id);

    public struct Message
    {
        public Hand Grasped;

        public Message(Hand grasped)
        {
            Grasped = grasped;
        }
    }
}