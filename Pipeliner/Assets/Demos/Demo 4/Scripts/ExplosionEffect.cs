using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Demos.Demo4
{
    public class ExplosionEffect : AbstractViewEffect
    {
        public ParticleSystem ParticleSystem;
        public AudioSource Source;
    
        [Space]
        public float PlayTime = 5f;

        public override void Play()
        {
            StartCoroutine(Effect());
        }

        public override void Stop()
        {
            StopCoroutine(Effect());
        }

        private IEnumerator Effect()
        {
            var timer = PlayTime;
            IsPlaying = true;
            
            ParticleSystem.Play();
    
            var soundTimer = 0f;
            var soundInterval = 0.1333f;
    
            while (timer > 0f)
            {
                if (soundTimer > 0f)
                {
                    soundTimer -= Time.deltaTime;
                }
                else
                {
                    Source.pitch = Random.Range(0.9f, 1.1f);
                    Source.PlayOneShot(Source.clip);
                    soundTimer = soundInterval;
                }
                
                timer -= Time.deltaTime;
                yield return null;
            }
            
            IsPlaying = false;
            
            ParticleSystem.Stop();
        }
    }
}
