﻿using System;
using System.Runtime.InteropServices;
using Ubiq.Messaging;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public class NetworkedMainMenuIndicator : MonoBehaviour, ISocialMenuBindable, INetworkObject, INetworkComponent, ISpawnable
    {
        private SocialMenu mainMenu;
        private NetworkContext context;
        private State[] state = new State[1];
        private Renderer[] renderers;
        private bool visible;
        private bool notify;

        public NetworkId Id { get; set; } = NetworkId.Unique();

        [Serializable]
        private struct State
        {
            public Vector3 position;
            public Quaternion rotation;
            public bool opened;
        }

        private void Awake()
        {
            renderers = GetComponentsInChildren<Renderer>(includeInactive:true);
        }

        private void Start()
        {
            context = NetworkScene.Register(this);
        }

        public void Bind(SocialMenu mainMenu)
        {
            // If we're bound, we're the local version
            // This means we're the authority on position/rotation
            this.mainMenu = mainMenu;
            mainMenu.OnStateChange.AddListener(MainMenu_OnStateChange);
            HandleState(mainMenu.state);

            // Local user sees the full ui instead
            SetVisibility(visible:false);
        }

        public void OnDestroy()
        {
            if (mainMenu)
            {
                mainMenu.OnStateChange.RemoveListener(MainMenu_OnStateChange);
            }
        }

        private void HandleState(SocialMenu.State state)
        {
            if (state == SocialMenu.State.Open)
            {
                this.state[0].opened = true;
            }
            else if (state == SocialMenu.State.Closed)
            {
                this.state[0].opened = false;
                notify = true;
            }
        }

        private void MainMenu_OnStateChange(SocialMenu mainMenu, SocialMenu.State state)
        {
            HandleState(state);
        }

        private void SetVisibility(bool visible)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = visible;
            }
        }

        public void Update()
        {
            if (mainMenu != null)
            {
                // Only send update if UI is opened locally
                // Or UI was just closed, in which case notify remotes
                if (state[0].opened || notify)
                {
                    // Update state from menu
                    state[0].position = mainMenu.transform.position;
                    state[0].rotation = mainMenu.transform.rotation;

                    // Send it through network
                    Send();

                    // No longer need to notify
                    notify = false;
                }
            }
        }

        private void Send()
        {
            var transformBytes = MemoryMarshal.AsBytes(new ReadOnlySpan<State>(state));
            var message = ReferenceCountedSceneGraphMessage.Rent(transformBytes.Length);
            transformBytes.CopyTo(new Span<byte>(message.bytes, message.start, message.length));

            context.Send(message);
        }

        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            MemoryMarshal.Cast<byte, State>(
                new ReadOnlySpan<byte>(message.bytes, message.start, message.length))
                .CopyTo(new Span<State>(state));

            OnRecv();
        }

        private void OnRecv()
        {
            if (!state[0].opened)
            {
                SetVisibility(visible:false);
                return;
            }

            SetVisibility(visible:true);
            transform.position = state[0].position;
            transform.rotation = state[0].rotation;
        }

        void ISpawnable.OnSpawned(bool local) { }
    }
}
