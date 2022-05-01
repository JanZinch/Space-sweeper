using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    
    [SerializeField] private MileageStage _mileageStage = null;
    [SerializeField] private WorldGenerator _worldGenerator = null;
    [SerializeField] private EffectsManager _effectsManager = null;

    [Space] 
    [SerializeField] private PlayerController _playerController = null;
    
    public PlayerController Player => _playerController;

    private void Awake()
    {
        Instance = this;
        _mileageStage.Size = _worldGenerator.GetMeshLength();
        _effectsManager.Initialize();
    }

    private void OnEnable()
    {
        _mileageStage.OnNextStage += () => _worldGenerator.Next();
    }

    /*private void Start()
    {
        PoolsManager.GetPooledObject(PooledObjectType.GAMMAZOID, Vector3.zero, Quaternion.identity);
    }*/

    private void OnDisable()
    {
        _mileageStage.OnNextStage -= () => _worldGenerator.Next();
    }
}
