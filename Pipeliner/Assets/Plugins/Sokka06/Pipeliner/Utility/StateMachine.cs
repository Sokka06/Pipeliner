using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A simple state interface for state machine.
    /// </summary>
    public interface IState { }
    
    /// <summary>
    /// A simple state machine.
    /// </summary>
    /// <typeparam name="T">Your state interface.</typeparam>
    public class StateMachine<T> where T : IState
    {
        public T CurrentState { get; protected set; }

        public event Action<(T previous, T current)> OnStateChanged;
        
        public StateMachine(T initialState)
        {
            CurrentState = initialState;
        }

        public virtual void SetState(T state, bool notify = true)
        {
            var oldState = CurrentState;
            CurrentState = state;
            
            if(notify)
                OnStateChanged?.Invoke((oldState, CurrentState));
        }
    }
}
