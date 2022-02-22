using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts;
using UnityEngine;
using UnityEditor;

namespace Ubiq.Samples
{
    [CustomEditor(typeof(TexturedAvatar))]
    public class TexturedAvatarEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Clear Saved Settings"))
            {
                (target as TexturedAvatar).ClearSettings();
            }
        }
    }
}
