using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.Demo3
{
    public class UserUI : MonoBehaviour
    {
        public UIView View;
    
        [Space]
        public ImageLoader Avatar;
        public TextMeshProUGUI EmailText;
        public TextMeshProUGUI NameText;

        private void Awake()
        {
            View.State.OnStateChanged += OnViewStateChanged;
        }

        private void OnDestroy()
        {
            View.State.OnStateChanged -= OnViewStateChanged;
        }
    
        private void OnViewStateChanged((IViewState previous, IViewState current) state)
        {
            if (state.current is IViewState.Visible)
            {
                Avatar.LoadImage(FakePlayServices.Instance.Data.data.avatar);
                EmailText.SetText(FakePlayServices.Instance.Data.data.email);
                NameText.SetText($"{FakePlayServices.Instance.Data.data.first_name} {FakePlayServices.Instance.Data.data.last_name}");
            }
        }
    }
}