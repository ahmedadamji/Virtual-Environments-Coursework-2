using Ubiq.Avatars;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class AvatarMenuController : MonoBehaviour
    {
        public GameObject[] avatars;
        public AvatarManager manager;

        public void ChangeAvatar(GameObject prefab)
        {
            if (Application.isPlaying)
            {
                manager.CreateLocalAvatar(prefab);
            }
        }

        public TexturedAvatar GetSkinnable()
        {
            if (manager.LocalAvatar)
            {
                return manager.LocalAvatar.GetComponent<TexturedAvatar>();
            }
            return null;
        }
    }
}