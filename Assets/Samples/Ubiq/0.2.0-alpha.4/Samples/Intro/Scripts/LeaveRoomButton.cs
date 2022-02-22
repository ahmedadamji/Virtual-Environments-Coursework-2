using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class LeaveRoomButton : MonoBehaviour
    {
        public SocialMenu mainMenu;

        // Expected to be operated by UI
        public void LeaveRoom ()
        {
            if (mainMenu && mainMenu.roomClient)
            {
                mainMenu.roomClient.Leave();
            }
        }
    }
}
