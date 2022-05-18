using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds an array of Scene Profiles that will be loaded.
/// </summary>
[CreateAssetMenu(menuName = "Demos/Load Profile", fileName = "Load Profile")]
public class LoadProfile : ScriptableObject
{
    public string Name;
    public SceneProfile[] SceneProfiles;
}
