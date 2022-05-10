using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionEffect : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public AudioSource Source;

    [Space]
    public float PlayTime = 5f;

    public bool IsPlaying { get; private set; }

    private void Start()
    {
        transform.root.GetComponentInChildren<Health>().onHealthMin += ONHealthMin;
    }

    private void OnDestroy()
    {
        transform.root.GetComponentInChildren<Health>().onHealthMin -= ONHealthMin;
    }

    private void ONHealthMin(int obj)
    {
        PlayEffect();
    }


    public void PlayEffect()
    {
        StartCoroutine(Effect());
    }
    
    public void StopEffect()
    {
        
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
