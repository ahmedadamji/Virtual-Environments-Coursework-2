using Ubiq.Avatars;
using Ubiq.XR;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class WristMenuInvoker : MonoBehaviour, IUseable
    {
        public MenuRequestSource source;

        public enum Wrist
        {
            Left,
            Right
        }
        public Wrist wrist;

        public void Use(Hand controller)
        {
            source.Request(gameObject);
        }

        public void UnUse(Hand controller) { }

        private void Update()
        {
            UpdatePositionAndRotation();
        }

        private void LateUpdate()
        {
            UpdatePositionAndRotation();
        }

        private void UpdatePositionAndRotation()
        {
            var node = wrist == Wrist.Left
                ? AvatarHints.NodePosRot.LeftWrist
                : AvatarHints.NodePosRot.RightWrist;
            if (AvatarHints.TryGet(node, out var positionRotation))
            {
                transform.position = positionRotation.position;
                transform.rotation = positionRotation.rotation;
            }
        }
    }
}
