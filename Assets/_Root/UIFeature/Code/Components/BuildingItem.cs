using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.UIFeature.Code.Components
{
    public class BuildingItem : MonoBehaviour
    {
        [field:SerializeField] public Image Image{ get; private set; }
        [field:SerializeField] public Image BackgroundImage{ get; private set; }
        [field:SerializeField] public Color SelectedColor{ get; private set; }
        [field:SerializeField] public Color UnselectedColor{ get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
        public bool IsSelected { get; set; }
        public event Action<int> OnSelected;
        public int SelectedIndex { get; set; }

        private void Awake()
        {
            Button.onClick.AddListener(RaiseOnSelected);
        }

        public void InitializeView(Sprite sprite, int index)
        {
            Image.sprite = sprite;
            SelectedIndex = index;
        }

        public void RaiseOnSelected()
        {
            OnSelected?.Invoke(SelectedIndex);
        }
    }
}
