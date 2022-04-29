using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public int Id { get; set; }
    public int Likes { get; set; }
}

[Serializable]
public struct GameData : IData
{
    public string FileName { get; set; }
    public List<ItemData> Datas { get; set; }
}
