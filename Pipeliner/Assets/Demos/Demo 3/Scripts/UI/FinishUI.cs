using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using TMPro;
using UnityEngine;

public class FinishUI : MonoBehaviour
{
    public UIView View;
    public PipelineRunnerBehaviour RunnerBehaviour;

    [Space] 
    public TextMeshProUGUI Text;

    private void Awake()
    {
        View.State.OnStateChanged += OnViewStateChanged;
    }
    
    private void OnDestroy()
    {
        View.State.OnStateChanged -= OnViewStateChanged;
    }

    private void OnEnable()
    {
        //Text.SetText(GetResultDescription(RunnerBehaviour.Runner.Result));
    }

    private void OnViewStateChanged((IViewState previous, IViewState current) state)
    {
        if (state.current is IViewState.Visible)
        {
            Text.SetText(GetResultDescription(RunnerBehaviour.Runner.Result));
        }
    }
    
    private string GetResultDescription(IPipelineResult result)
    {
        switch (result)
        {
            case IPipelineResult.Aborted aborted:
                return $"Initialization aborted...";
            case IPipelineResult.Default @default:
                break;
            case IPipelineResult.Failed failed:
                return $"Something went wrong...";
            case IPipelineResult.Success success:
                return $"Done";
            default:
                return "";
        }

        return "";
    }
}
