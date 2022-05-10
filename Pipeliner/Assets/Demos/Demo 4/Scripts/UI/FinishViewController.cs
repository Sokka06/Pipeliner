using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class FinishViewController : MonoBehaviour
    {
        public UIView View;

        private LevelController _levelController;

        private void OnValidate()
        {
            if (View == null)
                View = GetComponent<UIView>();
        }

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
                    break;
                case ILevelState.Finished finished:
                    View.Show();
                    break;
                case ILevelState.Started started:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
