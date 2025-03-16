using _Root.BuildingFeature.Code;
using _Root.BuildingFeature.Code.ScriptableObjectCode;
using _Root.GridFeature.Code.Presenter;
using _Root.UIFeature.Code.Components;
using _Root.UIFeature.Code.Presenter;
using _Root.UIFeature.Code.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Root.UIFeature.Code.Initializer
{
    public class UIInitializer
    {
        private RectTransform _uiRoot;
        private BottomPanelView _bottomPanelViewPrefab;
        private BuildingSO[] _buildings;
        private BuildingItem _buildingItemPrefab;
        private BuildController _buildController;

        public UIInitializer(RectTransform uiRoot, BottomPanelView bottomPanelViewPrefab, BuildingSO[] buildings, BuildingItem buildingItemPrefab, BuildController buildController)
        {
            _uiRoot = uiRoot;
            _bottomPanelViewPrefab = bottomPanelViewPrefab;
            _buildings = buildings;
            _buildingItemPrefab = buildingItemPrefab;
            _buildController = buildController;
        }

        public void Initialize(GridPresenter gridPresenter)
        {
            var view = Object.Instantiate(_bottomPanelViewPrefab, _uiRoot);
            var presenter = new BottomPanelPresenter(view, _buildings, _buildingItemPrefab, _buildController);
            presenter.OnBuildingChosen += gridPresenter.ShowGrid;
            presenter.OnDeleteClicked += _buildController.OnDeletingBuilding;
        }
    }
}