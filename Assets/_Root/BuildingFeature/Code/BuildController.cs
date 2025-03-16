using System;
using System.Collections.Generic;
using _Root.BuildingFeature.Code.Factory;
using _Root.BuildingFeature.Code.Presenter;
using _Root.Core.Code.Interfaces;
using _Root.SaveFeature;
using _Root.SaveFeature.Model;
using UnityEngine;
using UpdateFeature;

namespace _Root.BuildingFeature.Code
{
    public class BuildController : IUpdatable
    {
        private BuildingPresenter _currentBuilding;
        private bool _isBuilding;
        private Camera _mainCamera = Camera.main;
        private Vector2 _mousePosition;
        private float _cellSize;
        private BuildingFactory _buildingFactory;
        private bool _canBuildHere;
        private HashSet<Vector2> _occupiedCells = new HashSet<Vector2>();
        public Action OnBuildingPlaced = delegate { };
        private bool _isDeleting;
        private List<BuildingPresenter> _buildings = new List<BuildingPresenter>();

        public BuildController(BuildingFactory buildingFactory, float cellSize)
        {
            _buildingFactory = buildingFactory;
            _cellSize = cellSize;
            Updater.Instance.AddUpdatable(this);
        }

        public void SetMousePosition(Vector3 mousePosition)
        {
            _mousePosition = mousePosition;
        }
        
        public void Build()
        {
            if (_currentBuilding != null && _isBuilding)
            {
                Vector3 position = _currentBuilding.View.transform.position;
                if (CanBePlacedToPosition(position))
                {
                   OnBuildingPlaced();
                   SaveManager.Instance.AddSaveData(new BuildingSaveData
                   {
                       Index = _currentBuilding.GetIndex(),
                       OccupiedPositions = _currentBuilding.GetOccupiedPositions(),
                       Position = position
                   });
                    MarkOccupied(position);
                    _isBuilding = false;
                    _currentBuilding.OnBuildingDestroyed += DestroyBuilding;
                    _buildings.Add(_currentBuilding);
                    _currentBuilding = null;
                }
                else
                {
                    Debug.Log("Occupied building");
                }
            }
        }

        public void Build(BuildingSaveData saveData)
        {
            CreateBuilding(saveData.Index);
            _currentBuilding.OnBuildingDestroyed += DestroyBuilding;
            _currentBuilding.SetBuildingToPosition(saveData.Position);
            foreach (var vector2 in saveData.OccupiedPositions)
            {
                _occupiedCells.Add(vector2);
            }
            SaveManager.Instance.AddSaveData(new BuildingSaveData
            {
                Index = saveData.Index,
                OccupiedPositions = saveData.OccupiedPositions,
                Position = saveData.Position
            });
            _buildings.Add(_currentBuilding);
            _isBuilding = false;
        }

        private void DestroyBuilding(BuildingPresenter buildingPresenter)
        {
            if (_isDeleting)
            {
                buildingPresenter.DestroyBuilding();
                buildingPresenter.DeocupySells(_occupiedCells);
                SaveManager.Instance.RemoveSaveData(buildingPresenter.View.transform.position);
            }
        }

        private bool CanBePlacedToPosition(Vector2 position)
        {
            for (int x = 0; x < _currentBuilding.GetBuildingSize().x; x++)
            {
                for (int y = 0; y < _currentBuilding.GetBuildingSize().y; y++)
                {
                    Vector2 checkPosition = new Vector2(position.x + x * _cellSize, position.y + y * _cellSize);
                    if (_occupiedCells.Contains(checkPosition))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void MarkOccupied(Vector2 position)
        {
            for (int x = 0; x < _currentBuilding.GetBuildingSize().x; x++)
            {
                for (int y = 0; y < _currentBuilding.GetBuildingSize().y; y++)
                {
                    Debug.Log(_currentBuilding);
                    Vector2 checkPosition = new Vector2(position.x + x * _cellSize, position.y + y * _cellSize);
                    _currentBuilding.AddOccupiedPosition(checkPosition);
                    _occupiedCells.Add(checkPosition);
                }
            }
        }

        public void UpdateTick(float deltaTime)
        {
            if (_currentBuilding != null && _isBuilding)
            {
                Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(_mousePosition);
                Vector3 gridPosition = new Vector3(
                    Mathf.Round(worldPosition.x / _cellSize) * _cellSize, 
                    Mathf.Round(worldPosition.y / _cellSize) * _cellSize,
                    0);
                _currentBuilding.SetBuildingToPosition(gridPosition);
            }
        }

        public void CreateBuilding(int currentSelectedItem)
        {
            _currentBuilding = _buildingFactory.CreateBuilding(currentSelectedItem, _cellSize);
            
            _isBuilding = true;
        }

        public void OnDeletingBuilding(bool obj)
        {
            _isDeleting = obj;
        }

        public void LoadGame(PlaceBuildingWrapper obj)
        {
            foreach (BuildingPresenter building in _buildings)
            {
                building.DestroyBuilding();
            }
            _buildings.Clear();
            _occupiedCells.Clear();
            foreach (var buildingSaveData in obj.Buildings)
            {
                Build(buildingSaveData);
            }
        }
    }
}