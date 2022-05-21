using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demos.Common;
using UnityEngine;

namespace Demos.Demo3
{
    public class ItemsUI : MonoBehaviour
    {
        public UIView View;
        
        [Space]
        public DataSystem DataSystem;
        public ItemUI ItemTemplate;
        public RectTransform Container;
        
        private GameData _gameData;

        public List<ItemUI> Items { get; private set; } = new List<ItemUI>();

        private void Awake()
        {
            View.State.OnStateChanged += OnViewStateChanged;
        }

        private void OnDestroy()
        {
            View.State.OnStateChanged -= OnViewStateChanged;
            
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].onCountChanged -= OnCountChanged;
            }
        }
        
        private void OnViewStateChanged((IViewState previous, IViewState current) state)
        {
            if (state.current is IViewState.Visible)
            {
                // Initialize save data.
                _gameData = DataSystem.Get<GameData>();
                if (_gameData.Datas == null)
                    _gameData.Datas = new List<ItemData>(FakeFirebase.Instance.Data.data.Count);
                
                // Create item cards.
                for (int i = 0; i < FakeFirebase.Instance.Data.data.Count; i++)
                {
                    var item = InstantiateItem(FakeFirebase.Instance.Data.data[i]);

                    var index = _gameData.Datas.FindIndex(data => data.Id == item.Data.id);
                    if (index >= 0)
                    {
                        // Load likes from save
                        var data = _gameData.Datas[index];
                        item.SetCount(data.Likes);
                    }
                    else
                    {
                        // Add new item to save
                        _gameData.Datas.Add(new ItemData
                        {
                            Id = item.Data.id,
                            Likes = 0
                        });
                    }
                    
                    item.onCountChanged += OnCountChanged;
                    Items.Add(item);
                }
            }
        }

        private void OnCountChanged((int id, int count) obj)
        {
            var (id, count) = obj;
            var index = _gameData.Datas.FindIndex(data => data.Id == id);
            
            var data = _gameData.Datas[index];
            data.Likes = count;
            _gameData.Datas[index] = data;
            
            DataSystem.Save(_gameData);
        }

        /// <summary>
        /// Creates an Item card.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ItemUI InstantiateItem(Datum data)
        {
            var item = Instantiate(ItemTemplate, Container);
            item.Data = data;
            
            return item;
        }
    }
}