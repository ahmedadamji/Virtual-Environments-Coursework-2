using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;
using PlayerNumber = System.Int32;

public class MoveAndSync : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    public Hand grasped;
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
            transform.parent = grasped.transform;
            //transform.rotation = grasped.transform.rotation;
            context.SendJson(new Message(transform.position, transform.rotation, true));
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            return;
        }

        if (isOwned && grasped == null)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void IGraspable.Grasp(Hand controller)
    {
        if (accessManager.available && !accessManager.locked && grasped == null && isOwned == false)
        {
            grasped = controller;
            isOwned = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void IGraspable.Release(Hand controller)
    {
        transform.parent = null;
        grasped = null;
        isOwned = false;
        context.SendJson(new Message(transform.position, transform.rotation, false));
        
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        if (!grasped)
        {
            transform.localPosition = msg.Position;
            transform.localRotation = msg.Rotation;
            //transform.localRotation = msg.Transform.rotation;
            isOwned = msg.IsOwned;
            Debug.Log(grasped);
        }
    }

    NetworkId INetworkObject.Id => new NetworkId(id);

    public void ForceRelease()
    {
        isOwned = false;
        grasped = null;
        foreach (var handController in handControllers) handController.Vibrate(0.3f, 0.2f);
    }

    public struct Message
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public bool IsOwned;

        public Message(Vector3 position, Quaternion rotation, bool isOwned)
        {
            Position = position;
            Rotation = rotation;
            IsOwned = isOwned;
        }
    }
}