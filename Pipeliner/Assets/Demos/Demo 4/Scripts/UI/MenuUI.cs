using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class MenuUI : MonoBehaviour
    {
        public LevelButton LevelButtonTemplate;
        public RectTransform Container;
        public LevelDataObject[] Levels;

        private LoadManager _loadManager;

        public List<LevelButton> LevelButtons { get; private set; }

        private void Start()
        {
            _loadManager = LoadManager.Instance;
        
            LevelButtons = new List<LevelButton>(Levels.Length);
            for (int i = 0; i < Levels.Length; i++)
            {
                var button = InstantiateLevelButton(Levels[i]);
                button.onButtonClicked += OnLevelButtonClicked;
                LevelButtons.Add(button);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < LevelButtons.Count; i++)
            {
                LevelButtons[i].onButtonClicked -= OnLevelButtonClicked;
            }
        }

        private void OnLevelButtonClicked(LevelDataObject obj)
        {
            _loadManager.Load(obj.Data.Profile);
        }

        private LevelButton InstantiateLevelButton(LevelDataObject levelData)
        {
            var button = Instantiate(LevelButtonTemplate, Container);
            button.LevelData = levelData;
            return button;
        }
    }
}