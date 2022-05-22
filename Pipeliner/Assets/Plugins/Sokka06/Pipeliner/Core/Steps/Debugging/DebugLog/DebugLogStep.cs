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

    [Serializable]
    public struct DebugLogParameters : IStepParameters
    {
        public LogType Type;
        public string Message;
    }

    /// <summary>
    /// Prints a Debug Log with given message and type.
    /// </summary>
    public class DebugLogStep : AbstractStep
    {
        public DebugLogStep(DebugLogParameters parameters) : base(parameters)
        {
        }
        
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return null;
            var parameters = (DebugLogParameters)Parameters;
            
            switch (parameters.Type)
            {
                case LogType.Normal:
                    Debug.Log(parameters.Message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(parameters.Message);
                    break;
                case LogType.Error:
                    Debug.LogError(parameters.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Progress = 1f;
            result?.Invoke(new IStepResult.Success());
        }
    }
}