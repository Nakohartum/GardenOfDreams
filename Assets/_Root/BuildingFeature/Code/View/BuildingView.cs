using System;
using UnityEngine;

namespace _Root.BuildingFeature.Code.View
{
    public class BuildingView : MonoBehaviour
    {
        public event Action OnDestroyingBuilding = delegate { };
        private void OnMouseDown()
        {
            OnDestroyingBuilding();
        }
    }
}