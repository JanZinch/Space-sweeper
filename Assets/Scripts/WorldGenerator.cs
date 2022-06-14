using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using CodeBase.ApplicationLibrary.Common;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Transform _startSpawnPosition = null;
    [SerializeField] private int _pooledPartsCount = 6;

    [SerializeField] private MeshRenderer _tubePartMesh = null;
    [SerializeField] private bool _spawnRandom = false;
    
    private List<PooledObjectType> _tubePartsTypes = null;
    private Queue<PooledObject> _tubePartsPool = null;
    public Spawn Spawner { get; private set; } = null;
    
    private Func<Vector3, Quaternion, PooledObject> SpawnTubePart = null;
    private Queue<TubeData> _currentLevelMap = null;

    private Coroutine _exitRoutine = null;
    private WaitForSeconds _exitWaiting = null;
    
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

    private PooledObject SpawnRandomTubePart(Vector3 position, Quaternion rotation) {

        PooledObjectType randomType = _tubePartsTypes[Random.Range(0, _tubePartsTypes.Count)];
        PooledObject tube =  PoolsManager.GetPooledObject(randomType, position, rotation); 
        tube.GetLinkedComponent<ChannelTube>().Initialize();
        return tube;
    }

    private IEnumerator EndOfLevel()
    {
        Messenger.Broadcast(MessengerKeys.ON_LEVEL_PASSED);
        yield return _exitWaiting;
        SceneManager.Load(Scene.DOCK);
    }

    private PooledObject SpawnNextTubePart(Vector3 position, Quaternion rotation) {

        if (_currentLevelMap.Count == 0)
        {
            if (_exitRoutine == null)
            {
                _exitRoutine = StartCoroutine(EndOfLevel());
            }
            
            return null;  // end of level
        }
        
        TubeData tubeData = _currentLevelMap.Dequeue();
        
        PooledObject tube = PoolsManager.GetPooledObject(tubeData.TubeType, position, rotation * tubeData.Rotation);
        tube.GetLinkedComponent<ChannelTube>().Initialize();
        return tube;
    }

    private void Awake()
    {
        _exitWaiting = new WaitForSeconds(10.0f);
        
        if (_spawnRandom)
        {
            _tubePartsTypes = PoolsExplorer.GetTunnelPartsPoolsIds();
            SpawnTubePart = SpawnRandomTubePart;
        }
        else
        {
            _currentLevelMap = new Queue<TubeData>(TubesContainer.GetLevelMap(LevelUtils.CurrentLevel));
            SpawnTubePart = SpawnNextTubePart;
        }
        
        Spawner = new Spawn(_startSpawnPosition.position, new Vector3(0.0f, 0.0f, GetMeshLength()));
        
        _tubePartsPool = new Queue<PooledObject>(_pooledPartsCount);

        
    }

    private void Start()
    {
        for (int i = 0; i < _pooledPartsCount; i++)
        {
            PooledObject pooledObject = SpawnTubePart(Spawner.CurrentPosition, Quaternion.identity);

            if (pooledObject == null) break;
            
            _tubePartsPool.Enqueue(pooledObject);
            Spawner.Next();
        }
    }

    public void Next() {

        if (_tubePartsPool.Count != 0)
        {
            PooledObject deletedTube = _tubePartsPool.Dequeue();
            deletedTube.ReturnToPool();
        }
        
        PooledObject createdTube = SpawnTubePart(Spawner.CurrentPosition, Quaternion.identity);

        if (createdTube != null)
        {
            _tubePartsPool.Enqueue(createdTube);
            Spawner.Next();
        }

        
    }
    
}
