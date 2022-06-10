using UnityEngine;

namespace Entities
{
    public class ChannelTube : MonoBehaviour
    {
        [SerializeField] private Spawner[] _spawners = null;
        
        public void Initialize()
        {
            foreach (Spawner spawner in _spawners)
            {
                spawner.Spawn();
            }
        }
    }
}