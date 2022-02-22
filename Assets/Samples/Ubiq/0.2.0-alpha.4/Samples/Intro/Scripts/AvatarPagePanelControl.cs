using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class AvatarPagePanelControl : MonoBehaviour
    {
        public RawImage image;
        public Button button;

        [System.Serializable]
        public class BindEvent : UnityEvent<Texture2D> { };
        public BindEvent OnBind;

        private Action<Texture2D> onClick;
        private Texture2D texture;

        public void Bind(Action<Texture2D> onClick, Texture2D texture)
        {
            this.texture = texture;
            this.onClick = onClick;

            image.texture = texture;
            button.onClick.AddListener(Button_OnClick);

            OnBind.Invoke(texture);
        }

        private void Button_OnClick()
        {
            if (onClick != null && texture != null)
            {
                onClick(texture);
            }
        }
    }
}