using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEditor;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public static class EditorTools
    {
        [MenuItem("GameObject/Pipeliner/Pipeline Runner", false)]
        static void CreatePipelineRunner(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("Pipeline Runner");
            var runner = go.AddComponent<PipelineRunnerBehaviour>();
        
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/Pipeliner/Pipeline", false)]
        static void CreatePipeline(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("Pipeline");
            var pipeline = go.AddComponent<PipelineBehaviour>();
        
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}