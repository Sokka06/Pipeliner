using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ViewEntry
{
    public bool HideOnSetup = true;
    public UIView View;
}

/// <summary>
/// A barebones class for managing UI views.
/// </summary>
[DefaultExecutionOrder(-10)]
public class ViewManager : MonoBehaviour
{
    public List<ViewEntry> Views;

    public Dictionary<string, UIView> RegisteredViews;

    private void Awake()
    {
        RegisteredViews = new Dictionary<string, UIView>(Views.Count);
        SetupViews();
    }

    public void SetupViews()
    {
        for (int i = 0; i < Views.Count; i++)
        {
            var entry = Views[i];
            
            entry.View.Setup(this);
            if (entry.HideOnSetup)
                entry.View.Hide();
            
            RegisterView(entry.View);
        }
    }

    public UIView[] FindViews()
    {
        return FindObjectsOfType<UIView>();
    }

    private void RegisterView(UIView view)
    {
        var id = view.Id;
        if (RegisteredViews.ContainsKey(id))
            return;
        
        RegisteredViews.Add(id, view);
    }

    public UIView GetView(string identifier)
    {
        return RegisteredViews[identifier];
    }

    public UIView GetView(UIView view)
    {
        return GetView(view.Id);
    }
}
