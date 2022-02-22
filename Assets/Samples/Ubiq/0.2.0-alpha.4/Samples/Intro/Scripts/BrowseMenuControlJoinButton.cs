using Ubiq.Rooms;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class BrowseMenuControlJoinButton : MonoBehaviour
    {
        public BrowseMenuControl browseMenuControl;

        private RoomClient roomClient;
        private string joincode;

        private void OnEnable()
        {
            browseMenuControl.OnBind.AddListener(BrowseRoomControl_OnBind);
        }

        private void OnDisable()
        {
            if (browseMenuControl)
            {
                browseMenuControl.OnBind.RemoveListener(BrowseRoomControl_OnBind);
            }
        }

        private void BrowseRoomControl_OnBind(RoomClient roomClient, IRoom roomInfo)
        {
            this.roomClient = roomClient;
            this.joincode = roomInfo.JoinCode;
        }

        // Expected to be called by a UI element
        public void Join()
        {
            if (!roomClient || string.IsNullOrEmpty(joincode))
            {
                return;
            }

            roomClient.Join(joincode:joincode);
        }
    }
}