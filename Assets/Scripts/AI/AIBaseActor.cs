using UnityEngine;

namespace AI
{
    public abstract class AIBaseActor : MonoBehaviour
    {
        public abstract void Initialize();
        
        public abstract void Clear();
    }
}