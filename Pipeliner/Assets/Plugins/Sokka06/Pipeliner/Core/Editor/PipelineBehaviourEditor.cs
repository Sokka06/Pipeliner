using System;
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
            
            EditorApplication.update += UpdateInspector;
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateInspector;
        }

        private void UpdateInspector()
        {
            ValidateSteps();
        }

        private void ValidateSteps()
        {
            if (_target == null || EditorApplication.isPlaying)
                return;

            var hasChanged = false;
            var steps = _target.FindSteps();
            
            // Compare steps. Could also use LINQ SequenceEqual
            if (_target.Steps.Length != steps.Length)
            {
                hasChanged = true;
            }
            else
            {
                for (int i = 0; i < _target.Steps.Length; i++)
                {
                    if (_target.Steps[i].GetHashCode() == steps[i].GetHashCode())
                        continue;

                    hasChanged = true;
                    break;
                }
            }

            if (hasChanged)
                UpdateSteps(steps);
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

            return container;
        }
        
        private VisualElement CreateControls()
        {
            _controls = new VisualElement();

            var button = new Button(() =>
            {
                UpdateSteps(_target.FindSteps());
            })
            {
                text = "Update Steps"
            };
            _controls.Add(button);

            return _controls;
        }

        private void UpdateSteps(StepFactoryBehaviour[] steps)
        {
            _target.Steps = steps;
            EditorUtility.SetDirty(target);
        }
    }
}