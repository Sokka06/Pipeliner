using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button Button;
    public TextMeshProUGUI NameText;

    private LevelDataObject _levelData;

    public event Action<LevelDataObject> onButtonClicked; 

    public LevelDataObject LevelData
    {
        get => _levelData;
        set
        {
            _levelData = value;
            NameText.SetText(value.Data.Name);
        }
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
        onButtonClicked?.Invoke(LevelData);
    }
}
