using Ubiq.Logging;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Single.Questionnaire
{
    public class LogCollectorCreator : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if(Application.isEditor)
            {
                gameObject.AddComponent<LogCollector>();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
