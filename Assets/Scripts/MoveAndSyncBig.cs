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
            bigHandler.GraspMover(id, grasped.transform.position);
        }
    }

    void IGraspable.Grasp(Hand controller)
    {
        grasped = controller;
    }

    void IGraspable.Release(Hand controller)
    {
        bigHandler.ReleaseMover(id, controller.transform.position);
        grasped = null;
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
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