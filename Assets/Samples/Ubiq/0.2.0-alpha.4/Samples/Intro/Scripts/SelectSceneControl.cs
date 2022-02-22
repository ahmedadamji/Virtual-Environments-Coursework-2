using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class SelectSceneControl : MonoBehaviour
    {
        public string sceneName;

        // Start is called before the first frame update
        void Start()
        {
            GetComponentInParent<Button>().onClick.AddListener(Switch);
        }

        // Update is called once per frame
        void Switch()
        {
            RoomSceneManager.ChangeScene(this, sceneName);
        }
    }
}
