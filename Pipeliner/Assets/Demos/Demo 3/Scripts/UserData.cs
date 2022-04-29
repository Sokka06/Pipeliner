using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using UnityEngine;

[Serializable]
public struct UserData : IData
{
    public string FileName { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}";
    }
}