using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private List<AIBaseActor> _actors = null;
        [SerializeField] private DestructibleObject _destructibleObject = null;

        private void OnEnable()
        {
            _destructibleObject.OnRefresh += Clear;
        }

        public void Initialize()
        {
            foreach (AIBaseActor actor in _actors)
            {
                actor.Initialize();
            }
        }

        public void Clear()
        {
            foreach (AIBaseActor actor in _actors)
            {
                actor.Clear();
            }
        }
        
        private void OnDisable()
        {
            _destructibleObject.OnRefresh -= Clear;
        }
    }
}