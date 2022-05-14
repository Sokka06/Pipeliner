using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class FailSequenceController : AbstractSequenceController
    {
        public LevelController LevelController;
        
        public void Start()
        {
            LevelController.onLevelFail += OnLevelFail;
        }

        private void OnDestroy()
        {
            LevelController.onLevelFail -= OnLevelFail;
        }

        private void OnLevelFail()
        {
            Director.Play();
        }
    }
}