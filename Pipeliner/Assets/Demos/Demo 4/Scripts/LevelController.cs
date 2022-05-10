using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Demos.Demo4
{
    public struct LevelFinishData
    {
        public float Time;
    }
    
    public interface ILevelState : IState
    {
        public struct Default : ILevelState { }
        public struct Started : ILevelState { }
        public struct Finished : ILevelState { }
        public struct Failed : ILevelState { }
    }
    
    public class LevelController : MonoBehaviour
    {
        public LevelDataObject Data;

        public LevelFinishData FinishData { get; private set; }
        public StateMachine<ILevelState> State { get; private set; }
        public float CurrentTime { get; private set; }

        public event Action onLevelStart;
        public event Action<LevelFinishData> onLevelFinish;

        private void Awake()
        {
            State = new StateMachine<ILevelState>(new ILevelState.Default());
        }

        private void Start()
        {
            StartLevel();
        }

        private void FixedUpdate()
        {
            UpdateLevel(Time.deltaTime);
        }

        public void StartLevel()
        {
            State.SetState(new ILevelState.Started());
            FindObjectOfType<Finish>().onFinish += ONFinish;
            
            onLevelStart?.Invoke();
        }

        private void ONFinish(VehicleController obj)
        {
            FinishLevel();
        }

        public void UpdateLevel(float deltaTime)
        {
            if(!(State.CurrentState is ILevelState.Started))
                return;
            
            CurrentTime += deltaTime;
        }

        public void FinishLevel()
        {
            FindObjectOfType<Finish>().onFinish -= ONFinish;

            State.SetState(new ILevelState.Finished());            
            var levelData = new LevelFinishData
            {
                Time = CurrentTime
            };
            FinishData = levelData;
            onLevelFinish?.Invoke(levelData);
        }
    }
}
