using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Single.Spawning
{
    [RequireComponent(typeof(Button))]
    public class SpawnPrefabControl : MonoBehaviour
    {
        private GameObject prefab;

        public void SetPrefab(GameObject Prefab)
        {
            prefab = Prefab;
            GetComponentInChildren<Text>().text = prefab.name;
        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Spawn);
        }

        void Spawn()
        {
            NetworkSpawner.Spawn(this, prefab);
        }
    }
}