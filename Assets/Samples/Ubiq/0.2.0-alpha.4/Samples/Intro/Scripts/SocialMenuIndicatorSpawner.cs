using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class SocialMenuIndicatorSpawner : MonoBehaviour
    {
        public SocialMenu socialMenu;
        public GameObject indicatorTemplate;

        private RoomClient roomClient;
        private NetworkScene networkScene;
        private string roomUUID;

        private void Start()
        {
            if (socialMenu && socialMenu.roomClient && socialMenu.networkScene)
            {
                roomClient = socialMenu.roomClient;
                networkScene = socialMenu.networkScene;
                roomClient.OnJoinedRoom.AddListener(RoomClient_OnJoinedRoom);
            }
        }

        private void OnDestroy()
        {
            if (roomClient)
            {
                roomClient.OnJoinedRoom.RemoveListener(RoomClient_OnJoinedRoom);
            }
        }

        private void RoomClient_OnJoinedRoom(IRoom room)
        {
            if (roomClient && networkScene && roomClient.Room != null &&
                roomClient.Room.UUID != roomUUID )
            {
                roomUUID = roomClient.Room.UUID;

                var spawner = NetworkSpawner.FindNetworkSpawner(networkScene);
                var indicator = spawner.SpawnPersistent(indicatorTemplate);
                var bindable = indicator.GetComponent<ISocialMenuBindable>();
                if (bindable != null)
                {
                    bindable.Bind(socialMenu);
                }
            }
        }
    }
}