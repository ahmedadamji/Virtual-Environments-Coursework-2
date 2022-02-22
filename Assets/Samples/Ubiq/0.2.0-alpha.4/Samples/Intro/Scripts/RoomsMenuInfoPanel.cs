using System;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class RoomsMenuInfoPanel : MonoBehaviour
    {
        public RawImage PreviewImage;
        public Text Name;
        public Text Scene;
        public Text ScenePath;
        public Text Uuid;

        private string existing;

        public void Bind(IRoom args, RoomClient client)
        {
            Name.text = args.Name;
            Scene.text = args["scene-name"];
            ScenePath.text = args["scene-path"];
            Uuid.text = args.UUID;

            var image = args["scene-image"];
            if (image != null && image != existing)
            {
                client.GetBlob(args.UUID, image, (base64image) =>
                {
                    if (base64image.Length > 0)
                    {
                        var texture = new Texture2D(1, 1);
                        texture.LoadImage(Convert.FromBase64String(base64image));
                        existing = image;
                        PreviewImage.texture = texture;
                    }
                });
            }
        }
    }
}
