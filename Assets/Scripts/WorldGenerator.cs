using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Transform _startSpawnPosition = null;
    [SerializeField] private int _pooledPartsCount = 6;

    private List<PooledObjectType> _tubePartsTypes = null;
    [SerializeField] private MeshRenderer _tubePartMesh = null;
    
    private Queue<PooledObject> _tubePartsPool = null;
    public Spawn Spawner { get; private set; } = null;

    public class Spawn {
        
        public Vector3 Addend { get; set; } = default;
        public Vector3 CurrentPosition { get; set; } = default;

        public Spawn(Vector3 startPosition, Vector3 addend) {

            CurrentPosition = startPosition;
            Addend = addend;        
        }

        public void Next() {

            CurrentPosition += Addend;        
        }
    }


    public float GetMeshLength() { 
    
        return _tubePartMesh.bounds.size.z;
    }

    private PooledObject SpawnTubePart(Vector3 position, Quaternion rotation) {

        PooledObjectType randomType = _tubePartsTypes[Random.Range(0, _tubePartsTypes.Count)];
        return PoolsManager.GetObject(randomType, position, rotation);             
    }

    private void Awake()
    {
        Spawner = new Spawn(_startSpawnPosition.position, new Vector3(0.0f, 0.0f, GetMeshLength()));

        _tubePartsTypes = PoolsExplorer.GetTunnelPartsPoolsIds();
        _tubePartsPool = new Queue<PooledObject>(_pooledPartsCount);

        for (int i = 0; i < _pooledPartsCount; i++)
        {
            PooledObject pooledObject = SpawnTubePart(Spawner.CurrentPosition, Quaternion.identity);
            _tubePartsPool.Enqueue(pooledObject);
            Spawner.Next();
        }
    }
    
    public void Next() {

        PooledObject _deletedObject = _tubePartsPool.Dequeue();
        _deletedObject.ReturnToPool();
        
        _tubePartsPool.Enqueue(SpawnTubePart(Spawner.CurrentPosition, Quaternion.identity));
        Spawner.Next();
    }
    
}
