using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Demo4;
using DG.Tweening;
using Sokka06.Pipeliner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.Demo4
{
    public class LoadingUI : MonoBehaviour
    {
        public UIView View;
        
        [Space]
        public Slider ProgressBar;
        public TextMeshProUGUI NameText;
    
        private LoadManager _loadManager;
        private bool _isLoading;
    
        private void OnValidate()
        {
            if (View == null)
                View = GetComponent<UIView>();
        }
    
        private void Start()
        {
            _loadManager = LoadManager.Instance;
            _loadManager.onLoadBegin += OnLoadBegin;
            _loadManager.onLoadFinish += OnLoadFinish;
        }
    
        private void OnDestroy()
        {
            _loadManager.onLoadBegin -= OnLoadBegin;
            _loadManager.onLoadFinish -= OnLoadFinish;
        }
    
        private void OnEnable()
        {
            
        }
    
        private void OnDisable()
        {
            
        }
    
        private void LateUpdate()
        {
            if (_isLoading)
                ProgressBar.value = _loadManager.Runner.Progress;
        }
    
        private void OnLoadBegin(LoadProfile profile)
        {
            _isLoading = true;
            
            NameText.SetText(profile.Name);
            
            View.Show();
            View.CanvasGroup.DOFade(1f, 0.5f).ChangeStartValue(0f);
        }
        
        private void OnLoadFinish(LoadProfile load)
        {
            _isLoading = false;
            
            View.CanvasGroup.DOFade(0f, 0.5f).ChangeStartValue(1f).OnComplete(() =>
            {
                View.Hide();
            });
        }
    }
}