using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Sokka06.Pipeliner
{
    [Serializable]
    public struct AddressableScene
    {
        public bool SetActive;
        public LoadSceneMode LoadSceneMode;
        public AssetReference Asset;

        public AddressableScene( bool setActive = false, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, AssetReference asset = default)
        {
            SetActive = setActive;
            LoadSceneMode = loadSceneMode;
            Asset = asset;
        }
    }

    [Serializable]
    public struct AddressableSceneParameters : IStepParameters
    {
        public List<AddressableScene> Scenes;
    }
    
    public class AddressableSceneStep : AbstractStep
    {
        public AddressableSceneStep(IStepParameters parameters) : base(parameters)
        {
        }
        
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);

            var parameters = (AddressableSceneParameters)Parameters;

            for (int i = 0; i < parameters.Scenes.Count; i++)
            {
                if (!parameters.Scenes[i].Asset.RuntimeKeyIsValid())
                {
                    Debug.Log("No Addressable Scene set.");
                    continue;
                }

                var handle = default(AsyncOperationHandle<SceneInstance>);
                yield return handle = parameters.Scenes[i].Asset.LoadSceneAsync(parameters.Scenes[i].LoadSceneMode);
                
                if(parameters.Scenes[i].SetActive)
                    SceneManager.SetActiveScene(handle.Result.Scene);

                Progress = 1f;
            }
        }
    }
}