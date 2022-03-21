using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitializationUI : MonoBehaviour
{
    public UIView View;
    public PipelineRunnerBehaviour RunnerBehaviour;

    [Space] 
    public Slider ProgressBar;
    public TextMeshProUGUI StepText;
    
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
        var result = default(IPipelineResult);
        RunnerBehaviour.Run(value =>
        {
            result = value;
            View.Manager.GetView("Finish View").Show();
            View.Hide();
        });
    }
    
    private void OnViewStateChanged((IViewState previous, IViewState current) state)
    {
        // Used with Bolt/Visual Scripting
        /*if (state.current is IViewState.Visible)
        {
            var result = default(IPipelineResult);
            RunnerBehaviour.Run(value =>
            {
                result = value;
                EventBus.Trigger(UIEvents.ViewEvent, "Finish View");
            });
        }*/
    }

    private void LateUpdate()
    {
        if (RunnerBehaviour.Runner != null)
        {
            ProgressBar.value = RunnerBehaviour.Runner.Progress;
            StepText.SetText(GetStepDescription(RunnerBehaviour.Runner.Pipeline.Steps[RunnerBehaviour.Runner.StepIndex]));
        }
    }

    private string GetStepDescription(IStep step)
    {
        switch (step)
        {
            case LoadDataStep<UserData> userData:
                return $"User Data...";
            default:
                return "";
        }
    }
}
