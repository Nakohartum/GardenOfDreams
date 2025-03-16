using System;
using System.Collections.Generic;
using _Root.BuildingFeature.Code.Model;
using _Root.BuildingFeature.Code.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Root.BuildingFeature.Code.Presenter
{
    public class BuildingPresenter
    {
        private BuildingModel _buildingModel;
        public BuildingView View;
        public event Action<BuildingPresenter> OnBuildingDestroyed = delegate { };

        public BuildingPresenter(BuildingModel buildingModel, BuildingView view)
        {
            _buildingModel = buildingModel;
            View = view;
            View.OnDestroyingBuilding += OnDestroyBuilding;
        }

        private void OnDestroyBuilding()
        {
            OnBuildingDestroyed(this);
        }

        public void DestroyBuilding()
        {
            Object.Destroy(View.gameObject);
        }

        public void SetBuildingToPosition(Vector3 position)
        {
            View.gameObject.transform.position = position;
        }

        public Sprite GetBuildingSprite()
        {
            return _buildingModel.Sprite;
        }

        public Vector2Int GetBuildingSize()
        {
            return _buildingModel.Size;
        }

        public void AddOccupiedPosition(Vector2 checkPosition)
        {
            _buildingModel.OccupiedPositions.Add(checkPosition);
        }

        public void DeocupySells(HashSet<Vector2> occupiedCells)
        {
            foreach (var cell in _buildingModel.OccupiedPositions)
            {
                occupiedCells.Remove(cell);
            }
        }

        public List<Vector2> GetOccupiedPositions()
        {
            return _buildingModel.OccupiedPositions;
        }

        public int GetIndex()
        {
            return _buildingModel.PrefabIndex;
        }
    }
}