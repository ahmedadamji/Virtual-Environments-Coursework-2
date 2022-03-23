using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

using PlayerNumber = System.Int32;

public class moveGameboy : MonoBehaviour, IUseable, INetworkComponent, INetworkObject
{
    private AccessManager accessManager;
    public GameObject Gameboy;
    public GameObject Lid;

    public bool isUp;

    public uint id;
    NetworkId INetworkObject.Id => new NetworkId(id);

    public Vector3 position;
    private NetworkContext context;

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        isUp = !isUp;

        if (isUp)
        {
            Gameboy.transform.position = Gameboy.transform.position + new Vector3(0, 3, 0);
            Lid.transform.position = Lid.transform.position + new Vector3(0, 0f, -4);

        }
        else
        {
            Gameboy.transform.position = Gameboy.transform.position - new Vector3(0, 3, 0);
            Lid.transform.position = Lid.transform.position + new Vector3(0, 0f, -4);

        }
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
            isUp = !isUp;

            if (isUp)
            {
                Gameboy.transform.position = Gameboy.transform.position + new Vector3(0, 1.5f, 0);
                Lid.transform.position = Lid.transform.position + new Vector3(0, 0, -4);
                
            }
            else
            {
                Gameboy.transform.position = Gameboy.transform.position - new Vector3(0, 1.5f, 0);
                Lid.transform.position = Lid.transform.position + new Vector3(0, 0, 4);

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