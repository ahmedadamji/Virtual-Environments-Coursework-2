using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class NewRoomButton : MonoBehaviour
    {
        public SocialMenu mainMenu;
        public Text nameText;
        public bool publish;

        // Expected to be called by a UI element
        public void NewRoom ()
        {
            mainMenu.roomClient.Join(
                name: nameText.text,
                publish: publish);
        }
    }
}
