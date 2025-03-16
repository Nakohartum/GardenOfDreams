using _Root.BuildingFeature.Code.View;
using UnityEngine;

namespace _Root.BuildingFeature.Code.ScriptableObjectCode
{
    [CreateAssetMenu(fileName = nameof(BuildingSO), menuName = "Create/Building", order = 0)]
    public class BuildingSO : ScriptableObject
    {
        [field: SerializeField] public Sprite BuildingImage { get; private set; }
        [field: SerializeField] public BuildingView BuildingPrefab { get; private set; }
    }
}