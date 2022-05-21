using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Demos.Demo4
{
    [Serializable]
    public struct EngineAudio
    {
        public AudioSource Source;

        [Space] 
        public float MinVolume;
        public float MaxVolume;

        [Space] 
        public float MinPitch;
        public float MaxPitch;
    }

    [Serializable]
    public struct CollisionAudio
    {
        public AudioSource Source;
        public AudioClip[] Clips;
        
        [Space]
        public float MinImpulse;
        public float MaxImpulse;
    }

    public class VehicleAudio : MonoBehaviour, ICollisionCallbacks
    {
        public Vehicle Vehicle;

        [Space] 
        public EngineAudio Engine = new EngineAudio{MinVolume = 0.25f, MaxVolume = 1f, MinPitch = 0.5f, MaxPitch = 1.5f};
        public CollisionAudio Collision = new CollisionAudio{MinImpulse = 1f, MaxImpulse = 10f};

        private CollisionListener _collisionListener;
        private VehicleEngine _engine;
        private float _invFixedDeltaTime;

        private void Awake()
        {
            _collisionListener = Vehicle.GetComponent<CollisionListener>();
            _engine = Vehicle.GetComponentInChildren<VehicleEngine>();
            
            _invFixedDeltaTime = 1f / Time.fixedDeltaTime;
        }

        private void Start()
        {
            _collisionListener.Register(this);
        }

        private void OnDestroy()
        {
            _collisionListener.Unregister(this);
        }

        private void LateUpdate()
        {
            var engineNormalized = Mathf.InverseLerp(_engine.MinRPM, _engine.MaxRPM, _engine.CurrentRPM);
            Engine.Source.volume = Mathf.Lerp(Engine.MinVolume, Engine.MaxVolume, engineNormalized);
            Engine.Source.pitch = Mathf.Lerp(Engine.MinPitch, Engine.MaxPitch, engineNormalized);
        }

        public void CollisionEnter(Collision other)
        {
            var impulse = (other.impulse).magnitude * _invFixedDeltaTime;
            var mass = Vehicle.Controller.Rigidbody.mass;
            var min = Collision.MinImpulse * mass * _invFixedDeltaTime;
            var max = Collision.MaxImpulse * mass * _invFixedDeltaTime;
                
            if (!(Collision.Clips.Length > 0) || impulse < min)
                return;

            var volume = Mathf.InverseLerp(min, max, impulse);

            var clip = Collision.Clips[Random.Range(0, Collision.Clips.Length)];
            Collision.Source.transform.position = other.GetContact(0).point;
            Collision.Source.PlayOneShot(clip, volume);
        }

        public void CollisionStay(Collision other)
        {
            
        }

        public void CollisionExit(Collision other)
        {
            
        }
    }
}