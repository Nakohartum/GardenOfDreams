using System;
using System.Collections.Generic;
using System.Linq;
using _Root.BuildingFeature.Code;
using _Root.BuildingFeature.Code.Factory;
using _Root.BuildingFeature.Code.Presenter;
using _Root.BuildingFeature.Code.ScriptableObjectCode;
using _Root.Input;
using _Root.UIFeature.Code.Components;
using _Root.UIFeature.Code.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Root.UIFeature.Code.Presenter
{
    public class BottomPanelPresenter : IDisposable
    {
        private BottomPanelView _view;
        private BuildingSO[] _buildings;
        private BuildingItem _buildingItemPrefab;
        private int _currentSelectedItem = -1;
        private List<BuildingItem> _cachedBuildingItems = new List<BuildingItem>();
        private BuildController _buildController;
        public event Action OnBuildingChosen = delegate { }; 
        public event Action<bool> OnDeleteClicked = delegate { }; 

        public BottomPanelPresenter(BottomPanelView view, BuildingSO[] buildingItems, BuildingItem buildingItemPrefab, BuildController buildController)
        {
            _view = view;
            _buildings = buildingItems;
            _buildingItemPrefab = buildingItemPrefab;
            _buildController = buildController;
            InitializeView();
        }

        private void InitializeView()
        {
            InflateBuildings();
            SubscribeToButtons();
        }

        private void SubscribeToButtons()
        {
            _view.PlaceButton.onClick.AddListener(AddButtonClicked);
            _view.DeleteButton.onClick.AddListener(DeleteButtonClicked);
        }

        private void DeleteButtonClicked()
        {
            OnDeleteClicked(true);
        }

        private void AddButtonClicked()
        {
            if (_currentSelectedItem != -1)
            {
                OnDeleteClicked(false);
                _buildController.CreateBuilding(_currentSelectedItem);
                OnBuildingChosen();
            }
        }

        private void InflateBuildings()
        {
            for (int i = 0; i < _buildings.Length; i++)
            {
                var buildingItem = Object.Instantiate(_buildingItemPrefab, _view.BuildingsContainer.transform);
                buildingItem.InitializeView(_buildings[i].BuildingImage, i);
                buildingItem.Button.onClick.AddListener(ToggleSelectedState);
                buildingItem.OnSelected += OnItemSelected;
                _cachedBuildingItems.Add(buildingItem);
            }
        }

        private void OnItemSelected(int obj)
        {
            _currentSelectedItem = obj;
        }

        public void ToggleSelectedState()
        {
            for (int i = 0; i < _cachedBuildingItems.Count; i++)
            {
                if (_cachedBuildingItems[i].IsSelected)
                {
                    _cachedBuildingItems[i].IsSelected = false;
                }
                else
                {
                    _cachedBuildingItems[i].IsSelected = _currentSelectedItem == i;
                }
                _cachedBuildingItems[i].BackgroundImage.color = _cachedBuildingItems[i].IsSelected ? _cachedBuildingItems[i].SelectedColor : _cachedBuildingItems[i].UnselectedColor;
            }

            if (!_cachedBuildingItems.Select(q => q.IsSelected).Any())
            {
                _currentSelectedItem = -1;
            }
        }

        public void Dispose()
        {
            _view.PlaceButton.onClick.RemoveListener(AddButtonClicked);
            _view.DeleteButton.onClick.RemoveListener(DeleteButtonClicked);
        }
    }
}