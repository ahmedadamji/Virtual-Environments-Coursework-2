using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class AvatarSkinMenuControl : MonoBehaviour
    {
        private AvatarSkinMenuController controller;
        private Texture2D texture;

        public void Bind(AvatarSkinMenuController controller, Texture2D texture)
        {
            this.controller = controller;
            this.texture = texture;
            GetComponent<RawImage>().texture = texture;
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if(controller != null)
            {
                controller.ChangeTexture(texture);
            }
        }
    }
}