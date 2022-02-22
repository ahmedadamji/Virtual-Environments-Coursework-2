using System;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class CurrentRoomPanelControl : MonoBehaviour
    {
        public Text Joincode;
        public RawImage ScenePreview;

        private string existing;

        public void Bind(RoomClient client)
        {
            Joincode.text = client.Room.JoinCode.ToUpperInvariant();

            var image = client.Room["scene-image"];
            if (image != null && image != existing)
            {
                client.GetBlob(client.Room.UUID, image, (base64image) =>
                {
                    if (base64image.Length > 0)
                    {
                        var texture = new Texture2D(1, 1);
                        texture.LoadImage(Convert.FromBase64String(base64image));
                        existing = image;
                        ScenePreview.texture = texture;
                    }
                });
            }
        }
    }
}