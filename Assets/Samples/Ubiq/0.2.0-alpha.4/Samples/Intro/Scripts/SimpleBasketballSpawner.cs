using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{

    public class SimpleBasketballSpawner : MonoBehaviour
    {
        public GameObject Prefab;

        public void SpawnBasketball()
        {
            NetworkSpawner.Spawn(this, Prefab);
        }
    }
}