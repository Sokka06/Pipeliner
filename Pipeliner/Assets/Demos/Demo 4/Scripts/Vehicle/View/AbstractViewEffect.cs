using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public abstract class AbstractViewEffect : MonoBehaviour
    {
        public bool IsPlaying { get; protected set; }

        public abstract void Play();

        public abstract void Stop();
    }
}