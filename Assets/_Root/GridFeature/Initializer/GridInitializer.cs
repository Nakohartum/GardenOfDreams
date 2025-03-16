using _Root.GridFeature.Code.Model;
using _Root.GridFeature.Code.Presenter;
using _Root.GridFeature.Code.View;
using UnityEngine;

namespace _Root.GridFeature.Initializer
{
    public class GridInitializer : MonoBehaviour
    {
        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private int _gridHeight = 1;
        [SerializeField] private int _gridWeight = 1;
        public GridPresenter GridPresenter { get; private set; }
        public GridModel GridModel { get; private set; }
        
        public void Initialize()
        {
            GridModel = new GridModel
            {
                CellSize = _cellSize,
                GridHeight = _gridHeight,
                GridWidth = _gridWeight,
            };

            var view = new GameObject("Grid").AddComponent<GridView>();
            GridPresenter = new GridPresenter(view, GridModel);
            GridPresenter.CreateGrid();
        }
    }
}