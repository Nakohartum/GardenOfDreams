using System;
using System.Collections.Generic;
using _Root.Core.Code.Interfaces;
using UnityEngine;

namespace UpdateFeature
{
    public class Updater : MonoBehaviour
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        private static Updater _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static Updater Instance => _instance;

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var updatable in _updatables)
            {
                updatable.UpdateTick(deltaTime);
            }
        }
    }
}

