using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class HUDViewController : AbstractViewController
    {
        private LevelController _levelController;

        private void Awake()
        {
            _levelController = FindObjectOfType<LevelController>();
            _levelController.State.OnStateChanged += OnLevelStateChanged;
        }

        private void OnDestroy()
        {
            _levelController.State.OnStateChanged -= OnLevelStateChanged;
        }

        private void OnLevelStateChanged((ILevelState previous, ILevelState current) obj)
        {
            if (obj.current is ILevelState.Started)
            {
                View.Show();
            }
            else
            {
                View.Hide();
            }
        }
    }
}