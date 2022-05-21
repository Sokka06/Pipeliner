using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Demos.Demo4
{
    public interface IVehicleState : IState
    {
        public struct Alive : IVehicleState { }
        public struct Dead : IVehicleState { }
    }
    
    public class Vehicle : MonoBehaviour
    {
        public VehicleController Controller;
        public Health Health;
    
        private RigidbodyConstraints _constraints;
    
        public StateMachine<IVehicleState> State { get; private set; } = new StateMachine<IVehicleState>(new IVehicleState.Alive());
    
        private void Awake()
        {
            _constraints = Controller.Rigidbody.constraints;
        }
    
        private void Start()
        {
            Health.onHealthMin += OnHealthMin;
            State.OnStateChanged += OnStateChanged;
        }
    
        private void OnDestroy()
        {
            Health.onHealthMin -= OnHealthMin;
            State.OnStateChanged -= OnStateChanged;
        }
    
        private void OnHealthMin(int obj)
        {
            if (State.CurrentState is IVehicleState.Alive)
                State.SetState(new IVehicleState.Dead());
        }
        
        private void OnStateChanged((IVehicleState previous, IVehicleState current) obj)
        {
            if (obj.current is IVehicleState.Dead)
            {
                Controller.Input.Enabled = false;
            }
        }
    
        public void SetFreeze(bool value)
        {
            if (value)
            {
                Controller.Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                Controller.Rigidbody.constraints = _constraints;
            }
        }
    }
}