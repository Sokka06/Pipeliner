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

        private Player _player;

        public LevelFinishData FinishData { get; private set; }

        public StateMachine<ILevelState> State { get; private set; } =
            new StateMachine<ILevelState>(new ILevelState.Default());
        public float CurrentTime { get; private set; }

        public event Action onLevelStart;
        public event Action onLevelFail;
        public event Action<LevelFinishData> onLevelFinish;

        private void Start()
        {
            State.SetState(new ILevelState.Default());

            _player = FindObjectOfType<Player>();
            _player.Vehicle.Health.onHealthMin += HealthOnonHealthMin;
        }

        private void OnDestroy()
        {
            _player.Vehicle.Health.onHealthMin -= HealthOnonHealthMin;
        }

        private void HealthOnonHealthMin(int obj)
        {
            FailLevel();
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

        private void ONFinish(Vehicle obj)
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
            if(!(State.CurrentState is ILevelState.Started))
                return;
            
            FindObjectOfType<Finish>().onFinish -= ONFinish;

            State.SetState(new ILevelState.Finished());            
            var levelData = new LevelFinishData
            {
                Time = CurrentTime
            };
            FinishData = levelData;
            onLevelFinish?.Invoke(levelData);
        }

        public void FailLevel()
        {
            if(!(State.CurrentState is ILevelState.Started))
                return;
            
            FindObjectOfType<Finish>().onFinish -= ONFinish;
            State.SetState(new ILevelState.Failed());
            onLevelFail?.Invoke();
        }
    }
}
