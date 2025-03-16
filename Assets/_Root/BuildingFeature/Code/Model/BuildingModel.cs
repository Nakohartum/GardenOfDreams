using System.Collections.Generic;
using UnityEngine;

namespace _Root.BuildingFeature.Code.Model
{
    public class BuildingModel
    {
        public Sprite Sprite { get; set; }
        public Vector2Int Size { get; set; }
        public List<Vector2> OccupiedPositions { get; set; }
        public int PrefabIndex { get; set; }
    }
}