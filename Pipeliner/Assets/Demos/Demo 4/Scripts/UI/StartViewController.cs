using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class StartViewController : AbstractViewController
    {
        public CountdownUI Countdown;
        
        private LevelController _levelController;

        private void Awake()
        {
            _levelController = FindObjectOfType<LevelController>();
            _levelController.State.onStateChanged += OnLevelStateChanged;
        }

        private void OnDestroy()
        {
            _levelController.State.onStateChanged -= OnLevelStateChanged;
        }

        private void OnLevelStateChanged((ILevelState previous, ILevelState current) obj)
        {
            switch (obj.current)
            {
                case ILevelState.Default @default:
                    View.Show();
                    Countdown.StartCountdown(value =>
                    {
                        // Start race on last step
                        if (value < Countdown.Steps.Count - 1)
                            return;
                
                        _levelController.StartLevel();
            
                        // Unfreeze all racers.
                        
                    }, () => View.Hide());
                    break;
                case ILevelState.Failed failed:
                    break;
                case ILevelState.Finished finished:
                    break;
                case ILevelState.Started started:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
