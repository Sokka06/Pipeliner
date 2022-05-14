using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public abstract class AbstractViewController : MonoBehaviour
    {
        public UIView View;
    
        protected virtual void OnValidate()
        {
            if (View == null)
                View = GetComponent<UIView>();
        }
    }
}