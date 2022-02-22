using Ubiq.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Single.Questionnaire
{
    public class Questionnaire : MonoBehaviour
    {
        EventLogger results;

        // Start is called before the first frame update
        void Start()
        {
            results = new UserEventLogger(this);
        }

        public void Done()
        {
            foreach (var item in GetComponentsInChildren<Slider>())
            {
                results.Log("Answer", item.name, item.value);
            }
        }
    }
}