using _Root.BuildingFeature.Code.Factory;
using _Root.BuildingFeature.Code.ScriptableObjectCode;

namespace _Root.BuildingFeature.Code
{
    public class BuildFeatureInitializer
    {
        private BuildingSO[] _buildings;
        private float _cellSize;

        public BuildFeatureInitializer(BuildingSO[] buildings, float cellSize)
        {
            _buildings = buildings;
            _cellSize = cellSize;
        }

        public BuildController BuildController { get; private set; }
        public BuildingFactory BuildingFactory { get; private set; }

        public void Initialize()
        {
            
            BuildingFactory = new BuildingFactory(_buildings);
            BuildController = new BuildController(BuildingFactory, _cellSize);
        }
    }
}