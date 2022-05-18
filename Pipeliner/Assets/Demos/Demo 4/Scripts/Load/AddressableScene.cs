using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

[Serializable]
public struct AddressableScene
{
    public bool SetActive;
    public LoadSceneMode LoadSceneMode;
    public AssetReference Asset;

    public AddressableScene(bool setActive = false, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, AssetReference asset = default)
    {
        SetActive = setActive;
        LoadSceneMode = loadSceneMode;
        Asset = asset;
    }
}
