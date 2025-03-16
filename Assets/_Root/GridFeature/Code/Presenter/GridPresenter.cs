using _Root.GridFeature.Code.Model;
using _Root.GridFeature.Code.View;
using UnityEngine;

namespace _Root.GridFeature.Code.Presenter
{
    public class GridPresenter
    {
        private GridView _gridView;
        private GridModel _gridModel;
        private bool _isVisible = false;

        public GridPresenter(GridView gridView, GridModel gridModel)
        {
            _gridView = gridView;
            _gridModel = gridModel;
        }

        public void CreateGrid()
        {
            if (_isVisible) return;

            for (int x = -_gridModel.GridWidth; x <= _gridModel.GridWidth; x++)
            {
                CreateLine(new Vector3(x * _gridModel.CellSize, -_gridModel.GridHeight * _gridModel.CellSize, 0),
                    new Vector3(x * _gridModel.CellSize, _gridModel.GridHeight * _gridModel.CellSize, 0));
            }

            for (int y = -_gridModel.GridHeight; y <= _gridModel.GridHeight; y++)
            {
                CreateLine(new Vector3(-_gridModel.GridWidth * _gridModel.CellSize, y * _gridModel.CellSize, 0), 
                    new Vector3(_gridModel.GridWidth * _gridModel.CellSize, y * _gridModel.CellSize, 0));
            }

            _isVisible = true;
        }

        public void ShowGrid()
        {
            if (_isVisible)
            {
                return;
            }
            _gridView.gameObject.SetActive(true);
            _isVisible = true;
        }

        public void HideGrid()
        {
            if (!_isVisible)
            {
                return;
            }
            _gridView.gameObject.SetActive(false);
            _isVisible = false;
        }
        
        private void CreateLine(Vector3 start, Vector3 end)
        {
            GameObject lineObj = new GameObject("GridLine");
            lineObj.transform.parent = _gridView.gameObject.transform;

            LineRenderer line = lineObj.AddComponent<LineRenderer>();
            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
            line.positionCount = 2;
            line.SetPosition(0, start);
            line.SetPosition(1, end);

            line.sortingOrder = 10;
            line.useWorldSpace = true;
        }
    }
}