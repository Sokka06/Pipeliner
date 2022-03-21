using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoUI : MonoBehaviour
{
    public UIView View;
    [Space] 
    public Button StartButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnDestroy()
    {
        StartButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        View.Manager.GetView("Initialization View").Show();
        View.Hide();
    }
}
