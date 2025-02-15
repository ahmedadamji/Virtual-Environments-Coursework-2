﻿using System.Collections;
using System.Collections.Generic;
using Samples.Ubiq._0._2._0_alpha._4.Samples.Single.Rooms.Scripts;
using UnityEngine;
using UnityEditor;

namespace Ubiq.Samples.Minimal.Rooms
{
    [CustomEditor(typeof(RoomClientJoinHelper))]
    public class RoomClientJoinHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var component = target as RoomClientJoinHelper;

            if(GUILayout.Button("Join All RoomClients"))
            {
                component.JoinAllRoomClients();
            }
        }
    }

}