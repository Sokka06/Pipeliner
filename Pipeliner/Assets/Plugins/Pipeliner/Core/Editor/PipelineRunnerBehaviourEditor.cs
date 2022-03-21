using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sokka06.Pipeliner
{
    public class ControlsElement : VisualElement
    {
        private Button _runButton;
        private Button _abortButton;

        private readonly PipelineRunnerBehaviour _pipelineRunner;

        public ControlsElement(PipelineRunnerBehaviour pipelineRunner)
        {
            _pipelineRunner = pipelineRunner;

            _runButton = new Button(OnRunButtonClicked)
            {
                text = "Run"
            };
            Add(_runButton);
            
            _abortButton = new Button(OnAbortButtonClicked)
            {
                text = "Abort"
            };
            Add(_abortButton);
        }

        public void UpdateElement()
        {
            if (_pipelineRunner.Pipeline == null || !EditorApplication.isPlaying)
            {
                _runButton.SetEnabled(false);
                _abortButton.SetEnabled(false);
                return;
            }

            if (!_runButton.enabledSelf)
                _runButton.SetEnabled(true);
            if (!_abortButton.enabledSelf)
                _abortButton.SetEnabled(true);
        }

        private void OnRunButtonClicked()
        {
            _pipelineRunner.Run();
        }
        
        private void OnAbortButtonClicked()
        {
            _pipelineRunner.Runner?.Abort();
        }
    }
    
    public class InfoElement : VisualElement
    {
        private Foldout _foldout;
        private HelpBox _helpBox;

        private Label _stepsLabel;
        private Label _stateLabel;
        private Label _progressLabel;

        private readonly PipelineRunnerBehaviour _pipelineRunner;

        public InfoElement(PipelineRunnerBehaviour pipelineRunner)
        {
            _pipelineRunner = pipelineRunner;
            
            _foldout = new Foldout
            {
                text = "Info"
            };
            Add(_foldout);

            _helpBox = new HelpBox("", HelpBoxMessageType.Info);
            _foldout.Add(_helpBox);
            
            _stepsLabel = new Label("");
            _foldout.Add(_stepsLabel);

            _stateLabel = new Label("");
            _foldout.Add(_stateLabel);
            
            _progressLabel = new Label("");
            _foldout.Add(_progressLabel);
        }

        public void UpdateElement()
        {
            if (!_foldout.visible)
                return;
            
            if (_pipelineRunner.Runner == null)
            {
                _helpBox.text = "Run the pipeline to display useful information here.";
                _helpBox.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                return;
            }
            
            _helpBox.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            
            _stepsLabel.text = $"Step Count: {(_pipelineRunner.Runner.Pipeline.Steps.Length.ToString())}";
            _stateLabel.text = $"State: {(_pipelineRunner.Runner.State.CurrentState.GetType().Name)}";
            _progressLabel.text = $"Progress: {(_pipelineRunner.Runner.Progress.ToString("P"))} ({(_pipelineRunner.Runner.StepIndex + 1).ToString()}/{_pipelineRunner.Runner.Pipeline.Steps.Length})";
        }
    }
    
    public class LogElement : VisualElement
    {
        private Foldout _foldout;
        private HelpBox _logBox;
        private Button _clearButton;

        private readonly PipelineRunnerBehaviour _pipelineRunner;

        public LogElement(PipelineRunnerBehaviour pipelineRunner)
        {
            _pipelineRunner = pipelineRunner;
            
            _foldout = new Foldout
            {
                text = "Log"
            };
            Add(_foldout);

            _logBox = new HelpBox
            {
                messageType = HelpBoxMessageType.None
            };
            _foldout.Add(_logBox);

            _clearButton = new Button(OnClearButtonClicked)
            {
                text = "Clear"
            };
            _foldout.Add(_clearButton);
        }

        public void UpdateElement()
        {
            if (!_foldout.visible)
                return;
            
            if (_pipelineRunner.Runner == null)
            {
                var hidden = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                _logBox.style.display = hidden;
                _clearButton.style.display = hidden;
                return;
            }

            var visible = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _logBox.style.display = visible;
            _clearButton.style.display = visible;
            
            _logBox.text = _pipelineRunner.Runner.Logger.Logs.ToString();
        }

        private void OnClearButtonClicked()
        {
            _pipelineRunner.Runner.Logger.Clear();
        }
    }
    
    [CustomEditor(typeof(PipelineRunnerBehaviour))]
    public class PipelineRunnerBehaviourEditor : Editor
    {
        private PipelineRunnerBehaviour _target;
        private VisualElement _root;

        private ControlsElement _controlsElement;
        private InfoElement _infoElement;
        private LogElement _logElement;
        
        public virtual void OnEnable()
        {
            _target = (PipelineRunnerBehaviour)target;
            _root = new VisualElement();
            
            EditorApplication.update += UpdateInspector;
        }

        public virtual void OnDisable()
        {
            EditorApplication.update -= UpdateInspector;
        }
        
        private void UpdateInspector()
        {
            _controlsElement?.UpdateElement();
            _infoElement?.UpdateElement();
            _logElement?.UpdateElement();
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            _root.Add(CreateDefaultInspector());
            _root.Add(CreateCustomInspector());
            
            return _root;
        }
        
        /// <summary>
        /// Creates the default inspector Visual Element.
        /// </summary>
        /// <returns></returns>
        private VisualElement CreateDefaultInspector()
        {
            var container = new IMGUIContainer();
            container.onGUIHandler = () => DrawDefaultInspector();
            
            return container;
        }
        
        private VisualElement CreateCustomInspector()
        {
            var container = new VisualElement();
            
            // Add a 8px space between default inspector and custom inspector.
            var space = new VisualElement();
            space.style.height = 8;
            container.Add(space);

            // Add Controls
            container.Add(CreateControls());
            
            // Add Info
            container.Add(CreateInfo());
            
            // Add Log
            container.Add(CreateLog());
            
            return container;
        }
        
        private VisualElement CreateControls()
        {
            _controlsElement = new ControlsElement(_target);
            return _controlsElement;
        }

        private VisualElement CreateInfo()
        {
            _infoElement = new InfoElement(_target);
            return _infoElement;
        }
        
        private VisualElement CreateLog()
        {
            _logElement = new LogElement(_target);
            return _logElement;
        }
    }
}