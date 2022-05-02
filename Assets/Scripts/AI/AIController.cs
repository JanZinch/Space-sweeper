using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private List<AIBaseActor> _actors = null;

        public void Initialize()
        {
            foreach (AIBaseActor actor in _actors)
            {
                actor.Initialize();
            }
        }
    }
}