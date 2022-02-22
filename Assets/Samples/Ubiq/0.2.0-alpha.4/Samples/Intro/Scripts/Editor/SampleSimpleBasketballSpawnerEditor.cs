using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEditor;
using UnityEngine;

namespace Ubiq.Samples
{
    [CustomEditor(typeof(SimpleBasketballSpawner))]
    public class SampleSimpleBasketballSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn"))
            {
                (target as SimpleBasketballSpawner).SpawnBasketball();
            }
        }
    }
}