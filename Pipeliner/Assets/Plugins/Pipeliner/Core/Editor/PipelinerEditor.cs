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

        private readonly Pipeliner _pipeliner;

        public ControlsElement(Pipeliner pipeliner)
        {
            _pipeliner = pipeliner;

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
            if (_pipeliner.Runner == null)
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
            Debug.Log("Run Button Clicked");
            _pipeliner.StartCoroutine(_pipeliner.Run(result =>
            {
                Debug.Log("Pipeline ran from Editor. " + result);
            }));
        }
        
        private void OnAbortButtonClicked()
        {
            if (!(_pipeliner.Runner.State.CurrentState is IPipelineState.Running))
                return;
            
            Debug.Log("Abort Button Clicked");
        }
    }
    
    public class InfoElement : VisualElement
    {
        private Foldout _foldout;
        private HelpBox _helpBox;

        private Label _stepsLabel;
        private Label _stateLabel;
        private Label _progressLabel;

        private readonly Pipeliner Pipeliner;

        public InfoElement(Pipeliner pipeliner)
        {
            Pipeliner = pipeliner;
            
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
            
            if (Pipeliner.Runner == null)
            {
                _helpBox.text = "Run the pipeline to display useful information here.";
                _helpBox.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                return;
            }
            
            _helpBox.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            
            _stepsLabel.text = $"Step Count: {(Pipeliner.Runner.Steps.Length.ToString())}";
            _stateLabel.text = $"State: {(Pipeliner.Runner.State.CurrentState.GetType().Name)}";
            _progressLabel.text = $"Progress: {(Pipeliner.Runner.Progress.ToString("P"))} ({(Pipeliner.Runner.StepIndex + 1).ToString()}/{Pipeliner.Runner.Steps.Length})";
        }
    }
    
    [CustomEditor(typeof(Pipeliner))]
    public class PipelinerEditor : Editor
    {
        private Pipeliner _target;
        private VisualElement _root;

        private ControlsElement _controlsElement;
        private InfoElement _infoContainer;
        
        public virtual void OnEnable()
        {
            _target = (Pipeliner)target;
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
            _infoContainer?.UpdateElement();
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
            
            return container;
        }
        
        private VisualElement CreateControls()
        {
            _controlsElement = new ControlsElement(_target);
            return _controlsElement;
        }

        private VisualElement CreateInfo()
        {
            _infoContainer = new InfoElement(_target);
            return _infoContainer;
        }
    }
}