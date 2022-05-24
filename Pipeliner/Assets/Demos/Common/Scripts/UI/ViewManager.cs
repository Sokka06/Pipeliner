using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ViewEntry
{
    public bool ShowOnStart;
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

    private void Start()
    {
        for (int i = 0; i < Views.Count; i++)
        {
            var entry = Views[i];
            
            if (entry.ShowOnStart)
                entry.View.Show();
        }
    }

    public void SetupViews()
    {
        for (int i = 0; i < Views.Count; i++)
        {
            var entry = Views[i];
            
            entry.View.Setup(this);
            entry.View.Hide();
            
            RegisterView(entry.View);
        }
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
        if (!RegisteredViews.ContainsKey(identifier))
            return null;
        
        return RegisteredViews[identifier];
    }

    [ContextMenu("Find Views")]
    public void FindViews()
    {
        var views = FindObjectsOfType<UIView>();
        
        Views = new List<ViewEntry>(views.Length);
        for (int i = 0; i < views.Length; i++)
        {
            Views.Add(new ViewEntry
            {
                ShowOnStart = false,
                View = views[i]
            });
        }
    }
}
