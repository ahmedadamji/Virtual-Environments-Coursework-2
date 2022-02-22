using System;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class SceneInfo : MonoBehaviour
    {
        public Texture2D screenshot;

        public string base64image
        {
            get
            {
                var bytes = ImageConversion.EncodeToPNG(screenshot);
                return Convert.ToBase64String(bytes);
            }
        }

    }
}