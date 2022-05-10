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
    public RectTransform Container;
    
    public ViewManager Manager { get; protected set; }
    public CanvasGroup CanvasGroup { get; protected set; }
    public RectTransform RectTransform => transform as RectTransform;
    public StateMachine<IViewState> State { get; protected set; } = new StateMachine<IViewState>(new IViewState.Default());

    protected virtual void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
            Id = gameObject.name;

        if (Container == null)
        {
            var container = transform.Find("Container");
            /*if (container == null)
            {
                var go = new GameObject
                {
                    name = "Container",
                    transform =
                    {
                        parent = transform
                    }
                };
                container = go.transform as RectTransform;
            }*/
            Container = container as RectTransform;
        }
    }

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Setup(ViewManager viewManager)
    {
        Manager = viewManager;
    }

    public virtual void Show()
    {
        Container.gameObject.SetActive(true);
        State.SetState(new IViewState.Visible());
    }

    public virtual void Hide()
    {
        Container.gameObject.SetActive(false);
        State.SetState(new IViewState.Hidden());
    }
}
