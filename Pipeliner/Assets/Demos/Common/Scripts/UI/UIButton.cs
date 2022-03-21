// Used with Bolt/Visual Scripting
/*
using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    public string Identifier;
    
    public Button Button { get; protected set; }
    
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Identifier))
            Identifier = gameObject.name;
    }

    private void Awake()
    {
        Button = GetComponent<Button>();
        
        Button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        EventBus.Trigger(UIEvents.ButtonEvent, Identifier);
    }
}
*/
