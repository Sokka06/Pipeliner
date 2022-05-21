using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class VehicleView : MonoBehaviour
    {
        public Vehicle Vehicle;

        public AbstractViewEffect[] Effects;

        private void Start()
        {
            Vehicle.State.OnStateChanged += OnVehicleStateChanged;
        }

        private void OnDestroy()
        {
            Vehicle.State.OnStateChanged -= OnVehicleStateChanged;
        }

        private void OnVehicleStateChanged((IVehicleState previous, IVehicleState current) obj)
        {
            if (obj.current is IVehicleState.Dead)
            {
                for (int i = 0; i < Effects.Length; i++)
                {
                    Effects[i].Play();
                }
            }
        }
    }

}
