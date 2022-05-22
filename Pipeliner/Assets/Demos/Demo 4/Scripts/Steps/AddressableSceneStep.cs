using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Demos.Demo4
{
    public struct AddressableSceneStepParameters : IStepParameters
    {
        public AddressableScene Scene;

        public AddressableSceneStepParameters(AddressableScene scene)
        {
            Scene = scene;
        }
    }

    /// <summary>
    /// Loads an Addressable Scene.
    /// </summary>
    public class AddressableSceneStep : AbstractStep
    {
        public AddressableSceneStep(AddressableSceneStepParameters parameters) : base(parameters)
        {
        }

        private AsyncOperationHandle<SceneInstance> _handle;

        public override IEnumerator Run(Action<IStepResult> result)
        {
            var parameters = (AddressableSceneStepParameters)Parameters;
        
            if (!parameters.Scene.Asset.RuntimeKeyIsValid())
            {
                Debug.Log("No Addressable Scene set.");
            }
        
            _handle = Addressables.LoadSceneAsync(parameters.Scene.Asset, parameters.Scene.LoadSceneMode);

            while (!_handle.IsDone)
            {
                Progress = _handle.PercentComplete / 0.9f;
                yield return null;
            }

            if (parameters.Scene.SetActive)
                SceneManager.SetActiveScene(_handle.Result.Scene);

            Progress = 1f;
            result?.Invoke(new IStepResult.Success());
        }
    }
}