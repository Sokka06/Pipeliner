using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A simple value with change events.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class EventValue<TValue>
    {
        private TValue _value;
        
        public TValue Value
        {
            get => _value;
            set => SetValue(value);
        }

        public event Action<TValue> OnValueChanged;

        public EventValue(TValue value)
        {
            _value = value;
        }

        public virtual void SetValue(TValue value, bool notify = true)
        {
            _value = value;
        
            if (notify)
                OnValueChanged?.Invoke(Value);
        }
    }
}