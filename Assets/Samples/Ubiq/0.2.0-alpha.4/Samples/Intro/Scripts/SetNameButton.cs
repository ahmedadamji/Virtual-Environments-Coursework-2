using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class SetNameButton : MonoBehaviour
    {
        public NameManager nameManager;
        public Text nameText;

        // Expected to be called by a UI element
        public void SetName()
        {
            if (nameText && nameManager)
            {
                nameManager.SetName(nameText.text);
            }
        }
    }
}