using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Demos.Demo4
{
    public class IntroSequenceController : AbstractSequenceController
    {
        public LevelController LevelController;
        public CountdownUI Countdown;

        private Player _player;
        
        public void Start()
        {
            InputSystem.onAnyButtonPress.CallOnce(OnAnyButtonPress);
            _player = FindObjectOfType<Player>();
            _player.Vehicle.SetFreeze(true);
        }

        private void OnAnyButtonPress(InputControl obj)
        {
            Director.Play();
        }

        public void LevelCountdown()
        {
            Countdown.StartCountdown(value =>
            {
                // Start race on last step
                if (value < Countdown.Steps.Count - 1)
                    return;
                
                LevelController.StartLevel();
                _player.Vehicle.SetFreeze(false);
            });
        }

        private void OnDestroy()
        {
            
        }
    }
}

