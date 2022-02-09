using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public enum LogType
    {
        Normal,
        Warning,
        Error
    }

    public class DebugLogStep : AbstractStepBehaviour
    {
        public LogType Type;
        public string Message;
    
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);
            
            switch (Type)
            {
                case LogType.Normal:
                    Debug.Log(Message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(Message);
                    break;
                case LogType.Error:
                    Debug.LogError(Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SetProgress(1f);
        }
    }
}