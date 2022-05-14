using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class FailViewController : AbstractViewController
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
            switch (obj.current)
            {
                case ILevelState.Default @default:
                    break;
                case ILevelState.Failed failed:
                    View.Show();
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
