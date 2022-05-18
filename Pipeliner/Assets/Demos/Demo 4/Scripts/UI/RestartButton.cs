using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button Button;

    private LoadManager _loadManager;
    private Level _level;

    private void Start()
    {
        _loadManager = LoadManager.Instance;
        _level = Level.Instance;
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
        _loadManager.Load(_level.LevelData.Data.Profile);
    }
}