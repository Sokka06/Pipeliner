using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.Demo4
{
    public class LoadButton : MonoBehaviour
    {
        public LoadProfile Profile;
        public Button Button;

        private LoadManager _loadManager;

        private void Start()
        {
            _loadManager = LoadManager.Instance;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _loadManager.Load(Profile);
        }
    }
}