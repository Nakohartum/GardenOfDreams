using _Root.BuildingFeature.Code;
using _Root.BuildingFeature.Code.ScriptableObjectCode;
using _Root.GridFeature.Initializer;
using _Root.Input;
using _Root.SaveFeature;
using _Root.UIFeature.Code.Components;
using _Root.UIFeature.Code.Initializer;
using _Root.UIFeature.Code.View;
using UnityEngine;

namespace _Root.Core.Code
{
    public class Root : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private BuildingSO[] _buildingSO;
        [SerializeField] private RectTransform _uiRoot;
        [SerializeField] private BottomPanelView _bottomPanelViewPrefab;
        [SerializeField] private BuildingItem _buildingItemPrefab;
        private UIInitializer _uiInitializer;
        
        [Header("Grid")]
        [SerializeField] private GridInitializer _grid;


        private void Start()
        {
            
            _grid.Initialize();
            _grid.GridPresenter.HideGrid();
            var buildingInitializer = new BuildFeatureInitializer(_buildingSO, _grid.GridModel.CellSize);
            buildingInitializer.Initialize();
            _uiInitializer = new UIInitializer(_uiRoot, _bottomPanelViewPrefab, _buildingSO, _buildingItemPrefab, buildingInitializer.BuildController);
            _uiInitializer.Initialize(_grid.GridPresenter);
            buildingInitializer.BuildController.OnBuildingPlaced += _grid.GridPresenter.HideGrid;
            InputPresenter.Instance.OnClickPerfomed += buildingInitializer.BuildController.Build;
            InputPresenter.Instance.OnMouseMove += buildingInitializer.BuildController.SetMousePosition;
            InputPresenter.Instance.OnSaveClicked += SaveManager.Instance.SaveGame;
            InputPresenter.Instance.OnLoadClicked += SaveManager.Instance.LoadGame;
            SaveManager.Instance.OnGameLoading += buildingInitializer.BuildController.LoadGame;
        }
    }
}