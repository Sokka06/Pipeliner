using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Vehicle;
using TMPro;
using UnityEngine;

namespace Demos.Demo4
{
    public class TimeUI : MonoBehaviour
    {
        public TextMeshProUGUI TimeText;
        
        [Space] 
        public float NumberSpacing = 32;
        public float DividerSpacing = 16;

        private LevelController _levelController;
        
        private void OnValidate()
        {
            if (TimeText == null)
                return;
            
            SetTime(0f);
        }

        private void Start()
        {
            _levelController = FindObjectOfType<LevelController>();
        }

        private void LateUpdate()
        {
            if (!(_levelController.State.CurrentState is ILevelState.Started))
                return;
            
            SetTime(_levelController.CurrentTime);
        }

        private void SetTime(float time)
        {
            TimeText.SetText(time.FormatTime(NumberSpacing, DividerSpacing));
        }
    }
}

