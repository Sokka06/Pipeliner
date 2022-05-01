using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Common
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}