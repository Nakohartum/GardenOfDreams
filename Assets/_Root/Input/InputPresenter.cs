using System;
using _Root.BuildingFeature.Code.Presenter;
using _Root.Core.Code.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UpdateFeature;

namespace _Root.Input
{
    public class InputPresenter : IUpdatable
    {
        private static InputPresenter _instance;
        
        private PlayerInput _playerInput;
        private BuildingPresenter _currentBuilding;
        public event Action OnClickPerfomed = delegate { };
        public event Action<Vector3> OnMouseMove = delegate { }; 
        public event Action OnSaveClicked = delegate { };
        public Action OnLoadClicked = delegate { };

        public static InputPresenter Instance => _instance ??= new InputPresenter();
        
        private InputPresenter()
        {
            _playerInput = new PlayerInput();
            InitializeInput();
            Updater.Instance.AddUpdatable(this);
        }

        private void InitializeInput()
        {
            _playerInput.Player.Enable();
            _playerInput.Player.Click.performed += ClickOnperformed;
            _playerInput.Player.Save.performed += SaveOnperformed;
            _playerInput.Player.Load.performed += LoadOnperformed;
        }

        private void LoadOnperformed(InputAction.CallbackContext obj)
        {
            OnLoadClicked();
        }

        private void SaveOnperformed(InputAction.CallbackContext obj)
        {
            OnSaveClicked();
        }

        private void ClickOnperformed(InputAction.CallbackContext obj)
        {
            OnClickPerfomed();
        }

        public void UpdateTick(float deltaTime)
        {
            OnMouseMove(_playerInput.Player.MouseMovement.ReadValue<Vector2>());
        }
    }
}