﻿using System.Linq;
using Ubiq.XR;
using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public interface IFirework
    {
        void Attach(Hand hand);
    }

    /// <summary>
    /// The Fireworks Box is a basic interactive object. This object uses the NetworkSpawner
    /// to create shared objects (fireworks).
    /// The Box can be grasped and moved around, but note that the Box itself is *not* network
    /// enabled, and each player has their own copy.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class FireworksBox : MonoBehaviour, IUseable, IGraspable
    {
        public GameObject FireworkPrefab;

        private Hand follow;
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public void Grasp(Hand controller)
        {
            follow = controller;
        }

        public void Release(Hand controller)
        {
            follow = null;
        }

        public void UnUse(Hand controller)
        {
        }

        public void Use(Hand controller)
        {
            var firework = NetworkSpawner.SpawnPersistent(this, FireworkPrefab).GetComponents<MonoBehaviour>().Where(mb => mb is IFirework).FirstOrDefault() as IFirework;
            if (firework != null)
            {
                firework.Attach(controller);
            }
        }

        private void Update()
        {
            if (follow != null)
            {
                transform.position = follow.transform.position;
                transform.rotation = follow.transform.rotation;
                body.isKinematic = true;
            }
            else
            {
                body.isKinematic = false;
            }
        }
    }
}