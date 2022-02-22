using Ubiq.XR;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class SocialMenuRequestHandler : MonoBehaviour
    {
        public SocialMenu socialMenu;
        public MenuRequestSource source;

        private void OnEnable()
        {
            source.OnRequest.AddListener(MenuRequestSource_OnMenuRequest);
        }

        private void OnDisable()
        {
            source.OnRequest.RemoveListener(MenuRequestSource_OnMenuRequest);
        }

        private void MenuRequestSource_OnMenuRequest(GameObject requester)
        {
            socialMenu.Request();
        }
    }
}