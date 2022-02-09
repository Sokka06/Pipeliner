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
    
    public class AddressableSceneStep : AbstractStepBehaviour
    {
        public List<AddressableScene> Scenes;

        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);

            for (int i = 0; i < Scenes.Count; i++)
            {
                if (!Scenes[i].Asset.RuntimeKeyIsValid())
                {
                    Debug.Log("No Addressable Scene set.");
                    continue;
                }

                var handle = default(AsyncOperationHandle<SceneInstance>);
                yield return handle = Scenes[i].Asset.LoadSceneAsync(Scenes[i].LoadSceneMode);
                
                if(Scenes[i].SetActive)
                    SceneManager.SetActiveScene(handle.Result.Scene);
            }
        }
    }
}