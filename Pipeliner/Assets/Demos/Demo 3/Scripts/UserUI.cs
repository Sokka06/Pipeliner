using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    public UIView View;
    public DataSystem DataSystem;
    [Space]
    public TMP_InputField NameField;
    public Button SaveButton;

    private UserData _userData;

    private void Awake()
    {
        View.State.OnStateChanged += OnViewStateChanged;
        SaveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    private void OnDestroy()
    {
        View.State.OnStateChanged -= OnViewStateChanged;
        SaveButton.onClick.RemoveListener(OnSaveButtonClicked);
    }
    
    private void OnViewStateChanged((IViewState previous, IViewState current) state)
    {
        if (state.current is IViewState.Visible)
        {
            _userData = DataSystem.Get<UserData>(new UserData());
            NameField.text = _userData.Name;
        }
    }

    private void OnSaveButtonClicked()
    {
        _userData.Name = NameField.text;
        DataSystem.Save<UserData>(_userData);
    }
}
