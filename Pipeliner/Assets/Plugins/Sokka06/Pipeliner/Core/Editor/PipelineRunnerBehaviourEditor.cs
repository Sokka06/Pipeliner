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
            _pipelineRunner.RunPipeline();
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

        private VisualElement _container;
        private Label _stepsLabel;
        private Label _stateLabel;
        private Label _progressLabel;
        private LogElement _log;

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

            _container = new VisualElement();
            _foldout.Add(_container);
            
            _stepsLabel = new Label("");
            _container.Add(_stepsLabel);

            _stateLabel = new Label("");
            _container.Add(_stateLabel);
            
            _progressLabel = new Label("");
            _container.Add(_progressLabel);

            _log = new LogElement(pipelineRunner);
            _container.Add(_log);
        }

        public void UpdateElement()
        {
            _log.UpdateElement();
            
            if (!_foldout.visible)
                return;

            if (_pipelineRunner.Runner == null)
            {
                _helpBox.text = "Run the pipeline to display useful information here.";
                _helpBox.style.display = DisplayStyle.Flex;
                
                _container.style.display = DisplayStyle.None;
                return;
            }
            
            _helpBox.style.display = DisplayStyle.None;
            _container.style.display = DisplayStyle.Flex;

            _stepsLabel.text = $"Step Count: {(_pipelineRunner.Runner.Pipeline.Steps.Length.ToString())}";
            _stateLabel.text = $"State: {(_pipelineRunner.Runner.State.CurrentState.GetType().Name)}";
            _progressLabel.text = $"Progress: {(_pipelineRunner.Runner.Progress.ToString("P"))} ({(_pipelineRunner.Runner.StepIndex + 1).ToString()}/{_pipelineRunner.Runner.Pipeline.Steps.Length})";
        }
    }
    
    public class LogElement : VisualElement
    {
        private Foldout _foldout;

        private VisualElement _container;
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

            _container = new VisualElement();
            _foldout.Add(_container);

            _logBox = new HelpBox
            {
                messageType = HelpBoxMessageType.None
            };
            _container.Add(_logBox);

            _clearButton = new Button(OnClearButtonClicked)
            {
                text = "Clear"
            };
            _container.Add(_clearButton);
        }

        public void UpdateElement()
        {
            if (!_foldout.visible)
                return;

            if (_pipelineRunner.Runner == null)
            {
                _container.style.display = DisplayStyle.None;
                return;
            }
            
            _container.style.display = DisplayStyle.Flex;
            
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
            //TODO: Fix error caused by onGUIHandler when target serialized object is destroyed while inspector is open.
            var container = new IMGUIContainer();
            container.onGUIHandler = () => DrawDefaultInspector();
            
            return container;
        }
        
        private VisualElement CreateCustomInspector()
        {
            var container = new VisualElement();
            
            // Add a 8px space between default inspector and custom inspector.
            var space = new VisualElement
            {
                style =
                {
                    height = 8
                }
            };
            container.Add(space);

            // Add Controls
            container.Add(CreateControls());
            
            // Add Info
            container.Add(CreateInfo());

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
    }
}