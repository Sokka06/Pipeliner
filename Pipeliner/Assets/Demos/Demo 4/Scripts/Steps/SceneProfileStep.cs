using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Demos.Demo4
{
    public struct SceneProfileStepParameters : IStepParameters
    {
        public readonly SceneProfile Profile;

        public SceneProfileStepParameters(SceneProfile profile)
        {
            Profile = profile;
        }
    }

    /// <summary>
    /// Loads Addressable scenes in Scene Profile.
    /// </summary>
    public class SceneProfileStep : AbstractStep
    {
        private readonly AddressableSceneStep[] _steps;
    
        public SceneProfileStep(SceneProfileStepParameters parameters) : base(parameters)
        {
            _steps = new AddressableSceneStep[parameters.Profile.Scenes.Length];
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            base.Run(result);

            var parameters = (SceneProfileStepParameters)Parameters;

            var inv = 1f / parameters.Profile.Scenes.Length;
            for (int i = 0; i < parameters.Profile.Scenes.Length; i++)
            {
                var step = new AddressableSceneStep(new AddressableSceneStepParameters(parameters.Profile.Scenes[i]));
                IStepResult stepResult;
                var e = step.Run(value => stepResult = value);
                while (e.MoveNext())
                {
                    yield return e.Current;
                    Progress = i * inv + step.Progress * inv;
                }

                _steps[i] = step;
            }

            Progress = 1f;
            result?.Invoke(new IStepResult.Success());
        }
    }
}