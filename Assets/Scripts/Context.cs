using System;
using Assets.Scripts.Setups;
using CodeBase.ApplicationLibrary.Common;
using CodeBase.ApplicationLibrary.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Context : MonoBehaviour
{
    [SerializeField] private DataHelper _dataHelper = null;
    [SerializeField] private DelayedCallback _delayedCallback = null;
    [SerializeField] private Bootstrapper _bootstrapper = null;
    [SerializeField] private EquipmentSetup _equipmentSetup = null;
    
    public static DataHelper DataHelper => _instance._dataHelper;
    public static DelayedCallback DelayedCallback => _instance._delayedCallback;
        
    private static Context _instance = null;

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.CurrentScene == Scene.CHANNEL)
        {
            _bootstrapper.InitializePools();
        }
    }

    private void Awake()
    {
        _instance = this;
        
        DontDestroyOnLoad(this.gameObject);
        
        EquipmentUtils.Initialize(_equipmentSetup);
        
        SceneManager.AddOnSceneLoadedListener(OnSceneLoaded);
    }

    private void OnDestroy()
    {
        SceneManager.RemoveOnSceneLoadedListener(OnSceneLoaded);
    }
}