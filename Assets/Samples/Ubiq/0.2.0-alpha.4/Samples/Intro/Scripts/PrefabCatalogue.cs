using System.Collections.Generic;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    [CreateAssetMenu(menuName = "Prefab Catalogue")]
    public class PrefabCatalogue : ScriptableObject
    {
        public List<GameObject> prefabs;

        public int IndexOf(GameObject gameObject)
        {
            return prefabs.IndexOf(gameObject);
        }
    }
}