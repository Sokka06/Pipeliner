using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public UIView InitializationView;
    public PipelineRunnerBehaviour RunnerBehaviour;

    private void Awake()
    {
        InitializationView.State.OnStateChanged += OnViewStateChanged;
    }
    
    private void OnDestroy()
    {
        InitializationView.State.OnStateChanged -= OnViewStateChanged;
    }
    
    private void OnViewStateChanged((IViewState previous, IViewState current) state)
    {
        if (state.current is IViewState.Visible)
        {
        }
    }
}
