using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Common
{
    [CreateAssetMenu(fileName = "Demo Info", menuName = "Demos/Info")]
    public class DemoInfo : ScriptableObject
    {
        public string Title = "Demo";
        public string Subtitle = "Subtitle";
        [TextArea(4, 8)]
        public string Description = "Description";
    }
}