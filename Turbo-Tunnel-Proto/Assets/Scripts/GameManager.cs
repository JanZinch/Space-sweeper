using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MileageStage _mileageStage = null;
    [SerializeField] private WorldGenerator _worldGenerator = null;


    private void Awake()
    {
        _mileageStage.Size = _worldGenerator.GetMeshLength();
    }

    private void OnEnable()
    {
        _mileageStage.OnNextStage += () => _worldGenerator.Next();
    }

    private void OnDisable()
    {
        _mileageStage.OnNextStage -= () => _worldGenerator.Next();
    }
}
