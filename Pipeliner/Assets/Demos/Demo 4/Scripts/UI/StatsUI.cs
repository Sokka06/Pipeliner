using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Vehicle;
using TMPro;
using UnityEngine;

namespace Demos.Demo4
{
    public class StatsUI : MonoBehaviour
    {
        public UIView View;
        public TextMeshProUGUI Text;

        private LevelController _levelController;

        private void Start()
        {
            _levelController = FindObjectOfType<LevelController>();
            SetTime(_levelController.FinishData.Time);

        }

        private void OnDestroy()
        {
            
        }
        
        
        private void OnLevelFinish(LevelFinishData obj)
        {
            SetTime(obj.Time);
        }
        
        private void SetTime(float time)
        {
            Text.SetText(time.FormatTime(64, 32));
        }
    }
}