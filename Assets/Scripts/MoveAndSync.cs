using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;
using PlayerNumber = System.Int32;

public class MoveAndSync : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    [HideInInspector] public Hand grasped;
    public string id;
    private AccessManager accessManager;

    private bool isOwned;
    private NetworkContext context;
    private float distance;

    private HandController[] handControllers;

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
            transform.position = grasped.transform.position;
            transform.rotation = grasped.transform.rotation;
            context.SendJson(new Message(transform, true));
        }
    }

    void IGraspable.Grasp(Hand controller)
    {
        if (accessManager.available && !accessManager.locked && grasped == null && isOwned == false)
        {
            grasped = controller;
            isOwned = true;
            //GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void IGraspable.Release(Hand controller)
    {
        if (grasped)
        {
            context.SendJson(new Message(transform, false));
            isOwned = false;
            grasped = null;
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        isOwned = msg.IsOwned;
        if (isOwned)
        {
            transform.localPosition = msg.Transform.position;
            transform.localRotation = msg.Transform.rotation;
            Debug.Log("changed from server:" + msg.IsOwned);
        }
    }

    NetworkId INetworkObject.Id => new NetworkId(id);

    public void ForceRelease()
    {
        grasped = null;
        foreach (var handController in handControllers) handController.Vibrate(0.3f, 0.2f);
    }

    public struct Message
    {
        public TransformMessage Transform;
        public bool IsOwned;

        public Message(Transform transform, bool aIsOwned)
        {
            Transform = new TransformMessage(transform);
            IsOwned = aIsOwned;
        }
    }
}