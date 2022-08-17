using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.Demo3
{
    public class InitializationUI : MonoBehaviour
    {
        public UIView View;
        public PipelineRunnerBehaviour RunnerBehaviour;
    
        [Space] 
        public Slider ProgressBar;
        public TextMeshProUGUI StepText;
        
        private void Awake()
        {
            View.State.onStateChanged += OnViewStateChanged;
        }
        
        private void OnDestroy()
        {
            View.State.onStateChanged -= OnViewStateChanged;
        }

        private void OnViewStateChanged((IViewState previous, IViewState current) state)
        {
            if (state.current is IViewState.Visible)
            {
                var result = default(IPipelineResult);
                RunnerBehaviour.RunPipeline(value =>
                {
                    result = value;
                    View.Manager.GetView("Finish View").Show();
                    View.Hide();
                });
            }
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
                case PlayServicesStep playServices:
                    return "Google Play Services...";
                case FirebaseStep firebase:
                    return "Firebase...";
                case LoadDataStep<GameData> userData:
                    return $"Save Data...";
                default:
                    return "";
            }
        }
    }
}