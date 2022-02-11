using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sokka06.Pipeliner
{
    [CustomEditor(typeof(PipelineBehaviour))]
    public class PipelineBehaviourEditor : Editor
    {
        private PipelineBehaviour _target;
        private VisualElement _root;
        private VisualElement _controls;
        
        public virtual void OnEnable()
        {
            _target = (PipelineBehaviour)target;
            _root = new VisualElement();
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            _root.Add(CreateDefaultInspector());
            _root.Add(CreateCustomInspector());
            
            return _root;
        }
        
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

            return container;
        }
        
        private VisualElement CreateControls()
        {
            _controls = new VisualElement();

            var button = new Button(() =>
            {
                _target.Steps = new List<StepHandlerBehaviour>(_target.FindSteps());
                EditorUtility.SetDirty(target);
            })
            {
                text = "Update Steps"
            };
            _controls.Add(button);

            return _controls;
        }
    }
}