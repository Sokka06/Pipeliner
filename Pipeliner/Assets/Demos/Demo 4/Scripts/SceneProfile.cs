using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds Addressable Scenes that will be loaded.
/// </summary>
[CreateAssetMenu(menuName = "Demos/Scene Profile", fileName = "Scene Profile")]
public class SceneProfile : ScriptableObject
{
    public AddressableScene[] Scenes;
}
