using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class FinishSequenceController : AbstractSequenceController
    {
        public LevelController LevelController;
        
        public void Start()
        {
            LevelController.onLevelFinish += OnLevelFinish;
        }

        private void OnDestroy()
        {
            LevelController.onLevelFinish -= OnLevelFinish;
        }

        private void OnLevelFinish(LevelFinishData finishData)
        {
            Director.Play();
        }
    }
}

