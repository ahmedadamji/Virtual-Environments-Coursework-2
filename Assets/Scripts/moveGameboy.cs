using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

using PlayerNumber = System.Int32;

public class moveGameboy : MonoBehaviour, IUseable, INetworkComponent, INetworkObject
{
    //public StateLight indicator;
    private AccessManager accessManager;
    public GameObject Gameboy;

    public bool isUp;

    public uint id;
    NetworkId INetworkObject.Id => new NetworkId(id);

    public Vector3 position;
    private NetworkContext context;

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        position = msg.height;
        //indicator.ChangeState(msg.State);
    }

    private void Awake()
    {
        accessManager = GetComponent<AccessManager>();
    }

    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    struct Message
    {
        public Vector3 height;
    }

    public void Use(Hand controller)
    {
        if (accessManager.available && !accessManager.locked)
        {
            //indicator.ChangeState(!indicator.State);

            isUp = !isUp;

            if (isUp)
            {
                Gameboy.transform.position = Gameboy.transform.position + new Vector3(0, 3, 0);
            }
            else
            {
                Gameboy.transform.position = Gameboy.transform.position - new Vector3(0, 3, 0);
            }

            position = Gameboy.transform.position;

            Message message;
            message.height = Gameboy.transform.position;
            context.SendJson(message);
        }
    }

    public void UnUse(Hand controller)
    {

    }
}