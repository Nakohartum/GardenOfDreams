using System;
using System.Collections.Generic;
using System.IO;
using _Root.SaveFeature.Model;
using UnityEngine;

namespace _Root.SaveFeature
{
    public class SaveManager
    {
        private string _savePath;
        private static SaveManager _instance;

        public static SaveManager Instance
        {
            get { return _instance ??= new SaveManager(); }
        }

        private SaveManager()
        {
            _savePath = Path.Combine(Application.persistentDataPath, "save.json");
        }
        private List<BuildingSaveData> _buildingSaves = new List<BuildingSaveData>();
        public event Action<PlaceBuildingWrapper> OnGameLoading;

        public void AddSaveData(BuildingSaveData buildingSaveData)
        {
            _buildingSaves.Add(buildingSaveData);
        }

        public void SaveGame()
        {
            Debug.Log(_savePath);
            string json = JsonUtility.ToJson(new PlaceBuildingWrapper
            {
                Buildings = _buildingSaves
            });
            File.WriteAllText(_savePath, json);
        }

        public void LoadGame()
        {
            _buildingSaves.Clear();
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                PlaceBuildingWrapper wrapper = JsonUtility.FromJson<PlaceBuildingWrapper>(json);
                OnGameLoading(wrapper);
            }
        }

        public void RemoveSaveData(Vector2 transformPosition)
        {
            _buildingSaves.Remove(_buildingSaves.Find(q => q.Position == transformPosition));
        }
    }
}