using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;
using UnityEditor;

namespace Ubiq.Samples
{
    [CustomEditor(typeof(SceneInfo))]
    public class SceneInfoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.HelpBox("Stores basic properties about this scene that can be used when setting it as the scene for a Room.", MessageType.Info);

            var component = target as SceneInfo;


        }
    }
}