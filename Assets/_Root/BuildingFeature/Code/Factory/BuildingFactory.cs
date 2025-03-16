using System.Collections.Generic;
using _Root.BuildingFeature.Code.Model;
using _Root.BuildingFeature.Code.Presenter;
using _Root.BuildingFeature.Code.ScriptableObjectCode;
using _Root.BuildingFeature.Code.View;
using UnityEngine;

namespace _Root.BuildingFeature.Code.Factory
{
    public class BuildingFactory
    {
        private BuildingSO[] _buildingSOs;

        public BuildingFactory(BuildingSO[] buildingSos)
        {
            _buildingSOs = buildingSos;
        }

        public BuildingPresenter CreateBuilding(int index, float cellSize)
        {
            var view = Object.Instantiate(_buildingSOs[index].BuildingPrefab);
            var model = new BuildingModel
            {
                Sprite = _buildingSOs[index].BuildingImage,
                Size = CalculateSize(view, cellSize),
                OccupiedPositions = new List<Vector2>(),
                PrefabIndex = index,
            };

           
            
            var presenter = new BuildingPresenter(model, view);
            return presenter;
        }

        private Vector2Int CalculateSize(BuildingView view, float cellSize)
        {
            BoxCollider2D collider = view.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                Vector2 size = collider.size;
                int width = Mathf.CeilToInt(size.x / cellSize);
                int height = Mathf.CeilToInt(size.y / cellSize);
                return new Vector2Int(width, height);
            }

            // Если нет коллайдера, считаем 1x1
            return Vector2Int.one;
        }
    }
}