using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

public interface IViewState : IState
{
    public struct Default : IViewState { }
    public struct Hidden : IViewState { }
    public struct Visible : IViewState { }
}

[RequireComponent(typeof(CanvasGroup))]
public class UIView : MonoBehaviour
{
    public string Id;
    
    public ViewManager Manager { get; protected set; }
    public CanvasGroup CanvasGroup { get; protected set; }
    public RectTransform RectTransform => transform as RectTransform;
    public StateMachine<IViewState> State { get; protected set; }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
            Id = gameObject.name;
    }

    public virtual void Setup(ViewManager viewManager)
    {
        Manager = viewManager;
        CanvasGroup = GetComponent<CanvasGroup>();
        State = new StateMachine<IViewState>(new IViewState.Default());
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        State.SetState(new IViewState.Visible());
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        State.SetState(new IViewState.Hidden());
    }
}
