using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Demos.Demo4
{
    [RequireComponent(typeof(PlayableDirector))]
    public class SequenceViewEffect : AbstractViewEffect
    {
        public PlayableDirector Director;

        private void OnValidate()
        {
            if (Director == null)
                Director = GetComponent<PlayableDirector>();
        }

        public override void Play()
        {
            Director.Play();
        }

        public override void Stop()
        {
            Director.Stop();
        }
    }
}

