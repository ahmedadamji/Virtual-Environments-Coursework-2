using System;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class BrowseMenuControl : MonoBehaviour
    {
        public Text Name;
        public Text SceneName;
        public RawImage ScenePreview;

        [System.Serializable]
        public class BindEvent : UnityEvent<RoomClient, IRoom> { };
        public BindEvent OnBind;

        private string existing;

        public void Bind(RoomClient client, IRoom roomInfo)
        {
            Name.text = roomInfo.Name;
            SceneName.text = roomInfo["scene-name"];

            var image = roomInfo["scene-image"];
            if (image != null && image != existing)
            {
                client.GetBlob(roomInfo.UUID, image, (base64image) =>
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

            OnBind.Invoke(client,roomInfo);
        }
    }
}