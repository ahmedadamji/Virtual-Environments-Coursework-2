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
        if (accessManager.locked)
        {
            return;
        }
        if (grasped)
        {
            transform.parent = grasped.transform;
            context.SendJson(new Message(transform.position, transform.rotation, true, grasped.velocity));
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
        Vector3 velocity = Vector3.zero;
        if (controller != null)
        {
            velocity = controller.velocity;
        }
        context.SendJson(new Message(transform.position, transform.rotation, false, velocity));
        GetComponent<Rigidbody>().velocity = velocity;
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        if (!grasped)
        {
            transform.position = msg.Position;
            transform.rotation = msg.Rotation;
            //transform.localRotation = msg.Transform.rotation;
            isOwned = msg.IsOwned;
            Debug.Log(grasped);
            if (isOwned)
            {
                GetComponent<Rigidbody>().velocity = msg.Velocity;
            }

            if (msg.Locked)
            {
                accessManager.locked = true;
            }
        }
    }

    NetworkId INetworkObject.Id => new NetworkId(id);

    public void ForceRelease()
    {
        Debug.Log("force release");
        isOwned = false;
        grasped = null;
        transform.parent = null;
        foreach (var handController in handControllers) handController.Vibrate(0.3f, 0.2f);
        context.SendJson(new Message(transform.position, transform.rotation, false, Vector3.zero, true));
    }

    public struct Message
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public bool IsOwned;
        public Vector3 Velocity;
        public bool Locked;

        public Message(Vector3 position, Quaternion rotation, bool isOwned, Vector3 velocity, bool locked=false)
        {
            Position = position;
            Rotation = rotation;
            IsOwned = isOwned;
            Velocity = velocity;
            Locked = locked;
        }
    }
}