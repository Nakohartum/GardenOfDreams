using UnityEngine;
using UnityEngine.UI;

namespace _Root.UIFeature.Code.View
{
    public class BottomPanelView : MonoBehaviour
    {
        [field: Header("Buttons")]
        [field: SerializeField] public Button DeleteButton { get; private set; }
        [field: SerializeField] public Button PlaceButton { get; private set; }
        
        [field: Header("Buildings")]
        [field: SerializeField] public RectTransform BuildingsContainer { get; private set; }
    }
}