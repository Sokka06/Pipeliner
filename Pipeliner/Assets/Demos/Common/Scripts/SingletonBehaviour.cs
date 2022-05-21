using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Common
{
    /// <summary>
    /// Dirty filthy singleton from https://github.com/UnityCommunity/UnitySingleton/blob/master/Assets/Scripts/Singleton.cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : Component
    {
        public bool DontDestroyOnLoad = true;
        
        #region Fields
    
        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;
    
        #endregion
    
        #region Properties
    
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = FindObjectOfType<T> ();
                    if ( instance == null )
                    {
                        var obj = new GameObject
                        {
                            name = typeof ( T ).Name
                        };
                        instance = obj.AddComponent<T> ();
                    }
                }
                return instance;
            }
        }
    
        #endregion
    
        #region Methods
    
        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected virtual void Awake ()
        {
            if ( instance == null )
            {
                instance = this as T;
                
                if(DontDestroyOnLoad)
                    DontDestroyOnLoad ( gameObject );
            }
            else
            {
                Destroy ( gameObject );
            }
        }
    
        #endregion
    	
    }
}