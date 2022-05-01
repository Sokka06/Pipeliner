using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Demos.Demo4
{
    public struct LoadProfileStepParameters : IStepParameters
    {
        public readonly LoadProfile Profile;

        public LoadProfileStepParameters(LoadProfile profile)
        {
            Profile = profile;
        }
    }
    
    /// <summary>
    /// Loads Scene Profiles in Load Profile.
    /// </summary>
    public class LoadProfileStep : AbstractStep
    {
        public LoadProfileStep(LoadProfileStepParameters parameters) : base(parameters)
        {
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            base.Run(result);

            var parameters = (LoadProfileStepParameters)Parameters;
            
            var inv = 1f / parameters.Profile.SceneProfiles.Length;
            for (int i = 0; i < parameters.Profile.SceneProfiles.Length; i++)
            {
                var step = new SceneProfileStep(new SceneProfileStepParameters(parameters.Profile.SceneProfiles[i]));
                IStepResult stepResult;
                var e = step.Run(value => stepResult = value);
                while (e.MoveNext())
                {
                    yield return e.Current;
                    Progress = i * inv + step.Progress * inv;
                }
            }
            
            /*var invA = 1f / parameters.LoadProfile.SceneProfiles.Length;
            for (int i = 0; i < parameters.LoadProfile.SceneProfiles.Length; i++)
            {
                var sceneProfile = parameters.LoadProfile.SceneProfiles[i];
                var invB = 1f / sceneProfile.Scenes.Length;
                for (int j = 0; j < sceneProfile.Scenes.Length; j++)
                {
                    var scene = sceneProfile.Scenes[j];
                    var handle = Addressables.LoadSceneAsync(scene.Asset, scene.LoadSceneMode);

                    while (!handle.IsDone)
                    {
                        Progress += handle.PercentComplete * invA * invB;
                        yield return null;
                    }
                    
                    if (scene.SetActive)
                        SceneManager.SetActiveScene(handle.Result.Scene);
                }
            }*/

            Progress = 1f;
            result?.Invoke(new IStepResult.Success());
        }
    }
}