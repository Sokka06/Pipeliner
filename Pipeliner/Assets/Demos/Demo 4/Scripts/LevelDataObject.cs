using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public string Name;
    public LoadProfile Profile;
}

[CreateAssetMenu(fileName = "Level Data", menuName = "Demos/Level Data")]
public class LevelDataObject : ScriptableObject
{
    public LevelData Data;
}
