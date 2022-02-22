using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class JoinRoomButton : MonoBehaviour
    {
        public SocialMenu mainMenu;
        public PanelSwitcher mainPanel;
        public Text joincodeText;
        public TextEntry textEntry;
        public Image textInputArea;
        public string failMessage;
        public Color failTextInputAreaColor;

        private Color defaultTextInputAreaColor;
        private string lastRequestedJoincode;

        private void OnEnable()
        {
            mainMenu.roomClient.OnJoinedRoom.AddListener(RoomClient_OnJoinedRoom);
            mainMenu.roomClient.OnJoinRejected.AddListener(RoomClient_OnJoinRejected);
            defaultTextInputAreaColor = textInputArea.color;
        }

        private void OnDisable()
        {
            mainMenu.roomClient.OnJoinedRoom.RemoveListener(RoomClient_OnJoinedRoom);
            mainMenu.roomClient.OnJoinRejected.RemoveListener(RoomClient_OnJoinRejected);
            textInputArea.color = defaultTextInputAreaColor;
        }

        private void RoomClient_OnJoinedRoom(IRoom room)
        {
            mainPanel.SwitchPanelToDefault();
        }

        private void RoomClient_OnJoinRejected(Rejection rejection)
        {
            if (rejection.joincode != lastRequestedJoincode)
            {
                return;
            }

            textEntry.SetText(failMessage,textEntry.defaultTextColor,true);
            textInputArea.color = failTextInputAreaColor;
        }

        // Expected to be called by a UI element
        public void Join()
        {
            lastRequestedJoincode = joincodeText.text.ToLowerInvariant();
            mainMenu.roomClient.Join(joincode:lastRequestedJoincode);
        }
    }
}
