﻿using System;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.Events;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public interface ISocialMenuBindable
    {
        void Bind(SocialMenu mainMenu);
    }

    public class SocialMenu : MonoBehaviour
    {
        public enum State
        {
            Open,
            Closed
        }

        public NetworkScene networkSceneOverride;
        public Transform spawnRelativeTransform;

        // Provide central access to NetworkScene for the whole UI
        private NetworkScene _networkScene;
        public NetworkScene networkScene
        {
            get
            {
                if (networkSceneOverride)
                {
                    return networkSceneOverride;
                }

                if (!_networkScene)
                {
                    _networkScene = NetworkScene.FindNetworkScene(this);
                }

                return _networkScene;
            }
        }

        // Provide central access to RoomClient for the whole UI
        private RoomClient _roomClient;
        public RoomClient roomClient
        {
            get
            {
                if (!_roomClient)
                {
                    if (networkScene)
                    {
                        _roomClient = networkScene.GetComponent<RoomClient>();
                    }
                }

                return _roomClient;
            }
        }

        [Serializable]
        public class SocialMenuEvent : UnityEvent<SocialMenu,State> { }
        public SocialMenuEvent OnStateChange;

        [System.NonSerialized]
        public State state;

        private GameObject uiIndicator;

        private void OnEnable()
        {
            state = State.Open;
            OnStateChange.Invoke(this,state);
        }

        private void OnDisable()
        {
            state = State.Closed;
            OnStateChange.Invoke(this,state);
        }

        public void Request ()
        {
            var cam = Camera.main.transform;

            transform.position = cam.TransformPoint(spawnRelativeTransform.localPosition);
            transform.rotation = cam.rotation * spawnRelativeTransform.localRotation;
            gameObject.SetActive(true);
        }
    }
}