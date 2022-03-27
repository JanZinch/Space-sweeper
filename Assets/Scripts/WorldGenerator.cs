using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{


    
    [SerializeField] private Transform _startSpawnPosition = null;
    //[SerializeField] private Vector3 _spawnAddend = new Vector3(0.0f, 0.0f, 30.0f);
    [SerializeField] private int _pooledPartsCount = 6;

    [SerializeField] private List<int> _tubePartsPrefsId = null;
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

        int randomId = _tubePartsPrefsId[UnityEngine.Random.Range(0, _tubePartsPrefsId.Count)];

        return PoolsManager.GetObject(randomId, position, rotation).GetComponent<PooledObject>();             
    }

    private void Awake()
    {
        Spawner = new Spawn(_startSpawnPosition.position, new Vector3(0.0f, 0.0f, GetMeshLength()));

        //Debug.Log("Start Spawn addend: " + Spawner.Addend);

        _tubePartsPool = new Queue<PooledObject>(_pooledPartsCount);

        for (int i = 0; i < _pooledPartsCount; i++) {

            _tubePartsPool.Enqueue(SpawnTubePart(Spawner.CurrentPosition, Quaternion.identity));
            Spawner.Next();

            //Debug.Log("Spawn addend: " + Spawner.Addend);
            
        }
    }


    public void Next() {

        PooledObject _deletedObject = _tubePartsPool.Dequeue();
        _deletedObject.ReturnToPool();
        
        _tubePartsPool.Enqueue(SpawnTubePart(Spawner.CurrentPosition, Quaternion.identity));
        Spawner.Next();

        //Debug.Log("Spawn addend: " + Spawner.Addend);
    }

    private void OnDestroy()
    {
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
