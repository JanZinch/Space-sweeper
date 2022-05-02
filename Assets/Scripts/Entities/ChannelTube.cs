using UnityEngine;

namespace Entities
{
    public class ChannelTube : MonoBehaviour
    {
        [SerializeField] private ActorsSpawn _actorsSpawn = null;
        
        public void Initialize()
        {
            if (_actorsSpawn != null)
            {
                _actorsSpawn.Spawn();
            }
        }
    }
}