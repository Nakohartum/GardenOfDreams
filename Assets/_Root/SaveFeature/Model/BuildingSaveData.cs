using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Root.SaveFeature.Model
{
    [Serializable]
    public class BuildingSaveData
    {
        public Vector2 Position;
        public int Index;
        public List<Vector2> OccupiedPositions;
    }

    [Serializable]
    public class PlaceBuildingWrapper
    {
        public List<BuildingSaveData> Buildings;
    }
}